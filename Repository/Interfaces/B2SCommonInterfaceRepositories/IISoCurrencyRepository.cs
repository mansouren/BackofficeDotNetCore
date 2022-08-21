using B2SCommon.Models;
using Dto.B2SCommon;
using Repository.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.B2SCommonInterfaceRepositories
{
    public interface IISoCurrencyRepository : IRepository<ISOCurrency>
    {
        Task<IEnumerable<IsoCurrencyDto>> GetAll(CancellationToken cancellationToken);
        Task<IsoCurrencyDto> GetById(int id, CancellationToken cancellationToken);
        Task<IsoCurrencyDto> AddIsoCurrency(IsoCurrencyDto dto, CancellationToken cancellationToken);
        Task<IsoCurrencyDto> UpdateIsoCurrency(int id, IsoCurrencyDto dto, CancellationToken cancellationToken);
    }
}
