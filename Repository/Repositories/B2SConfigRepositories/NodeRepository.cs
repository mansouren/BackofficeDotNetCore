using B2SConfig.Models;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;
using Dto.B2SConfig.Node;
using Repository.PublicInterfaces;
using Data.Interfaces;

namespace Repository.Repositories.B2SConfigRepositories
{
    public class NodeRepository : B2SConfigRepository<CfgNode>, INodeRepository
    {
        private readonly IRepository<CfgNodeState> nodeStateRepository;
        private readonly AutoMapper.IMapper mapper;

        public NodeRepository(IB2SConfigDBContext dbContext, INodeStateRepository nodeStateRepository,AutoMapper.IMapper mapper) : base(dbContext)
        {
            this.nodeStateRepository = nodeStateRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<NodeDto>> GetAll(CancellationToken cancellationToken)
        {
            
            var nodeList = await DBContext.Database.FetchAsync<CfgNode>(cancellationToken);
            var nodeStatelst = await DBContext.Database.FetchAsync<CfgNodeState>(cancellationToken);

            var result = nodeList.Join(nodeStatelst, o => o.ID, i => i.NodeID, (o, i) => new NodeDto
            {
                ID = o.ID,
                AcqSwitchCode = o.AcqSwitchCode,
                BaseLogAddress = o.BaseLogAddress,
                BinaryAddress = i.BinaryAddress,
                Cmd = i.Cmd,
                CommandIPAddress = o.CommandIPAddress,
                CommandPort = o.CommandPort,
                CurrentVersion = i.CurrentVersion,
                ExpceptionLogPath = i.ExpceptionLogPath,
                HsmEnabled = o.HsmEnabled,
                IssuerFITTableID = o.IssuerFITTableID,
                IsNative = i.IsNative,
                MonitoringIPAddress = o.MonitoringIPAddress,
                IssSwitchCode = o.IssSwitchCode,
                LastChangeStateOn = i.LastChangeStateOn,
                PID = i.PID,
                LastUpgradeOn = i.LastUpgradeOn,
                MultiWorker = o.MultiWorker,
                MaxThreadPool = o.MaxThreadPool,
                TestTerminals = o.TestTerminals,
                UpgrageRequired = i.UpgrageRequired,
                TopupEnabled = o.TopupEnabled,
                MonitoringPort = o.MonitoringPort,
                NextState = i.NextState,
                NodeId = i.NodeID,
                NodeName = o.NodeName,
                PrSwitchCode = o.PrSwitchCode,
                RemoteUpdateEnabled = i.RemoteUpdateEnabled,
                ZoneId = o.ZoneId
            }).ToList();

            if (!result.Any()) throw new Exception("ListIsEmpty");

            return result;
        }
        public async Task<NodeDto> GetById(int id, CancellationToken cancellationToken)
        {
            var node = await DBContext.Database.FirstOrDefaultAsync<CfgNode>(cancellationToken, x => x.ID == id);
            if (node == null) throw new Exception("ItemNotFound");
            return NodeDto.FromEntity(mapper,node);
        }

     
        public async Task<NodeDto> AddNode(NodeDto nodeDto, CancellationToken cancellationToken)
        {

            DBContext.Database.BeginTransaction();
            try
            {
               
                var model = nodeDto.ToEntity(mapper);
                model.ID = await GenerateID();
                await base.AddAsync(model, cancellationToken);

                var nodeState = new CfgNodeState
                {
                    NodeID = model.ID,
                    RemoteUpdateEnabled = nodeDto.RemoteUpdateEnabled,
                    CurrentVersion = nodeDto.CurrentVersion,
                    UpgrageRequired = nodeDto.UpgrageRequired,
                    NextState = nodeDto.NextState,
                    ExpceptionLogPath = nodeDto.ExpceptionLogPath,
                    BinaryAddress = nodeDto.BinaryAddress,
                    IsNative = nodeDto.IsNative,
                    Cmd = nodeDto.Cmd,
                    PID = nodeDto.PID
                };

                await nodeStateRepository.AddAsync(nodeState, cancellationToken);

                DBContext.Database.CompleteTransaction();
                return nodeDto;
            }
            catch
            {
                DBContext.Database.AbortTransaction();
                throw;
            }


        }
        public async Task UpdateNode(NodeDto nodeDto, int id, CancellationToken cancellationToken)
        {
            DBContext.Database.BeginTransaction();
            try
            {
                var model = await DBContext.Database.FirstOrDefaultAsync<CfgNode>(cancellationToken, x => x.ID == id);
                model = nodeDto.ToEntity(mapper, model);
                await base.UpdateAsync(model, cancellationToken);

                var selectedNodeState = await GetNodeState(id, cancellationToken);
                selectedNodeState.NodeID = nodeDto.NodeId;
                selectedNodeState.RemoteUpdateEnabled = nodeDto.RemoteUpdateEnabled;
                selectedNodeState.CurrentVersion = nodeDto.CurrentVersion;
                selectedNodeState.UpgrageRequired = nodeDto.UpgrageRequired;
                selectedNodeState.NextState = nodeDto.NextState;
                selectedNodeState.ExpceptionLogPath = nodeDto.ExpceptionLogPath;
                selectedNodeState.BinaryAddress = nodeDto.BinaryAddress;
                selectedNodeState.IsNative = nodeDto.IsNative;
                selectedNodeState.Cmd = nodeDto.Cmd;
                selectedNodeState.PID = nodeDto.PID;

                await nodeStateRepository.UpdateAsync(selectedNodeState, cancellationToken);

                DBContext.Database.CompleteTransaction();
            }
            catch
            {
                DBContext.Database.AbortTransaction();
                throw;
            }




        }

        public async Task<CfgNodeState> GetNodeState(int id, CancellationToken cancellationToken)
        {
            var nodeState = await nodeStateRepository.DBContext.Database
                .FirstOrDefaultAsync<CfgNodeState>(cancellationToken, x => x.NodeID == id);
            if (nodeState == null) throw new Exception("ItemNotFound");
            return nodeState;
        }

        public async Task<IEnumerable<CfgNodeState>> GetAllNodeStates()
        {
            var nodeStates = await nodeStateRepository.DBContext.Database.FetchAsync<CfgNodeState>();
            return nodeStates;
        }

        public async Task DeleteNode(int id, CancellationToken cancellationToken)
        {
            DBContext.Database.BeginTransaction();
            try
            {
                var node = await DBContext.Database.FirstOrDefaultAsync<CfgNode>(cancellationToken, x => x.ID == id);
                await base.DeleteAsync(node, cancellationToken);
                var nodestate = await GetNodeState(id, cancellationToken);
                await nodeStateRepository.DeleteAsync(nodestate, cancellationToken);
                DBContext.Database.CompleteTransaction();
            }
            catch
            {
                DBContext.Database.AbortTransaction();
                throw;
            }
        }

        public async Task<bool> IsExistNode(int nodeId, CancellationToken cancellationToken)
        {
            var node = await GetById(nodeId, cancellationToken);
            if (node == null)
            {
                return true;
            }
            throw new Exception("NodeExistsInDB");
        }

        public async Task<int> GenerateID()
        {
            var nodes = await DBContext.Database.FetchAsync<CfgNode>();
            int Id = 0;
            if (nodes.Any())
                Id = nodes.Max(r => r.ID) + 1;
            return Id;
        }
    }
}
