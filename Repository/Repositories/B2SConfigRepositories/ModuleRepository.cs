using B2SConfig.Models;
using Data;
using Dto.B2SConfig.Connector;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;
using Data.Interfaces;

namespace Repository.Repositories.B2SConfigRepositories
{
    public class ModuleRepository : B2SConfigRepository<CfgModule>, IModuleRepository
    {
        private readonly AutoMapper.IMapper mapper;

        public ModuleRepository(IB2SConfigDBContext dbContext,AutoMapper.IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }

     
        public async Task<IEnumerable<ModuleDto>> GetAll(CancellationToken cancellationToken)
        {
            var lst = await DBContext.Database.FetchAsync<CfgModule>(cancellationToken);
            return lst.Select(m => new ModuleDto
            {
                ID = m.ID,
                AssemblyName = m.AssemblyName,
                ClassName = m.ClassName,
                Description = m.Description,
                ModuleType = m.ModuleType,
                EnableReversal = m.EnableReversal,
                IsFinancial = m.IsFinancial
            });
        }

        public async Task<ModuleDto> GetById(int id, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.FirstOrDefaultAsync<CfgModule>(cancellationToken, x => x.ID == id);
            if (entity == null) throw new Exception("ItemNotFound");
            return ModuleDto.FromEntity(mapper, entity);
        }

        public async Task<ModuleDto> AddModule(ModuleDto dto, CancellationToken cancellationToken)
        {
            var model = dto.ToEntity(mapper);
            await base.AddAsync(model, cancellationToken);
            return dto;
        }

        public async Task<ModuleDto> UpdateModule(ModuleDto dto, int id, CancellationToken cancellationToken)
        {
            var model = await DBContext.Database.FirstOrDefaultAsync<CfgModule>(cancellationToken, x => x.ID == id);
            model = dto.ToEntity(mapper, model);

            await base.UpdateAsync(model, cancellationToken);
            return dto;
        }
    }
}
