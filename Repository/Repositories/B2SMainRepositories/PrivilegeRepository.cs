using B2SMain.Models;
using Data.Interfaces;
using Repository.Interfaces.B2SMainInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;

namespace Repository.Repositories.B2SMainRepositories
{
    public class PrivilegeRepository : B2SMainRepository<Privilege>, IPrivilegeRepository
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;

        public PrivilegeRepository(IB2SMainDBContext dbContext, IUserRepository userRepository, IRoleRepository roleRepository) : base(dbContext)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        public async Task AddPrivilege(string gid, string privilegeTitle, string? gref, CancellationToken cancellationToken)
        {
            var privilege = new Privilege
            {
                gid = Guid.Parse(gid),
                Title = privilegeTitle,
                gref = string.IsNullOrEmpty(gref) ? null : Guid.Parse(gref)

            };
            await base.AddAsync(privilege, cancellationToken);
        }

        public bool CheckPrivilege(Guid gid, string username, CancellationToken cancellationToken)
        {
            var user = userRepository.GetUserByUserName(username);
            if (user.IsManager)
                return true;
            
            List<int> userRoles = userRepository.UserRoleCache.FindAll(x => x.UserId == user.Id).Select(x => x.RoleId).ToList();
            var privilege = DBContext.Database.SingleOrDefaultAsync<Privilege>(cancellationToken, x => x.gid == gid).Result;

            List<int> rolePrivileges = roleRepository.RolePrivilegesCache.FindAll(x => x.PrivilegeId == privilege.ID).Select(x => x.RoleId).ToList();

            return rolePrivileges.Any(p => userRoles.Contains(p));
        }
    }
}
