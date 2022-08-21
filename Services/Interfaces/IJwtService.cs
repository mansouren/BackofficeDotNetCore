using Dto.B2SMain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IJwtService
    {
        JwtSecurityToken GenerateToken(UserJwtDto dto);
    }
}
