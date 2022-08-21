using B2SConfig.Models;
using Data;
using Dto.B2SConfig.Connector;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;
using Data.Interfaces;

namespace Repository.Repositories.B2SConfigRepositories
{
    public class ModuleFormatterRepository : B2SConfigRepository<CfgModule_Formatter>, IModuleFormatterRepository
    {
        private readonly AutoMapper.IMapper mapper;

        public ModuleFormatterRepository(IB2SConfigDBContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ModuleFormatterDto>> GetAll(CancellationToken cancellationToken)
        {
            var list = await DBContext.Database.FetchAsync<CfgModule_Formatter>(cancellationToken);
            return list.Select(m => new ModuleFormatterDto
            {
                ID = m.ID,
                AssemblyName = m.AssemblyName,
                ClassName = m.ClassName,
                Description = m.Description,
                ModuleType = m.ModuleType
            });
        }

        public async Task<ModuleFormatterDto> GetById(int id, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.FirstOrDefaultAsync<CfgModule_Formatter>(cancellationToken, x => x.ID == id);
            if (entity == null) throw new Exception("ItemNotFound");
            return ModuleFormatterDto.FromEntity(mapper, entity);
        }

        public async Task<ModuleFormatterDto> AddModuleFormatter(ModuleFormatterDto dto, CancellationToken cancellationToken)
        {
            var model = dto.ToEntity(mapper);
            model.ID = await GenerateID();
            await base.AddAsync(model, cancellationToken);
            return dto;
        }

        public async Task<ModuleFormatterDto> UpdateModuleFormatter(ModuleFormatterDto dto, int id, CancellationToken cancellationToken)
        {
            var model = await DBContext.Database.FirstOrDefaultAsync<CfgModule_Formatter>(cancellationToken, x => x.ID == id);
            if (model == null) throw new Exception("ItemNotFound");
            model = dto.ToEntity(mapper,model);
            await base.UpdateAsync(model, cancellationToken);
            return dto;
        }

        public async Task<int> GenerateID()
        {
            var lst = await DBContext.Database.FetchAsync<CfgModule_Formatter>();
            int id = 0;
            if (lst.Any())
                id = lst.Max(x => x.ID) + 1;
            return id;
        }
    }
}
