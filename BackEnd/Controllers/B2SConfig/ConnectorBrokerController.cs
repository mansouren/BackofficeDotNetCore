using B2SConfig.Models;
using Dto.B2SConfig.Connector;
using Framework.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces.B2SConfigInterfaceRepositories;

namespace BackEnd.Controllers.B2SConfig
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class ConnectorBrokerController : ControllerBase
    {
        private readonly IConnectorBrokerRepository connectorBrokerRepository;

        public ConnectorBrokerController(IConnectorBrokerRepository connectorBrokerRepository)
        {
            this.connectorBrokerRepository = connectorBrokerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConnectorBrokerDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await connectorBrokerRepository.GetAll(cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ConnectorBrokerDto>> Get(int id, CancellationToken cancellationToken)
        {
            return Ok(await connectorBrokerRepository.GetById(id, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult> Create(ConnectorBrokerDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }

            await connectorBrokerRepository.AddConnectorBroker(dto, cancellationToken);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(ConnectorBrokerDto dto, int id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }
            await connectorBrokerRepository.UpdateConnectorBroker(dto, id, cancellationToken);
            return Ok();
        }

        //[HttpDelete("{id:int}")]
        //public async Task Delete(int id, CancellationToken cancellationToken)
        //{
        //    await connectorBrokerRepository.DeleteConnectorBroker(id, cancellationToken);
        //}
    }
}
