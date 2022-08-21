using B2SMain.Models;
using Data;
using Data.Interfaces;
using Repository.Interfaces.B2SMainInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;
using Utilities.Exceptions;
using Repository.PublicInterfaces;
using Dto.B2SMain;
using Utilities;
using static Utilities.Enums.BasicValue;

namespace Repository.Repositories.B2SMainRepositories
{
    public class UserRepository : B2SMainRepository<User>, IUserRepository
    {
        private readonly IB2SMainRepository<UserRole> userRoleRepository;
        private readonly AutoMapper.IMapper mapper;

        public List<UserRole> UserRoleCache => userRoleRepository.EntitiesCache;

        public UserRepository(IB2SMainDBContext dbContext, IB2SMainRepository<UserRole> userRoleRepository, AutoMapper.IMapper mapper) : base(dbContext)
        {
            this.userRoleRepository = userRoleRepository;
            this.mapper = mapper;
        }

        public async Task<User> GetUserByUserNameAndPassword(string userName, string password, CancellationToken cancellationToken)
        {
            try
            {
                var user = await DBContext.Database.SingleOrDefaultAsync<User>(cancellationToken, x => x.UserName == userName);

                if (user == null)
                    throw new NotFoundException("UserNotFound");

                if (user.Password != password)
                    throw new NotFoundException("UserNotFound");
                return user;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<UserJwtDto> Authenticate(LoginDto dto, CancellationToken cancellationToken)
        {
            try
            {
                string hashPassword = dto.Password.ToHash();
                var user = await GetUserByUserNameAndPassword(dto.UserName, hashPassword, cancellationToken);
                if (!user.Status)
                    throw new LogicException("UserAccountHasBeenBlocked");
                var roles = await userRoleRepository.DBContext.Database.FetchAsync<UserRole>(cancellationToken, x => x.UserId == user.Id);
                if (!user.IsManager && roles.Any())
                    throw new LogicException("UserHasNoRole");
                if (user.ExpirationPassDate <= DateTime.Now)
                {
                    user.Action = (int)UserActions.ChangePasswordIsMandatory;
                    await base.UpdateAsync(user, cancellationToken);
                }

                var userJwtDto = new UserJwtDto
                {
                    ID = user.Id,
                    UserName = user.UserName,
                    UserRoles = roles
                };
                return userJwtDto;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task AddUser(UserDto dto, CancellationToken cancellationToken)
        {
            await DBContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var model = dto.ToEntity(mapper);
                model.Password = model.Password.ToHash();
                model.ExpirationPassDate = DateTime.Now.AddMonths(1);
                await base.AddAsync(model, cancellationToken);
                foreach (var roleId in dto.RoleIds)
                {

                    var role = await DBContext.Database.FirstOrDefaultAsync<Role>(cancellationToken, x => x.Id == roleId);
                    if (role != null)
                    {
                        var userRole = new UserRole
                        {
                            RoleId = roleId,
                            UserId = model.Id
                        };
                        await userRoleRepository.AddAsync(userRole, cancellationToken);
                    }
                    else
                    {
                        throw new NotFoundException("RoleNotFound");
                    }

                }
                DBContext.Database.CompleteTransaction();
            }
            catch (Exception)
            {
                DBContext.Database.AbortTransaction();
                throw ;
            }

        }

        public async Task UpdateUser(UserDto dto, int id, CancellationToken cancellationToken)
        {
            await DBContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var user = await DBContext.Database.SingleOrDefaultAsync<User>(cancellationToken, x => x.Id == id);
                if (user == null) throw new NotFoundException("UserNotFound");
                dto.ToEntity(mapper, user);
                user.ExpirationPassDate = DateTime.Now.AddMonths(1);
                await base.UpdateAsync(user, cancellationToken);
                var userRoles = await DBContext.Database.FetchAsync<UserRole>(cancellationToken, x => x.UserId == user.Id);
                if (dto.HasChangeInPermissions)
                {
                    userRoles.ForEach(async x =>(await userRoleRepository.DBContext.Database.FetchAsync<UserRole>(cancellationToken)).Remove(x));
                    foreach (var roleId in dto.RoleIds)
                    {
                        var role = await DBContext.Database.FirstOrDefaultAsync<Role>(cancellationToken, x => x.Id == roleId);
                        if(role != null)
                        {
                            foreach (var item in userRoles)
                            {
                                item.RoleId = roleId;
                                item.UserId = user.Id;

                                await userRoleRepository.UpdateAsync(item, cancellationToken);

                            }
                        }
                        else
                        {
                            throw new NotFoundException("RoleNotFound");
                        }
                       
                    }
                }
                DBContext.Database.CompleteTransaction();
            }
            catch (Exception)
            {
                DBContext.Database.AbortTransaction();
                throw ;
            }
        }

        public async Task<bool> IsExistUser(string userName, string password, CancellationToken cancellationToken)
        {
            string hashedPass = password.ToHash();
            var user = await DBContext.Database.SingleOrDefaultAsync<User>(cancellationToken, x => x.UserName == userName && x.Password == hashedPass);
            if (user != null)
                throw new LogicException("UserIsDuplicated");

            return false;
        }

        public User GetUserByUserName(string username)
        {
            var user = DBContext.Database.SingleOrDefault<User>(x => x.UserName == username);
            if (user == null) throw new NotFoundException("UserNotFound");
            return user;
        }
    }
}
