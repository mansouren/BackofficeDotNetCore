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
    public class RouteController : ControllerBase
    {
        private readonly IRouteRepository repository;

        public RouteController(IRouteRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RouteDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await repository.GetAll(cancellationToken));
        }

        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<CfgRoute>> Get(int id, CancellationToken cancellationToken)
        //{
        //    return Ok(await repository.GetById(id, cancellationToken));
        //}

        [HttpPost]
        public async Task<ActionResult> Create(RouteDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }

            await repository.AddRoute(dto, cancellationToken);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(RouteDto dto, int id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }
            await repository.UpdateRoute(dto, id, cancellationToken);
            return Ok();
        }

        //[HttpDelete("{id:int}")]
        //public async Task Delete(int id, CancellationToken cancellationToken)
        //{
        //    await repository.DeleteRoute(id, cancellationToken);
        //}
    }
}
