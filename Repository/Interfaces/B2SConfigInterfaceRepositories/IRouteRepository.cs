using B2SConfig.Models;
using Dto.B2SConfig.Switch;
using Repository.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.B2SConfigInterfaceRepositories
{
    public interface IRouteRepository : IRepository<CfgRoute>
    {
        Task<IEnumerable<RouteDto>> GetAll(CancellationToken cancellationToken);
        Task<CfgRoute> GetById(int id, CancellationToken cancellationToken);
        Task AddRoute(RouteDto dto, CancellationToken cancellationToken);
        Task UpdateRoute(RouteDto dto, int id, CancellationToken cancellationToken);
        Task DeleteRoute(int id, CancellationToken cancellationToken);
       
    }
}
