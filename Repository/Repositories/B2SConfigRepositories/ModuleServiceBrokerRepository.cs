using B2SConfig.Models;
using Data;
using Dto.B2SConfig.Connector;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;
using Data.Interfaces;

namespace Repository.Repositories.B2SConfigRepositories
{
    public class ModuleServiceBrokerRepository : B2SConfigRepository<CfgModule_ServiceBroker>, IModuleServiceBrokerRepository
    {
        private readonly AutoMapper.IMapper mapper;

        public ModuleServiceBrokerRepository(IB2SConfigDBContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }


        public async Task<IEnumerable<ModuleServiceBrokerDto>> GetAll(CancellationToken cancellationToken)
        {
            var entities = await DBContext.Database.FetchAsync<CfgModule_ServiceBroker>(cancellationToken);
            return entities.Select(x => new ModuleServiceBrokerDto
            {
                ID = x.ID,
                ModuleType = x.ModuleType,
                Description = x.Description,
                ClassName = x.ClassName,
                AssemblyName = x.AssemblyName,
                EnableReversal = x.EnableReversal,
                IsFinancial = x.IsFinancial
            });
        }

        public async Task<ModuleServiceBrokerDto> GetById(int id, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.FirstOrDefaultAsync<CfgModule_ServiceBroker>(cancellationToken, x => x.ID == id);
            if (entity == null) throw new Exception("ItemNotFound");
            var result = ModuleServiceBrokerDto.FromEntity(mapper, entity);
            return result;
        }

        public async Task<ModuleServiceBrokerDto> AddModuleServiceBroker(ModuleServiceBrokerDto dto, CancellationToken cancellationToken)
        {
            var model = dto.ToEntity(mapper);
            model.ID = await GenerateID();
            await base.AddAsync(model, cancellationToken);
            var result = ModuleServiceBrokerDto.FromEntity(mapper, model);
            return result;
        }

        public async Task<ModuleServiceBrokerDto> UpdateModuleServiceBroker(ModuleServiceBrokerDto dto, int id, CancellationToken cancellationToken)
        {
            var model = await DBContext.Database.FirstOrDefaultAsync<CfgModule_ServiceBroker>(cancellationToken, x => x.ID == id);
            if (model == null) throw new Exception("ItemNotFound");

            model = dto.ToEntity(mapper, model);
            model.ID = id;
            await base.UpdateAsync(model, cancellationToken);
            dto.ID = id;
            return dto;
        }
        public async Task<int> GenerateID()
        {
            var lst = await DBContext.Database.FetchAsync<CfgModule_ServiceBroker>();
            int id = 0;
            if (lst.Any())
                id = lst.Max(x => x.ID) + 1;
            return id;
        }
    }
}
