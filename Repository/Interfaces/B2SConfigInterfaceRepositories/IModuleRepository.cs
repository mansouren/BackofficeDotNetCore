using B2SConfig.Models;
using Dto.B2SConfig.Connector;
using Repository.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.B2SConfigInterfaceRepositories
{
    public interface IModuleRepository : IRepository<CfgModule>
    {
        Task<IEnumerable<ModuleDto>> GetAll(CancellationToken cancellationToken);
        Task<ModuleDto> GetById(int id, CancellationToken cancellationToken);
        Task<ModuleDto> AddModule(ModuleDto dto, CancellationToken cancellationToken);
        Task<ModuleDto> UpdateModule(ModuleDto dto, int id, CancellationToken cancellationToken);
        
    }
}
