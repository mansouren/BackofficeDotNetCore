using Dto.B2SCommon;
using Framework.Api;
using Framework.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces.B2SCommonInterfaceRepositories;

namespace BackEnd.Controllers.B2SCommon
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class IsoCurrencyController : ControllerBase
    {
        private readonly IISoCurrencyRepository repository;

        public IsoCurrencyController(IISoCurrencyRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IsoCurrencyDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await repository.GetAll(cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ApiResult<IsoCurrencyDto>> Get(int id, CancellationToken cancellationToken)
        {
            return Ok(await repository.GetById(id, cancellationToken));
        }

        [HttpPost]
        public async Task<ApiResult<IsoCurrencyDto>> Create(IsoCurrencyDto dto, CancellationToken cancellationToken)
        {

            var result = await repository.AddIsoCurrency(dto, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<ApiResult<IsoCurrencyDto>> Update(IsoCurrencyDto dto, int id, CancellationToken cancellationToken)
        {

            var result = await repository.UpdateIsoCurrency(id, dto, cancellationToken);
            return Ok(result);
        }
    }
}
