using B2SConfig.Models;
using Data;
using Dto.B2SConfig.Switch;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;
using Data.Interfaces;

namespace Repository.Repositories.B2SConfigRepositories
{
    public class RouteRepository : B2SConfigRepository<CfgRoute>, IRouteRepository
    {
        private readonly AutoMapper.IMapper mapper;

        public RouteRepository(IB2SConfigDBContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task AddRoute(RouteDto dto, CancellationToken cancellationToken)
        {
           
            var model = dto.ToEntity(mapper);
            await base.AddAsync(model, cancellationToken);
        }

        public async Task DeleteRoute(int id, CancellationToken cancellationToken)
        {
            var entity = await GetById(id, cancellationToken);
            if (entity == null) throw new Exception("ItemNotFound");
            await base.DeleteAsync(entity, cancellationToken);
        }

        public async Task<IEnumerable<RouteDto>> GetAll(CancellationToken cancellationToken)
        {
            var lst = await DBContext.Database.FetchAsync<CfgRoute>(cancellationToken);
            return lst.Select(x => new RouteDto
            {
                ID = x.ID,
                DestConnector = x.DestConnector,
                CardGroupId = x.CardGroupId,
                FromPan = x.FromPan,
                FromTerm = x.FromTerm,
                GroupId = x.GroupId,
                IIN = x.IIN,
                ToPan = x.ToPan,
                ToTerm = x.ToTerm,
                PrCode = x.PrCode,
                ManagerID = x.ManagerID,
                Mti = x.Mti,
                NodeID = x.NodeID,
                PosCondition = x.PosCondition,
                Priority = x.Priority,
                ServiceBrokerID = x.ServiceBrokerID,
                SourceChannelID = x.SourceChannelID,
                SourceConnector = x.SourceConnector
            });
        }

        public async Task<CfgRoute> GetById(int id, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.FirstOrDefaultAsync<CfgRoute>(cancellationToken, x => x.ID == id);
            if (entity == null) throw new Exception("ItemNotFound");
            return entity;
        }

        public async Task UpdateRoute(RouteDto dto, int id, CancellationToken cancellationToken)
        {
            var model = await GetById(id, cancellationToken);
            model = dto.ToEntity(mapper, model);

            await base.UpdateAsync(model, cancellationToken);
        }
    }
}
