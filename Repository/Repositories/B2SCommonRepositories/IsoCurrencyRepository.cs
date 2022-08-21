using B2SCommon.Models;
using Data.Interfaces;
using Dto.B2SCommon;
using Repository.Interfaces.B2SCommonInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;
using Utilities.Exceptions;

namespace Repository.Repositories.B2SCommonRepositories
{
    public class IsoCurrencyRepository : B2SCommonRepository<ISOCurrency>, IISoCurrencyRepository
    {
        private readonly AutoMapper.IMapper mapper;

        public IsoCurrencyRepository(IB2SCommonDBContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }


        public async Task<IEnumerable<IsoCurrencyDto>> GetAll(CancellationToken cancellationToken)
        {
            var list = await DBContext.Database.FetchAsync<ISOCurrency>(cancellationToken);
            return list.Select(x => new IsoCurrencyDto
            {
                ID = x.ID,
                SeparatorPoints = x.SeparatorPoints,
                Symbol = x.Symbol
            });
        }

        public async Task<IsoCurrencyDto> GetById(int id, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.SingleOrDefaultAsync<ISOCurrency>(cancellationToken, x => x.ID == id);
            if (entity == null) throw new NotFoundException("ItemNotFound");
            var dto = IsoCurrencyDto.FromEntity(mapper, entity);
            return dto;
        }
        public async Task<IsoCurrencyDto> AddIsoCurrency(IsoCurrencyDto dto, CancellationToken cancellationToken)
        {
            var entity = dto.ToEntity(mapper);
            await base.AddAsync(entity, cancellationToken);
            return dto;
        }
        public async Task<IsoCurrencyDto> UpdateIsoCurrency(int id, IsoCurrencyDto dto, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.SingleOrDefaultAsync<ISOCurrency>(cancellationToken, x => x.ID == id);
            await base.UpdateAsync(entity, cancellationToken);
            return dto;
        }
    }
}
