using Repository.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using B2SConfig.Models;
using Dto.B2SConfig.Node;

namespace Repository.Interfaces.B2SConfigInterfaceRepositories
{
    public interface INodeRepository : IRepository<CfgNode>
    {
        Task<int> GenerateID();
        Task<IEnumerable<NodeDto>> GetAll(CancellationToken cancellationToken);
        Task<NodeDto> GetById(int id, CancellationToken cancellationToken);
        Task<NodeDto> AddNode(NodeDto nodeDto, CancellationToken cancellationToken);
        Task UpdateNode(NodeDto nodeDto,int id, CancellationToken cancellationToken);
        Task DeleteNode(int id, CancellationToken cancellationToken);
        Task<CfgNodeState> GetNodeState(int id, CancellationToken cancellationToken);
        Task<IEnumerable<CfgNodeState>> GetAllNodeStates();
        Task<bool> IsExistNode(int nodeId, CancellationToken cancellationToken);

    }
}
