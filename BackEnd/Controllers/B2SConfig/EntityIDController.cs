using Dto.B2SConfig.Entities;
using Framework.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces.B2SConfigInterfaceRepositories;

namespace BackEnd.Controllers.B2SConfig
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class EntityIDController : ControllerBase
    {
        private readonly IEntityIDRepository repository;

        public EntityIDController(IEntityIDRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntityIDDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await repository.GetAll(cancellationToken));
        }


        [HttpPost]
        public async Task<ActionResult<EntityIDDto>> Create(EntityIDDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }

           var result = await repository.AddEntityID(dto, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<EntityIDDto>> Update(EntityIDDto dto, int id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }
           var result = await repository.UpdateEntityID(dto, id, cancellationToken);
            return Ok(result);
        }
    }
}
