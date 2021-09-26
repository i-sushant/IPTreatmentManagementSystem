using AuthorizationService.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationService.Service
{
    public class TokenService : ITokenService
    {
        
        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysuperdupersecret"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
               {
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("Id", user.Id.ToString()),

                };
            var token = new JwtSecurityToken(
                    issuer: "mySystem",
                    audience: "myUsers",
                    claims: claims,
                    expires: DateTime.Now.AddDays(10),
                    signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
