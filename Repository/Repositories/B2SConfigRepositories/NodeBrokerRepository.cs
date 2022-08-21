using B2SConfig.Models;
using Data;
using Dto.B2SConfig.Switch;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;
using Data.Interfaces;

namespace Repository.Repositories.B2SConfigRepositories
{
    public class NodeBrokerRepository : B2SConfigRepository<CfgNodeBroker>, INodeBrokerRepository
    {
        private readonly AutoMapper.IMapper mapper;

        public NodeBrokerRepository(IB2SConfigDBContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task<IEnumerable<NodeBrokerDto>> GetAll(CancellationToken cancellationToken)
        {
            var lst = await DBContext.Database.FetchAsync<CfgNodeBroker>(cancellationToken);
            return lst.Select(x => new NodeBrokerDto
            {
                ID = x.ID,
                NodeID = x.NodeID,
                BrokerID = x.BrokerID,
                EnableConsole = x.EnableConsole,
                EnableLog = x.EnableLog,

            });
        }

        public async Task<NodeBrokerDto> GetById(int id, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.FirstOrDefaultAsync<CfgNodeBroker>(cancellationToken, x => x.ID == id);
            if (entity == null) throw new Exception("ItemNotFound");
            return NodeBrokerDto.FromEntity(mapper, entity);
        }

        public async Task<NodeBrokerDto> AddNodeBroker(NodeBrokerDto dto, CancellationToken cancellationToken)
        {
            var model = dto.ToEntity(mapper);
            await base.AddAsync(model, cancellationToken);
            return NodeBrokerDto.FromEntity(mapper, model);
        }


        public async Task<NodeBrokerDto> UpdateNodeBroker(NodeBrokerDto dto, int id, CancellationToken cancellationToken)
        {
            var model = await DBContext.Database.FirstOrDefaultAsync<CfgNodeBroker>(cancellationToken, x => x.ID == id);
            if (model == null) throw new Exception("ItemNotFound");

            model = dto.ToEntity(mapper, model);
            model.ID = id;
            await base.UpdateAsync(model, cancellationToken);
            dto.ID = id;
            return dto;
        }
    }
}
