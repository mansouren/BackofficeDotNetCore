using B2SConfig.Models;
using Data;
using Dto.B2SConfig.Connector;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;
using Data.Interfaces;
using AutoMapper;

namespace Repository.Repositories.B2SConfigRepositories
{
    public class ModuleValidatorRepository : B2SConfigRepository<CfgModule_Validator>, IModuleValidatorRepository
    {
        private readonly AutoMapper.IMapper mapper;

        public ModuleValidatorRepository(IB2SConfigDBContext dbContext,AutoMapper.IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }

        
        public async Task<int> GenerateID()
        {
            var lst = await DBContext.Database.FetchAsync<CfgModule_Validator>();
            int id = 0;
            if (lst.Any())
                id = lst.Max(x => x.ID) + 1;
            return id;
        }

        public async Task<IEnumerable<ModuleValidatorDto>> GetAll(CancellationToken cancellationToken)
        {
            var lst = await DBContext.Database.FetchAsync<CfgModule_Validator>(cancellationToken);
            return lst.Select(x => new ModuleValidatorDto
            {
                ID = x.ID,
                AssemblyName = x.AssemblyName,
                ClassName = x.ClassName,
                Description = x.Description,
                Direction = x.Direction,
                ModuleType = x.ModuleType,
                Priority = x.Priority,
                ValidatorGroupID = x.ValidatorGroupID
            });
        }

        public async Task<ModuleValidatorDto> GetById(int id, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.FirstOrDefaultAsync<CfgModule_Validator>(cancellationToken, x => x.ID == id);
            if (entity == null) throw new Exception("ItemNotFound");
            return ModuleValidatorDto.FromEntity(mapper, entity);
        }
        public async Task<ModuleValidatorDto> AddModuleValidator(ModuleValidatorDto dto, CancellationToken cancellationToken)
        {
            var model = dto.ToEntity(mapper);
            model.ID = await GenerateID();
            await base.AddAsync(model, cancellationToken);
            return ModuleValidatorDto.FromEntity(mapper, model);
        }

        public async Task<ModuleValidatorDto> UpdateModuleValidator(ModuleValidatorDto dto, int id, CancellationToken cancellationToken)
        {
            var model = await DBContext.Database.FirstOrDefaultAsync<CfgModule_Validator>(cancellationToken, x => x.ID == id);
            if (model == null) throw new Exception("ItemNotFound");

            model = dto.ToEntity(mapper, model);
            model.ID = id;
            await base.UpdateAsync(model, cancellationToken);
            dto.ID = id;
            return dto;
        }
    }
}
