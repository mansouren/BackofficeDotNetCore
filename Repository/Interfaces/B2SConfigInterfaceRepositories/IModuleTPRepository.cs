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
    public interface IModuleTPRepository : IRepository<CfgModule_TP>
    {
        Task<IEnumerable<ModuleTPDto>> GetAll(CancellationToken cancellationToken);
        Task<ModuleTPDto> GetById(int id, CancellationToken cancellationToken);
        Task<ModuleTPDto> AddModuleTp(ModuleTPDto dto, CancellationToken cancellationToken);
        Task<ModuleTPDto> UpdateModuleTp(ModuleTPDto dto, int id, CancellationToken cancellationToken);
        Task<int> GenerateID();
    }
}
