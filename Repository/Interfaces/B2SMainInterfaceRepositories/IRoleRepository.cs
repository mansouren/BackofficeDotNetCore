using B2SMain.Models;
using Dto.B2SMain;
using Repository.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.B2SMainInterfaceRepositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task AddRole(RoleDto roleDto, CancellationToken cancellationToken);
        Task UpdateRole(RoleDto dto, int id, CancellationToken cancellationToken);
        public List<RolePrivilege> RolePrivilegesCache { get;}
    }
}
