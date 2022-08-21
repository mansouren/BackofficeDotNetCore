using AutoMapper;
using B2SConfig.Models;
using Data.Interfaces;
using Dto.B2SConfig;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;
using Utilities.Exceptions;

namespace Repository.Repositories.B2SConfigRepositories
{
    public class RouteCardGroupRepository : B2SConfigRepository<CfgRouteCardGroup>, IRouteCardGroupRepository
    {
        private readonly AutoMapper.IMapper mapper;

        public RouteCardGroupRepository(IB2SConfigDBContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }


        public async Task<IEnumerable<RouteCardGroupDto>> GetAll(CancellationToken cancellationToken)
        {
            var routeCardGroups = await DBContext.Database.FetchAsync<CfgRouteCardGroup>(cancellationToken);
            
            return routeCardGroups.Select(x => new RouteCardGroupDto
            {
                Pan = x.Pan,
                GroupID = x.GroupID,
                IsActive = x.IsActive,
                CreatedBy = x.CreatedBy,
                CreatedOn = x.CreatedOn,
                ID = x.ID,
                ModifiedBy = x.ModifiedBy,
                ModifiedOn = x.ModifiedOn

            });

        }

        public async Task<RouteCardGroupDto> GetById(long id, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.SingleOrDefaultAsync<CfgRouteCardGroup>(cancellationToken, x => x.ID == id);
            if (entity == null) throw new NotFoundException("ItemNotFound");
            var dto = RouteCardGroupDto.FromEntity(mapper, entity);
            return dto;
        }

        public async Task<RouteCardGroupDto> AddRouteCardGroup(RouteCardGroupDto dto, CancellationToken cancellationToken)
        {
            var entity = dto.ToEntity(mapper);
            entity.CreatedOn = DateTime.Now;
            await base.AddAsync(entity, cancellationToken);
            return dto;
        }

        public async Task<RouteCardGroupDto> UpdateRouteCardGroup(RouteCardGroupDto dto, long id, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.SingleOrDefaultAsync<CfgRouteCardGroup>(cancellationToken, x => x.ID == id);
            if (entity == null) throw new NotFoundException("ItemNotFound");
            entity = dto.ToEntity(mapper, entity);
            entity.ModifiedOn = DateTime.Now;
            await base.UpdateAsync(entity, cancellationToken);
            return dto;
        }
    }
}
