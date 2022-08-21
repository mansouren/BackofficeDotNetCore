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
    public class ModuleFormatController : ControllerBase
    {
        private readonly IModuleFormatterRepository repository;

        public ModuleFormatController(IModuleFormatterRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleFormatterDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await repository.GetAll(cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ModuleFormatterDto>> Get(int id, CancellationToken cancellationToken)
        {
            return Ok(await repository.GetById(id, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult> Create(ModuleFormatterDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }

           var result =  await repository.AddModuleFormatter(dto, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(ModuleFormatterDto dto, int id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }
            var result= await repository.UpdateModuleFormatter(dto, id, cancellationToken);
            return Ok(result);
        }

       
    }
}
