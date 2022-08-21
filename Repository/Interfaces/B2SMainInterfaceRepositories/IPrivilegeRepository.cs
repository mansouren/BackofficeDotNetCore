using B2SMain.Models;
using Repository.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.B2SMainInterfaceRepositories
{
    public interface IPrivilegeRepository : IRepository<Privilege>
    {
        Task AddPrivilege(string gid, string privilegeTitle, string? gref, CancellationToken cancellationToken);
        bool CheckPrivilege(Guid gid, string username, CancellationToken cancellationToken);
    }
}
