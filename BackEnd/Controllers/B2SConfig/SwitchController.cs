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
    public class SwitchController : ControllerBase
    {
        private readonly ISwitchRepository repository;

        public SwitchController(ISwitchRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SwitchDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await repository.GetAll(cancellationToken));
        }

        //[HttpGet("{id:long}")]
        //public async Task<ActionResult<CfgSwitch>> Get(int id, CancellationToken cancellationToken)
        //{
        //    return Ok(await repository.GetById(id, cancellationToken));
        //}

        [HttpPost]
        public async Task<ActionResult> Create(SwitchDto dto, CancellationToken cancellationToken)
        {
            
            await repository.AddSwitch(dto, cancellationToken);
            return Ok();
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult> Update(SwitchDto dto, int id, CancellationToken cancellationToken)
        {
           
            await repository.UpdateSwitch(dto, id, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id:long}")]
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await repository.DeleteSwitch(id, cancellationToken);
        }
    }
}
