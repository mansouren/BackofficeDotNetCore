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
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByUserNameAndPassword(string userName, string password,CancellationToken cancellationToken);
        Task<UserJwtDto> Authenticate(LoginDto dto, CancellationToken cancellationToken);
        Task AddUser(UserDto dto, CancellationToken cancellationToken);
        Task UpdateUser(UserDto dto, int id, CancellationToken cancellationToken);
        Task<bool> IsExistUser(string userName,string password,CancellationToken cancellationToken);
        User GetUserByUserName(string username);
        public List<UserRole> UserRoleCache { get; }
    }
}
