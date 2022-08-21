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
    public interface IModuleFormatterRepository : IRepository<CfgModule_Formatter>
    {
        Task<IEnumerable<ModuleFormatterDto>> GetAll(CancellationToken cancellationToken);
        Task<ModuleFormatterDto> GetById(int id, CancellationToken cancellationToken);
        Task<ModuleFormatterDto> AddModuleFormatter(ModuleFormatterDto dto, CancellationToken cancellationToken);
        Task<ModuleFormatterDto> UpdateModuleFormatter(ModuleFormatterDto dto,int id, CancellationToken cancellationToken);
        Task<int> GenerateID();
    }
}
