using B2SConfig.Models;
using Dto.B2SConfig;
using Repository.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.B2SConfigInterfaceRepositories
{
    public interface IRouteCardGroupRepository : IRepository<CfgRouteCardGroup>
    {
        Task<IEnumerable<RouteCardGroupDto>> GetAll(CancellationToken cancellationToken);
        Task<RouteCardGroupDto> GetById(long id, CancellationToken cancellationToken);
        Task<RouteCardGroupDto> AddRouteCardGroup(RouteCardGroupDto dto, CancellationToken cancellationToken);
        Task<RouteCardGroupDto> UpdateRouteCardGroup(RouteCardGroupDto dto, long id, CancellationToken cancellationToken);
        
    }
}
