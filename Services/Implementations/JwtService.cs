using B2SMain.Models;
using Dto.B2SMain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Services.Implementations
{
    public class JwtService : IJwtService
    {
        private readonly SiteSettings settings;

        public JwtService(IOptionsSnapshot<SiteSettings> settings)
        {
            this.settings = settings.Value;
        }
        public JwtSecurityToken GenerateToken(UserJwtDto dto)
        {
            byte[] secretKey = Encoding.UTF8.GetBytes(settings.JwtSettings.SecretKey);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256);

            byte[] encryptKey = Encoding.UTF8.GetBytes(settings.JwtSettings.EncryptionKey);
            var encryptionCredential = new EncryptingCredentials(new SymmetricSecurityKey(encryptKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);


            var claims = new ClaimsIdentity(GetClaims(dto));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = settings.JwtSettings.Audience,
                Issuer = settings.JwtSettings.Issuer,
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(settings.JwtSettings.ExpirationMinutes),
                NotBefore = DateTime.UtcNow.AddMinutes(settings.JwtSettings.NotBeforeMinutes),
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptionCredential,
                Subject = claims
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return securityToken;
        }

        private IEnumerable<Claim> GetClaims(UserJwtDto dto)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, dto.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, dto.ID.ToString()));

            foreach (var userRole in dto.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.ToString()));
            }
            return claims;
        }
    }
}
