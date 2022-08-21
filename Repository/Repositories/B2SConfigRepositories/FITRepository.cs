using B2SConfig.Models;
using Data.Interfaces;
using Dto.B2SConfig;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;
using Utilities.Exceptions;

namespace Repository.Repositories.B2SConfigRepositories
{
    public class FITRepository : B2SConfigRepository<CfgFIT>, IFITRepository
    {
        private readonly AutoMapper.IMapper mapper;

        public FITRepository(IB2SConfigDBContext dbContext,AutoMapper.IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }

       

        public async Task<IEnumerable<FITDto>> GetAll(CancellationToken cancellationToken)
        {
           var list = await DBContext.Database.FetchAsync<CfgFIT>(cancellationToken);
           return list.Select(x => new FITDto
            {
                FITName = x.FITName,
                ID = x.ID
            });
        }

        public async Task<FITDto> GetById(int id, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.SingleOrDefaultAsync<CfgFIT>(cancellationToken, x => x.ID == id);
            if (entity == null) throw new NotFoundException("ItemNotFound");
            var dto = FITDto.FromEntity(mapper, entity);
            return dto;
        }

        public async Task<FITDto> AddFIT(FITDto dto, CancellationToken cancellationToken)
        {
            var entity = dto.ToEntity(mapper);
            await base.AddAsync(entity, cancellationToken);
            return dto;
        }

        public async Task<FITDto> UpdateFIT(FITDto dto, int id, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.SingleOrDefaultAsync<CfgFIT>(cancellationToken, x => x.ID == id);
            if (entity == null) throw new NotFoundException("ItemNotFound");
            entity = dto.ToEntity(mapper, entity);
            await base.UpdateAsync(entity, cancellationToken);
            return dto;
        }
    }
}
