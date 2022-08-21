using B2SConfig.Models;
using Dto.B2SConfig.Channel;
using Framework.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces.B2SConfigInterfaceRepositories;

namespace BackEnd.Controllers.B2SConfig
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class ChannelInfoController : ControllerBase
    {
        private readonly IChannelInfoRepository channelInfoRepository;

        public ChannelInfoController(IChannelInfoRepository channelInfoRepository)
        {
            this.channelInfoRepository = channelInfoRepository;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChannelInfoDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await channelInfoRepository.GetAllChannels(cancellationToken));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ChannelInfoDto>> Get(int id, CancellationToken cancellationToken)
        {
            return Ok(await channelInfoRepository.GetChannelInfo(id, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<ChannelInfoDto>> Create(ChannelInfoDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }

            var result = await channelInfoRepository.AddChannelInfo(dto, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<ChannelInfoDto>> Update(ChannelInfoDto dto, int id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }
            var result = await channelInfoRepository.UpdateChannelInfo(dto, id, cancellationToken);
            return Ok(result);
        }

        
    }
}
