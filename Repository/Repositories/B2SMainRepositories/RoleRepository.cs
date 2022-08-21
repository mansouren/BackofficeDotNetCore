using B2SMain.Models;
using Data;
using Data.Interfaces;
using Dto.B2SMain;
using Repository.Interfaces.B2SMainInterfaceRepositories;
using Repository.PublicClasses;
using Repository.PublicInterfaces;
using PetaPoco;
using Utilities.Exceptions;

namespace Repository.Repositories.B2SMainRepositories
{
    public class RoleRepository : B2SMainRepository<Role>, IRoleRepository
    {
        private readonly IB2SMainRepository<RolePrivilege> rolePrivilegeRepository;

        public RoleRepository(IB2SMainDBContext dbContext,IB2SMainRepository<RolePrivilege> rolePrivilegeRepository) : base(dbContext)
        {
            this.rolePrivilegeRepository = rolePrivilegeRepository;
        }

        public List<RolePrivilege> RolePrivilegesCache => rolePrivilegeRepository.EntitiesCache;

        public async Task AddRole(RoleDto roleDto, CancellationToken cancellationToken)
        {
            DBContext.Database.BeginTransaction();
            try
            {
                var role = new Role
                {
                    Title = roleDto.Title
                };
                await base.AddAsync(role, cancellationToken);
                foreach (var privilegeId in roleDto.PrivilegeIds)
                {
                    try
                    {
                        var privilege =await DBContext.Database.FirstOrDefaultAsync<Privilege>(cancellationToken, x => x.ID == privilegeId);
                        if(privilege != null)
                        {
                            var rolePrivilege = new RolePrivilege
                            {
                                RoleId = role.Id,
                                PrivilegeId = privilegeId
                            };
                            await rolePrivilegeRepository.AddAsync(rolePrivilege, cancellationToken);
                        }
                       
                    }
                    catch (Exception)
                    {

                        DBContext.Database.AbortTransaction();
                        throw new NotFoundException("PrivilegeNotFound");
                    }
                   
                }
                DBContext.Database.CompleteTransaction();
            }
            catch
            {
                DBContext.Database.AbortTransaction();
                throw new LogicException();
            }
        }

        public async Task UpdateRole(RoleDto dto, int id, CancellationToken cancellationToken)
        {
            DBContext.Database.BeginTransaction();
            try
            {
                var role =await DBContext.Database.SingleOrDefaultAsync<Role>(cancellationToken, x => x.Id == id);
                if (role == null) throw new NotFoundException("ItemNotFound");
                role.Title = dto.Title;
                await base.UpdateAsync(role, cancellationToken);
                var rolePrivileges =await DBContext.Database.FetchAsync<RolePrivilege>(cancellationToken, x => x.RoleId == id);
                if(dto.HasChangeInPrivileges)
                {
                    rolePrivileges.ForEach(async x =>await rolePrivilegeRepository.DBContext.Database.DeleteAsync<RolePrivilege>(x));
                    foreach (var privilegeId in dto.PrivilegeIds)
                    {
                        var privilege =await DBContext.Database.FirstOrDefaultAsync<Privilege>(cancellationToken, x => x.ID == privilegeId);
                        if(privilege != null)
                        {
                            var rp = new RolePrivilege
                            {
                                PrivilegeId = privilegeId,
                                RoleId = role.Id
                            };
                            await rolePrivilegeRepository.AddAsync(rp, cancellationToken);
                        }
                        else
                        {
                            throw new NotFoundException("PrivilegeNotFound");
                        }
                    }
                }
                DBContext.Database.CompleteTransaction();
            }
            catch (Exception)
            {
                DBContext.Database.AbortTransaction();
                throw;
            }
        }
    }
}
