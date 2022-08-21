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
    public class FieldSelectController : ControllerBase
    {
        private readonly IFieldSelectRepository repository;

        public FieldSelectController(IFieldSelectRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FieldSelectDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await repository.GetAll(cancellationToken));
        }


        [HttpPost]
        public async Task<ActionResult<FieldSelectDto>> Create(FieldSelectDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }

            var result = await repository.AddFieldSelect(dto, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<FieldSelectDto>> Update(FieldSelectDto dto, int id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }
            var result = await repository.UpdateFieldSelect(dto, id, cancellationToken);
            return Ok(result);
        }
    }
}
