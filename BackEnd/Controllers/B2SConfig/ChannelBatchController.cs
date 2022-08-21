using B2SConfig.Models;
using Dto.B2SConfig.Channel;
using Framework.Api;
using Framework.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces.B2SConfigInterfaceRepositories;


namespace BackEnd.Controllers.B2SConfig
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class ChannelBatchController : ControllerBase
    {
        private readonly IChannelBatchRepository repository;

        public ChannelBatchController(IChannelBatchRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ApiResult<IEnumerable<ChannelBatchDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await repository.GetAllChannels(cancellationToken));
        }

        [HttpGet("{id:long}")]
        public async Task<ApiResult<ChannelBatchDto>> Get(int id, CancellationToken cancellationToken)
        {
            return Ok(await repository.GetChannelBatch(id, cancellationToken));
        }

        [HttpPost]
        public async Task<ApiResult<ChannelBatchDto>> Create(ChannelBatchDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }

           var result =  await repository.AddChannelBatch(dto, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id:long}")]
        public async Task<ApiResult<ChannelBatchDto>> Update(ChannelBatchDto dto, int id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                throw new Utilities.Exceptions.BadRequestException("ModelIsNotValid");
            }
            var result = await repository.UpdateChannelBatch(id,dto, cancellationToken);
            return Ok(result);
        }

    }
}
