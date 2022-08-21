using B2SConfig.Models;
using Data.Interfaces;
using Dto.B2SConfig.Channel;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;

namespace Repository.Repositories.B2SConfigRepositories
{
    public class ChannelBatchRepository : B2SConfigRepository<CfgChannelBatch>, IChannelBatchRepository
    {
        private readonly AutoMapper.IMapper mapper;

        public ChannelBatchRepository(IB2SConfigDBContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }

       

        public async Task<IEnumerable<ChannelBatchDto>> GetAllChannels(CancellationToken cancellationToken)
        {
            var channelBatchList = await DBContext.Database.FetchAsync<CfgChannelBatch>(cancellationToken);
            return channelBatchList.Select(c => new ChannelBatchDto
            {
                ID = c.ID,
                BatchDay = c.BatchDay,
                BatchMonth = c.BatchMonth,
                FromDate = c.FromDate,
                ToDate = c.ToDate,
                InterfaceID = c.InterfaceID,
                IsActive = c.IsActive,
                ServerDatetime = c.ServerDatetime,
                TraceNo = c.TraceNo,
                TrTraceNo = c.TrTraceNo

            });
        }

        public async Task<ChannelBatchDto> GetChannelBatch(long id, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.FirstOrDefaultAsync<CfgChannelBatch>(cancellationToken, x => x.ID == id);
            if (entity == null) throw new Exception("ItemNotFound");
            var result = ChannelBatchDto.FromEntity(mapper, entity);
            return result;
        }
        public async Task<ChannelBatchDto> AddChannelBatch(ChannelBatchDto dto, CancellationToken cancellationToken)
        {
            var model = dto.ToEntity(mapper);
            await base.AddAsync(model, cancellationToken);
            var result = ChannelBatchDto.FromEntity(mapper,model);
            return result;
        }

        public async Task<ChannelBatchDto> UpdateChannelBatch(long id, ChannelBatchDto dto, CancellationToken cancellationToken)
        {
            var model = await DBContext.Database.FirstOrDefaultAsync<CfgChannelBatch>(cancellationToken, x => x.ID == id);
            if (model == null) throw new Exception("ItemNotFound");

            model = dto.ToEntity(mapper,model);
            model.ID = id;
            await base.UpdateAsync(model, cancellationToken);
            dto.ID = id;
            return dto;
            
        }

    }
}
