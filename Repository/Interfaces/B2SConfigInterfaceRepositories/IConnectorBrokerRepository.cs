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
    public interface IConnectorBrokerRepository : IRepository<CfgConnectorBroker>
    {
        Task<IEnumerable<ConnectorBrokerDto>> GetAll(CancellationToken cancellationToken);
        Task<CfgConnectorBroker> GetById(int id, CancellationToken cancellationToken);
        Task AddConnectorBroker(ConnectorBrokerDto dto, CancellationToken cancellationToken);
        Task UpdateConnectorBroker(ConnectorBrokerDto dto, int id, CancellationToken cancellationToken);
        Task DeleteConnectorBroker(int id, CancellationToken cancellationToken);
    }
}
