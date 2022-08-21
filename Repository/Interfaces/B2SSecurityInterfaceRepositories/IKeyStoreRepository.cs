using B2SSecurity.Models;
using Dto.B2SSecurity;
using Repository.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.B2SSecurityInterfaceRepositories
{
    public interface IKeyStoreRepository : IRepository<KeyStore>
    {
        Task<IEnumerable<KeyStoreDto>> GetAll(CancellationToken cancellationToken);
        Task<KeyStoreDto> GetById(int keyId,CancellationToken cancellationToken);
        Task<KeyStoreDto> AddKeyStore(KeyStoreDto dto,CancellationToken cancellationToken);
        Task<KeyStoreDto> UpdateKeyStore(int keyId,KeyStoreDto dto,CancellationToken cancellationToken);
    }
}
