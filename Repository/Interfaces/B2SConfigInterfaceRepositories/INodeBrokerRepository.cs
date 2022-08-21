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
    public interface INodeBrokerRepository : IRepository<CfgNodeBroker>
    {
        Task<IEnumerable<NodeBrokerDto>> GetAll(CancellationToken cancellationToken);
        Task<NodeBrokerDto> GetById(int id, CancellationToken cancellationToken);
        Task<NodeBrokerDto> AddNodeBroker(NodeBrokerDto dto, CancellationToken cancellationToken);
        Task<NodeBrokerDto> UpdateNodeBroker(NodeBrokerDto dto, int id, CancellationToken cancellationToken);
    }
}
