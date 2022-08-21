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
    public class ModuleController : ControllerBase
    {
        private readonly IModuleRepository repository;

        public ModuleController(IModuleRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await repository.GetAll(cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CfgModule>> Get(int id, CancellationToken cancellationToken)
        {
            return Ok(await repository.GetById(id, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult> Create(ModuleDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }

            await repository.AddModule(dto, cancellationToken);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(ModuleDto dto, int id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }
            await repository.UpdateModule(dto, id, cancellationToken);
            return Ok();
        }

       
    }
}
