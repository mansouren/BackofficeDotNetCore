using Dto.B2SMain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces.B2SMainInterfaceRepositories;
using Utilities.Exceptions;

namespace BackEnd.Controllers.B2SMain
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }


        [HttpPost]
        public async Task<ActionResult> Create(RoleDto dto, CancellationToken cancellationToken)
        {
            await roleRepository.AddRole(dto, cancellationToken);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(RoleDto dto, int id, CancellationToken cancellationToken)
        {

            await roleRepository.UpdateRole(dto, id, cancellationToken);

            return Ok();
        }
    }
}
