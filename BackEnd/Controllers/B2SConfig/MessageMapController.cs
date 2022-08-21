using B2SConfig.Models;
using Dto.B2SConfig.Switch;
using Framework.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces.B2SConfigInterfaceRepositories;

namespace BackEnd.Controllers.B2SConfig
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class MessageMapController : ControllerBase
    {
        private readonly IMessageMapRepository messageMapRepository;

        public MessageMapController(IMessageMapRepository messageMapRepository)
        {
            this.messageMapRepository = messageMapRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageMapDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await messageMapRepository.GetAll(cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MessageMapDto>> Get(int id, CancellationToken cancellationToken)
        {
            return Ok(await messageMapRepository.GetById(id, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<MessageMapDto>> Create(MessageMapDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }

           var result = await messageMapRepository.AddMessageMap(dto, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<MessageMapDto>> Update(MessageMapDto dto, int id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }
           var result = await messageMapRepository.UpdateMessageMap(dto, id, cancellationToken);
            return Ok(result);
        }

      
    }
}
