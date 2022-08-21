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
    public class RouteCardGroupController : ControllerBase
    {
        private readonly IRouteCardGroupRepository repository;

        public RouteCardGroupController(IRouteCardGroupRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RouteCardGroupDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await repository.GetAll(cancellationToken));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<RouteCardGroupDto>> Get(long id, CancellationToken cancellationToken)
        {
            return Ok(await repository.GetById(id, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<RouteCardGroupDto>> Create(RouteCardGroupDto dto, CancellationToken cancellationToken)
        {
            var result = await repository.AddRouteCardGroup(dto, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<RouteCardGroupDto>> Update(RouteCardGroupDto dto, long id, CancellationToken cancellationToken)
        {
            
            var result = await repository.UpdateRouteCardGroup(dto, id, cancellationToken);
            return Ok(result);
        }
    }
}
