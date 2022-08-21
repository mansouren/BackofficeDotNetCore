using Dto.B2SMain;
using Framework.Api;
using Framework.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces.B2SMainInterfaceRepositories;

namespace BackEnd.Controllers.B2SMain
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class AcqCurrencyExchangeController : ControllerBase
    {
        private readonly IAcqCurrencyExchangeRepository repository;

        public AcqCurrencyExchangeController(IAcqCurrencyExchangeRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AcqCurrencyExchangeDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await repository.GetAll(cancellationToken));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<AcqCurrencyExchangeDto>> Get(long id, CancellationToken cancellationToken)
        {
            return Ok(await repository.GetById(id, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<AcqCurrencyExchangeDto>> Create(AcqCurrencyExchangeDto dto, CancellationToken cancellationToken)
        {

            var result = await repository.AddAcqCurrencyExchange(dto, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id:long}")]
        public async Task<ApiResult<AcqCurrencyExchangeDto>> Update(AcqCurrencyExchangeDto dto, long id, CancellationToken cancellationToken)
        {

            var result = await repository.UpdateAcqCurrencyExchange(id, dto, cancellationToken);
            return Ok(result);
        }
    }
}
