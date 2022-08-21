using Dto.B2SConfig;
using Framework.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces.B2SConfigInterfaceRepositories;

namespace BackEnd.Controllers.B2SConfig
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class FITController : ControllerBase
    {
        private readonly IFITRepository repository;

        public FITController(IFITRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FITDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await repository.GetAll(cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FITDto>> Get(int id, CancellationToken cancellationToken)
        {
            return Ok(await repository.GetById(id, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<FITDto>> Create(FITDto dto, CancellationToken cancellationToken)
        {
            var result = await repository.AddFIT(dto, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<FITDto>> Update(FITDto dto, int id, CancellationToken cancellationToken)
        {

            var result = await repository.UpdateFIT(dto, id, cancellationToken);
            return Ok(result);
        }
    }
}
