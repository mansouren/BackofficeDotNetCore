using B2SConfig.Models;
using Data;
using Data.Interfaces;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.PublicClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;
using Dto.B2SConfig.Channel;

namespace Repository.Repositories.B2SConfigRepositories
{
    public class ChannelInfoRepository : B2SConfigRepository<CfgChannelInfo>, IChannelInfoRepository
    {
        private readonly AutoMapper.IMapper mapper;

        public ChannelInfoRepository(IB2SConfigDBContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }


        public async Task<IEnumerable<ChannelInfoDto>> GetAllChannels(CancellationToken cancellationToken)
        {
            try
            {
                var list = await DBContext.Database.FetchAsync<CfgChannelInfo>(cancellationToken);
                return list.Select(c => new ChannelInfoDto
                {
                    ID = c.ID,
                    CardExchnageTableId = c.CardExchnageTableId,
                    ChannelType = c.ChannelType,
                    ChannelBankAccountId = c.ChannelBankAccountId,
                    SettlementExchangeTableId = c.SettlementExchangeTableId,
                    CreatedOn = c.CreatedOn,
                    DefaultCurrency = c.DefaultCurrency,
                    Description = c.Description,
                    EncyptionMethodType = c.EncyptionMethodType,
                    FITID = c.FITID,
                    Identifier = c.Identifier,
                    IIN = c.IIN,
                    InterchangeFeeTableID = c.InterchangeFeeTableID,
                    IsSettlementSupported = c.IsSettlementSupported,
                    KeyCount = c.KeyCount,
                    MacFormat = c.MacFormat,
                    SecurityType = c.SecurityType,
                    SupportedCardHolderCurrencies = c.SupportedCardHolderCurrencies,
                    SupportedCurrencies = c.SupportedCurrencies,
                    SupportedSettlementCurrencies = c.SupportedSettlementCurrencies,
                    MacMethodType = c.MacMethodType,
                    RRNSeedID = c.RRNSeedID,
                    SupportedTxnTypes = c.SupportedTxnTypes,
                    SwitchCode = c.SwitchCode,
                    TransactionExchangeTableId = c.TransactionExchangeTableId,
                    ZMKEntryID = c.ZMKEntryID

                });
            }
            catch (Exception)
            {
                throw new NullReferenceException("ObjectNotFetched");

            }

        }

        public async Task<ChannelInfoDto> GetChannelInfo(long id, CancellationToken cancellationToken)
        {
            var model = await DBContext.Database.FirstOrDefaultAsync<CfgChannelInfo>(cancellationToken, x => x.ID == id);
            if (model == null) throw new Exception("ItemNotFound");
            var result = ChannelInfoDto.FromEntity(mapper, model);
            return result;
        }



        public async Task<ChannelInfoDto> AddChannelInfo(ChannelInfoDto dto, CancellationToken cancellationToken)
        {
            var model = dto.ToEntity(mapper);
            model.ID = await GenerateID();
            model.CreatedOn = DateTime.Now;
            await base.AddAsync(model, cancellationToken);
            return dto;
        }
        public async Task<ChannelInfoDto> UpdateChannelInfo(ChannelInfoDto dto, long id, CancellationToken cancellationToken)
        {
            var model = await DBContext.Database.FirstOrDefaultAsync<CfgChannelInfo>(cancellationToken, x => x.ID == id);
            if (model == null) throw new Exception("ItemNotFound");
            model = dto.ToEntity(mapper, model);
            await base.UpdateAsync(model, cancellationToken);
            return dto;
        }

        public async Task<long> GenerateID()
        {
            var channelInfos = await DBContext.Database.FetchAsync<CfgChannelInfo>();
            long Id = 0;
            if (channelInfos.Any())
                Id = channelInfos.Max(r => r.ID) + 1;
            return Id;
        }
    }
}
