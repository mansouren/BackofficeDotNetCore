using B2SConfig.Models;
using Data;
using Dto.B2SConfig.Channel;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;
using Data.Interfaces;


namespace Repository.Repositories.B2SConfigRepositories
{
    public class ChannelKeysRepository : B2SConfigRepository<CfgChannelKey>, IChannelKeysRepository
    {
        private readonly AutoMapper.IMapper mapper;

        public ChannelKeysRepository(IB2SConfigDBContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ChannelKeyDto>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var list = await DBContext.Database.FetchAsync<CfgChannelKey>(cancellationToken);
                return list.Select(c => new ChannelKeyDto
                {
                    ID = c.ID,
                    Title = c.Title,
                    ChannelID = c.ChannelID,
                    KeyEntryID = c.KeyEntryID,
                    IsActive = c.IsActive,
                    KeyIndex = c.KeyIndex,
                    KeyUsage = c.KeyUsage,
                    OldKeyEntryID = c.OldKeyEntryID,
                    CreatedOn = c.CreatedOn,
                    ModifiedOn = c.ModifiedOn
                });
            }
            catch (Exception)
            {

                throw new NullReferenceException("ObjectNotFetched");
            }

        }

        public async Task<ChannelKeyDto> GetById(int id, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.FirstOrDefaultAsync<CfgChannelKey>(cancellationToken, x => x.ID == id);
            if (entity == null) throw new Exception("ItemNotFound");
            var result = ChannelKeyDto.FromEntity(mapper, entity);
            return result;
        }

        public async Task<ChannelKeyDto> AddChannelKey(ChannelKeyDto dto, CancellationToken cancellationToken)
        {
            var entity = dto.ToEntity(mapper);
            entity.CreatedOn = DateTime.Now;
            entity.ModifiedOn = DateTime.Now;
            await base.AddAsync(entity, cancellationToken);
            return dto;

        }

        public async Task<ChannelKeyDto> UpdateChannelKey(ChannelKeyDto dto, int id, CancellationToken cancellationToken)
        {
            var model = await DBContext.Database.FirstOrDefaultAsync<CfgChannelKey>(cancellationToken, x => x.ID == id);
            if (model == null) throw new Exception("ItemNotFound");
            dto.ID = id;
            model = dto.ToEntity(mapper, model);
            model.ModifiedOn = DateTime.Now;
            await base.UpdateAsync(model, cancellationToken);
            return dto;
        }
    }
}
