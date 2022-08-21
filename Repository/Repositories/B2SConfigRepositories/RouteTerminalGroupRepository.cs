using B2SConfig.Models;
using Data.Interfaces;
using Dto.B2SConfig;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;
using Utilities.Exceptions;

namespace Repository.Repositories.B2SConfigRepositories
{
    public class RouteTerminalGroupRepository : B2SConfigRepository<CfgRouteTerminalGroup>, IRouteTerminalGroupRepository
    {
        private readonly AutoMapper.IMapper mapper;

        public RouteTerminalGroupRepository(IB2SConfigDBContext dbContext,AutoMapper.IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task<IEnumerable<RouteTerminalGroupDto>> GetAll(CancellationToken cancellationToken)
        {
            var routeCardGroups = await DBContext.Database.FetchAsync<CfgRouteTerminalGroup>(cancellationToken);
            return routeCardGroups.Select(x => new RouteTerminalGroupDto
            {
                TerminalNo = x.TerminalNo,
                GroupID = x.GroupID
            });

        }

        public async Task<RouteTerminalGroupDto> GetById(long id, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.SingleOrDefaultAsync<CfgRouteTerminalGroup>(cancellationToken, x => x.ID == id);
            if (entity == null) throw new NotFoundException("ItemNotFound");
            var dto = RouteTerminalGroupDto.FromEntity(mapper, entity);
            return dto;
        }

        public async Task<RouteTerminalGroupDto> AddRouteTerminalGroup(RouteTerminalGroupDto dto, CancellationToken cancellationToken)
        {
            var entity = dto.ToEntity(mapper);
            await base.AddAsync(entity, cancellationToken);
            return dto;
        }

        public async Task<RouteTerminalGroupDto> UpdateRouteTerminalGroup(RouteTerminalGroupDto dto, long id, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.SingleOrDefaultAsync<CfgRouteTerminalGroup>(cancellationToken, x => x.ID == id);
            if (entity == null) throw new NotFoundException("ItemNotFound");
            entity = dto.ToEntity(mapper, entity);
            await base.UpdateAsync(entity, cancellationToken);
            return dto;
        }
    }
}
