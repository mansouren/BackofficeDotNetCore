using Dto.B2SConfig.Tasks;
using Framework.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces.B2SConfigInterfaceRepositories;

namespace BackEnd.Controllers.B2SConfig
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository repository;

        public TaskController(ITaskRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await repository.GetAll(cancellationToken));
        }


        [HttpPost]
        public async Task<ActionResult> Create(TaskDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }

            await repository.AddTask(dto, cancellationToken);
            return Ok();
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult> Update(TaskDto dto, int id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }
            await repository.UpdateTask(dto, id, cancellationToken);
            return Ok();
        }

        
    }
}
