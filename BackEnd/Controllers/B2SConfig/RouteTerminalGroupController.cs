using Dto.B2SConfig;
using Framework.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces.B2SConfigInterfaceRepositories;

namespace BackEnd.Controllers.B2SConfig
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class RouteTerminalGroupController : ControllerBase
    {
        private readonly IRouteTerminalGroupRepository repository;

        public RouteTerminalGroupController(IRouteTerminalGroupRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RouteTerminalGroupDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await repository.GetAll(cancellationToken));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<RouteTerminalGroupDto>> Get(long id, CancellationToken cancellationToken)
        {
            return Ok(await repository.GetById(id, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<RouteTerminalGroupDto>> Create(RouteTerminalGroupDto dto, CancellationToken cancellationToken)
        {
            var result = await repository.AddRouteTerminalGroup(dto, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<RouteTerminalGroupDto>> Update(RouteTerminalGroupDto dto, long id, CancellationToken cancellationToken)
        {

            var result = await repository.UpdateRouteTerminalGroup(dto, id, cancellationToken);
            return Ok(result);
        }
    }
}
