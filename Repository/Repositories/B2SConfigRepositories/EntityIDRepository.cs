using B2SConfig.Models;
using Data;
using Data.Interfaces;
using Dto.B2SConfig.Entities;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;

namespace Repository.Repositories.B2SConfigRepositories
{
    public class EntityIDRepository : B2SConfigRepository<CfgEntityID>, IEntityIDRepository
    {
        private readonly AutoMapper.IMapper mapper;

        public EntityIDRepository(IB2SConfigDBContext dbContext,AutoMapper.IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task<IEnumerable<EntityIDDto>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var list = await DBContext.Database.FetchAsync<CfgEntityID>(cancellationToken);
                return list.Select(e => new EntityIDDto
                {
                    ID = e.ID,
                    DefaultSeed = e.DefaultSeed,
                    CreatedOn = e.CreatedOn,
                    Description = e.Description,
                    Id1 = e.ID,
                    IncrementCounter = e.IncrementCounter,
                    LastID = e.LastID,
                });
            }
            catch (Exception)
            {

                throw new NullReferenceException("ObjectNotFetched");
            }
           
        }

        public async Task<EntityIDDto> GetByID(int id, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.FirstOrDefaultAsync<CfgEntityID>(cancellationToken, x => x.ID == id);
            if (entity == null) throw new Exception("ItemNotFound");
            return EntityIDDto.FromEntity(mapper, entity);
        }
        public async Task<EntityIDDto> AddEntityID(EntityIDDto dto, CancellationToken cancellationToken)
        {
            var entity = dto.ToEntity(mapper);
            entity.ID = await GenerateID();
            entity.CreatedOn = DateTime.Now;
            await base.AddAsync(entity, cancellationToken);
            return dto;
        }
        public async Task<EntityIDDto> UpdateEntityID(EntityIDDto dto, int id, CancellationToken cancellationToken)
        {
            var model = await DBContext.Database.FirstOrDefaultAsync<CfgEntityID>(cancellationToken, x => x.ID == id);
            model = dto.ToEntity(mapper, model);
            model.CreatedOn = DateTime.Now;
            await base.UpdateAsync(model, cancellationToken);
            return dto;

        }

        public async Task<int> GenerateID()
        {
            var lst = await DBContext.Database.FetchAsync<CfgEntityID>();
            int id = 0;
            if (lst.Any())
                id = lst.Max(x => x.ID) + 1;
            return id;
        }
    }
}
