using B2SConfig.Models;
using Dto.B2SConfig.Connector;
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
    public class NodeBrokerController : ControllerBase
    {
        private readonly INodeBrokerRepository repository;

        public NodeBrokerController(INodeBrokerRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NodeBrokerDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await repository.GetAll(cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<NodeBrokerDto>> Get(int id, CancellationToken cancellationToken)
        {
            return Ok(await repository.GetById(id, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult> Create(NodeBrokerDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }

           var result=  await repository.AddNodeBroker(dto, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(NodeBrokerDto dto, int id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }
            var result= await repository.UpdateNodeBroker(dto, id, cancellationToken);
            return Ok(result);
        }

       
    }
}
