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
    public interface IRouteTerminalGroupRepository : IRepository<CfgRouteTerminalGroup>
    {
        Task<IEnumerable<RouteTerminalGroupDto>> GetAll(CancellationToken cancellationToken);
        Task<RouteTerminalGroupDto> GetById(long id, CancellationToken cancellationToken);
        Task<RouteTerminalGroupDto> AddRouteTerminalGroup(RouteTerminalGroupDto dto, CancellationToken cancellationToken);
        Task<RouteTerminalGroupDto> UpdateRouteTerminalGroup(RouteTerminalGroupDto dto, long id, CancellationToken cancellationToken);
    }
}
