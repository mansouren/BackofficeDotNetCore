using B2SConfig.Models;
using Data;
using Dto.B2SConfig.Switch;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;
using Data.Interfaces;

namespace Repository.Repositories.B2SConfigRepositories
{
    public class MessageMapRepository : B2SConfigRepository<CfgMessageMap>, IMessageMapRepository
    {
        private readonly AutoMapper.IMapper mapper;

        public MessageMapRepository(IB2SConfigDBContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }



        public async Task<IEnumerable<MessageMapDto>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var lst = await DBContext.Database.FetchAsync<CfgMessageMap>(cancellationToken);
                return lst.Select(m => new MessageMapDto
                {
                    ID = m.ID,
                    ChannelIdentifier = m.ChannelIdentifier,
                    IsAllowed = m.IsAllowed,
                    MappedMTI = m.MTI,
                    MappedPrCode = m.MappedPrCode,
                    Mti = m.MTI,
                    PrCode = m.PrCode

                });

            }
            catch (Exception)
            {

                throw new NullReferenceException("ObjectNotFetched");

            }

        }

        public async Task<MessageMapDto> GetById(int id, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.FirstOrDefaultAsync<CfgMessageMap>(cancellationToken, x => x.ID == id);
            if (entity == null) throw new Exception("ItemNotFound");
            return MessageMapDto.FromEntity(mapper, entity);

        }

        public async Task<MessageMapDto> AddMessageMap(MessageMapDto dto, CancellationToken cancellationToken)
        {
            var entity = dto.ToEntity(mapper);
            await base.AddAsync(entity, cancellationToken);
            return dto;
        }


        public async Task<MessageMapDto> UpdateMessageMap(MessageMapDto dto, int id, CancellationToken cancellationToken)
        {
            var model = await DBContext.Database.FirstOrDefaultAsync<CfgMessageMap>(cancellationToken, x => x.ID == id);
            model = dto.ToEntity(mapper, model);
            await base.UpdateAsync(model, cancellationToken);
            return dto;
        }
    }
}
