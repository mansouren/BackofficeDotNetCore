using Dto.B2SSecurity;
using Framework.Api;
using Framework.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces.B2SSecurityInterfaceRepositories;

namespace BackEnd.Controllers.B2SSecurity
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class KeyStoreController : ControllerBase
    {
        private readonly IKeyStoreRepository repository;

        public KeyStoreController(IKeyStoreRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KeyStoreDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await repository.GetAll(cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ApiResult<KeyStoreDto>> Get(int id, CancellationToken cancellationToken)
        {
            return Ok(await repository.GetById(id, cancellationToken));
        }

        [HttpPost]
        public async Task<ApiResult<KeyStoreDto>> Create(KeyStoreDto dto, CancellationToken cancellationToken)
        {
            
            var result = await repository.AddKeyStore(dto, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<ApiResult<KeyStoreDto>> Update(KeyStoreDto dto, int id, CancellationToken cancellationToken)
        {
            
            var result = await repository.UpdateKeyStore(id, dto, cancellationToken);
            return Ok(result);
        }
    }
}
