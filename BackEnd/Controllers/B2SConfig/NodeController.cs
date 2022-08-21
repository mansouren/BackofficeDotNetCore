using B2SConfig.Models;
using Dto.B2SConfig.Node;
using Framework.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces.B2SConfigInterfaceRepositories;

namespace BackEnd.Controllers.B2SConfig
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class NodeController : ControllerBase
    {
        private readonly INodeRepository nodeRepository;

        public NodeController(INodeRepository nodeRepository)
        {
            this.nodeRepository = nodeRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NodeDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await nodeRepository.GetAll(cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<NodeDto>> Get(int id,CancellationToken cancellationToken)
        {
            return Ok(await nodeRepository.GetById(id, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<NodeDto>> Create(NodeDto nodeDto,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(nodeDto);
            }

            await nodeRepository.AddNode(nodeDto, cancellationToken);
            return Ok(nodeDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<NodeDto>> Update(NodeDto nodeDto,int id,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(nodeDto);
            }
            await nodeRepository.UpdateNode(nodeDto, id, cancellationToken);
            return Ok(nodeDto);
        }

        [HttpDelete("{id:int}")]
        public async Task Delete(int id,CancellationToken cancellationToken)
        {
            await nodeRepository.DeleteNode(id, cancellationToken);
        }
    }
}
