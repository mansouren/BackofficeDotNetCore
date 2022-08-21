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
    public interface IModuleValidatorRepository : IRepository<CfgModule_Validator>
    {
        Task<IEnumerable<ModuleValidatorDto>> GetAll(CancellationToken cancellationToken);
        Task<ModuleValidatorDto> GetById(int id, CancellationToken cancellationToken);
        Task<ModuleValidatorDto> AddModuleValidator(ModuleValidatorDto dto, CancellationToken cancellationToken);
        Task<ModuleValidatorDto> UpdateModuleValidator(ModuleValidatorDto dto, int id, CancellationToken cancellationToken);
        Task<int> GenerateID();
    }
}
