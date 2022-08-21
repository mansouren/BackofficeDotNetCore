using B2SMain.Models;
using Data.Interfaces;
using Dto.B2SMain;
using Repository.Interfaces.B2SMainInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;
using Utilities.Exceptions;

namespace Repository.Repositories.B2SMainRepositories
{
    public class AcqCurrencyExchangeRepository : B2SMainRepository<AcqCurrencyExchange>, IAcqCurrencyExchangeRepository
    {
        private readonly AutoMapper.IMapper mapper;

        public AcqCurrencyExchangeRepository(IB2SMainDBContext dbContext,AutoMapper.IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }


        public async Task<IEnumerable<AcqCurrencyExchangeDto>> GetAll(CancellationToken cancellationToken)
        {
            var list = await DBContext.Database.FetchAsync<AcqCurrencyExchange>(cancellationToken);
            return list.Select(x => new AcqCurrencyExchangeDto
            {
                ID = x.ID,
                ConstAmount = x.ConstAmount,
                DestCurrencyId = x.DestCurrencyId,
                ExchangeRate = x.ExchangeRate,
                MaxAmount = x.MaxAmount,
                MinAmount = x.MinAmount,
                Percent = x.Percent,
                ReverseExchangeRate = x.ReverseExchangeRate,
                SourceCurrencyId = x.SourceCurrencyId,
                TxnFeeId = x.TxnFeeId,
            });
            
        }

        public async Task<AcqCurrencyExchangeDto> GetById(long id, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.SingleOrDefaultAsync<AcqCurrencyExchange>(cancellationToken, x => x.ID == id);
            if (entity == null) throw new NotFoundException("ItemNotFound");
            var dto = AcqCurrencyExchangeDto.FromEntity(mapper, entity);
            return dto;
        }
        
        public async Task<AcqCurrencyExchangeDto> AddAcqCurrencyExchange(AcqCurrencyExchangeDto dto, CancellationToken cancellationToken)
        {
            var entity = dto.ToEntity(mapper);
            await base.AddAsync(entity, cancellationToken);
            return dto;
        }
        
        public async Task<AcqCurrencyExchangeDto> UpdateAcqCurrencyExchange(long id, AcqCurrencyExchangeDto dto, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.SingleOrDefaultAsync<AcqCurrencyExchange>(cancellationToken, x => x.ID == id);
            await base.UpdateAsync(entity, cancellationToken);
            return dto;
        }
    }
}
