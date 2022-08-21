using AutoMapper;
using B2SConfig.Models;
using Dto.B2SConfig.Channel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using AutoMapper.QueryableExtensions;
using Framework.Api.Filters;
using Framework.Attributes;

namespace BackEnd.Controllers.B2SConfig
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    [PrivilegeChecker("EB667B24-B89D-460A-8800-442BB7F45178", "ChannelKey")]
    public class ChannelKeyController : ControllerBase
    {
        private readonly IChannelKeysRepository channelKeysRepository;


        public ChannelKeyController(IChannelKeysRepository channelKeysRepository)
        {
            this.channelKeysRepository = channelKeysRepository;

        }

        [HttpGet]
        [PrivilegeChecker("3F674A2B-B8FD-4273-AA90-9AB723ACD990", "GetAllChannelKey")]
        public async Task<ActionResult<IEnumerable<ChannelKeyDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await channelKeysRepository.GetAll(cancellationToken));
        }

        [HttpGet("{id:int}")]
        [PrivilegeChecker("EFA79C30-09A1-41E6-90B0-D5964D7875A6", "GetChannelKey")]
        public async Task<ActionResult<ChannelKeyDto>> Get(int id, CancellationToken cancellationToken)
        {
            return Ok(await channelKeysRepository.GetById(id, cancellationToken));
        }

        [HttpPost]
        [PrivilegeChecker("040751F5-7241-47AA-B129-3D691B73A72A", "AddChannelKey")]
        public async Task<ActionResult<ChannelKeyDto>> Create(ChannelKeyDto dto, CancellationToken cancellationToken)
        {
            
            var result = await channelKeysRepository.AddChannelKey(dto, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        [PrivilegeChecker("5D33CB4A-0C89-49F2-A40D-81B2FFE91977", "UpdateChannelKey")]
        public async Task<ActionResult<ChannelKeyDto>> Update(ChannelKeyDto dto, int id, CancellationToken cancellationToken)
        {
            
           var result =  await channelKeysRepository.UpdateChannelKey(dto, id, cancellationToken);
            return Ok(result);
        }

       
    }
}
