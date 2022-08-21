using B2SConfig.Models;
using Dto.B2SConfig.Connector;
using Framework.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces.B2SConfigInterfaceRepositories;

namespace BackEnd.Controllers.B2SConfig
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class ModuleServiceBrokerController : ControllerBase
    {
        private readonly IModuleServiceBrokerRepository repository;

        public ModuleServiceBrokerController(IModuleServiceBrokerRepository repository)
        {
            this.repository = repository;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleServiceBrokerDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await repository.GetAll(cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ModuleServiceBrokerDto>> Get(int id, CancellationToken cancellationToken)
        {
            return Ok(await repository.GetById(id, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult> Create(ModuleServiceBrokerDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }

           var result = await repository.AddModuleServiceBroker(dto, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(ModuleServiceBrokerDto dto, int id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }
            var result = await repository.UpdateModuleServiceBroker(dto, id, cancellationToken);
            return Ok(result);
        }

        
    }
}
