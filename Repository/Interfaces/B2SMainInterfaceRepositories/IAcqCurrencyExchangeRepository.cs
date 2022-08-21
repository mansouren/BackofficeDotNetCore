using B2SMain.Models;
using Dto.B2SMain;
using Repository.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.B2SMainInterfaceRepositories
{
    public interface IAcqCurrencyExchangeRepository : IRepository<AcqCurrencyExchange>
    {
        Task<IEnumerable<AcqCurrencyExchangeDto>> GetAll(CancellationToken cancellationToken);
        Task<AcqCurrencyExchangeDto> GetById(long id, CancellationToken cancellationToken);
        Task<AcqCurrencyExchangeDto> AddAcqCurrencyExchange(AcqCurrencyExchangeDto dto, CancellationToken cancellationToken);
        Task<AcqCurrencyExchangeDto> UpdateAcqCurrencyExchange(long id, AcqCurrencyExchangeDto dto, CancellationToken cancellationToken);
    }
}
