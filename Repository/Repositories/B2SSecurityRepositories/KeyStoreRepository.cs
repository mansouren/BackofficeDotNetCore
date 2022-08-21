using B2SSecurity.Models;
using Data.Interfaces;
using Dto.B2SSecurity;
using Repository.Interfaces.B2SSecurityInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;
using Utilities.Exceptions;

namespace Repository.Repositories.B2SSecurityRepositories
{
    public class KeyStoreRepository : B2SSecurityRepository<KeyStore>, IKeyStoreRepository
    {
        private readonly AutoMapper.IMapper mapper;

        public KeyStoreRepository(IB2SSecurityDBContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }


        public async Task<IEnumerable<KeyStoreDto>> GetAll(CancellationToken cancellationToken)
        {
            var list = await DBContext.Database.FetchAsync<KeyStore>(cancellationToken, x => x.KeyTypeId == 4);
            return list.Select(x => new KeyStoreDto
            {
                KeyTypeId = x.KeyTypeId,
                CreatedBy = x.CreatedBy,
                CreatedOn = x.CreatedOn,
                EncKeyId = x.EncKeyId,
                EncModel = x.EncModel,
                ID = x.KeyId,
                KeyValue1 = x.KeyValue1,
                KVC = x.KVC,
                SwitchId = x.SwitchId,
                Title = x.Title
            });
        }

        public async Task<KeyStoreDto> GetById(int keyId, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.SingleOrDefaultAsync<KeyStore>(cancellationToken, x => x.KeyId == keyId);
            if (entity == null) throw new NotFoundException("ItemNotFound");
            var dto = KeyStoreDto.FromEntity(mapper, entity);
            return dto;
        }
        public async Task<KeyStoreDto> AddKeyStore(KeyStoreDto dto, CancellationToken cancellationToken)
        {
            var entity = dto.ToEntity(mapper);
            entity.KeyId = dto.ID;
            await base.AddAsync(entity, cancellationToken);
            return dto;
        }

        public async Task<KeyStoreDto> UpdateKeyStore(int keyId, KeyStoreDto dto, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.SingleOrDefaultAsync<KeyStore>(cancellationToken, x => x.KeyId == keyId);
            entity.KeyId = dto.ID;
            await base.UpdateAsync(entity, cancellationToken);
            return dto;
        }
    }
}
