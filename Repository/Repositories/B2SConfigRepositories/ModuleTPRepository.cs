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
    public class ModuleTPRepository : B2SConfigRepository<CfgModule_TP>, IModuleTPRepository
    {
        private readonly AutoMapper.IMapper mapper;

        public ModuleTPRepository(IB2SConfigDBContext dbContext,AutoMapper.IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ModuleTPDto>> GetAll(CancellationToken cancellationToken)
        {
            var entities = await DBContext.Database.FetchAsync<CfgModule_TP>(cancellationToken);
            return entities.Select(m => new ModuleTPDto
            {
                ID = m.ID,
                IsFinancial = m.IsFinancial,
                EnableReversal = m.EnableReversal,
                AssemblyName = m.AssemblyName,
                ClassName = m.ClassName,
                Description = m.Description,
                DestFunctionCode = m.DestFunctionCode,
                EnableConsole = m.EnableConsole,
                EnableLog = m.EnableLog,
                IsAllowed = m.IsAllowed,
                MappingRequired = m.MappingRequired,
                ModuleType = m.ModuleType,
                MTI = m.MTI,
                PrCode = m.PrCode,
                ServiceBrokerID = m.ServiceBrokerID,
                TxnType = m.TxnType
            });
        }

        public async Task<ModuleTPDto> GetById(int id, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.FirstOrDefaultAsync<CfgModule_TP>(cancellationToken, x => x.ID == id);
            if (entity == null) throw new Exception("ItemNotFound");
            return ModuleTPDto.FromEntity(mapper, entity);
        }

        public async Task<ModuleTPDto> AddModuleTp(ModuleTPDto dto, CancellationToken cancellationToken)
        {
            var model = dto.ToEntity(mapper);
            model.ID = await GenerateID();
            await base.AddAsync(model, cancellationToken);
            var result = ModuleTPDto.FromEntity(mapper, model);
            return result;
        }

        public async Task<ModuleTPDto> UpdateModuleTp(ModuleTPDto dto, int id, CancellationToken cancellationToken)
        {
            var model = await DBContext.Database.FirstOrDefaultAsync<CfgModule_TP>(cancellationToken, x => x.ID == id);
            if (model == null) throw new Exception("ItemNotFound");

            model = dto.ToEntity(mapper, model);
            model.ID = id;
            await base.UpdateAsync(model, cancellationToken);
            dto.ID = id;
            return dto;
        }

        public async Task<int> GenerateID()
        {
            var lst = await DBContext.Database.FetchAsync<CfgModule_TP>();
            int id = 0;
            if (lst.Any())
                id = lst.Max(x => x.ID) + 1;
            return id;
        }
    }
}
