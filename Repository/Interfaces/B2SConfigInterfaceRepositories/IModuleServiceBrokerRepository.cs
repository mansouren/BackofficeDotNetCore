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
    public interface IModuleServiceBrokerRepository : IRepository<CfgModule_ServiceBroker>
    {
        Task<IEnumerable<ModuleServiceBrokerDto>> GetAll(CancellationToken cancellationToken);
        Task<ModuleServiceBrokerDto> GetById(int id, CancellationToken cancellationToken);
        Task<ModuleServiceBrokerDto> AddModuleServiceBroker(ModuleServiceBrokerDto dto, CancellationToken cancellationToken);
        Task<ModuleServiceBrokerDto> UpdateModuleServiceBroker(ModuleServiceBrokerDto dto, int id, CancellationToken cancellationToken);
        Task<int> GenerateID();
    }
}
