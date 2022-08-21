using Dto.B2SMain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces.B2SMainInterfaceRepositories;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace BackEnd.Controllers.B2SMain
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJwtService jwtService;
        private readonly IUserRepository userRepository;

        public UserController(IJwtService jwtService,IUserRepository userRepository)
        {
            this.jwtService = jwtService;
            this.userRepository = userRepository;
            
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] UserDto dto, CancellationToken cancellationToken)
        {
            
            bool exit = await userRepository.IsExistUser(dto.UserName, dto.Password, cancellationToken);
           if (!exit)
            await userRepository.AddUser(dto, cancellationToken);
            return Ok();

        }


        //[PermissionChecker("8262378D-BF99-4CFC-80D9-F89B3BF10388", "Edit")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserDto dto, CancellationToken cancellationToken)
        {
            await userRepository.UpdateUser(dto,id, cancellationToken);
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDto dto,CancellationToken cancellationToken)
        {
            var userJwtDto =await userRepository.Authenticate(dto, cancellationToken);
            var token = jwtService.GenerateToken(userJwtDto);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(accessToken);
        }
    }
}
