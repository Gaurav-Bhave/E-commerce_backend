using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Practiced_E_commerce.Dto.Login;
using Practiced_E_commerce.Models;

namespace Practiced_E_commerce.Token
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string GenerateToken(UserRoleDto userrole)
        {
            var jwtsetting = _configuration.GetSection("JwtSetting");

            var secretkey = jwtsetting.GetValue<string>("SecretKey");
            var issuer = jwtsetting.GetValue<string>("Issuer");
            var audience = jwtsetting.GetValue<string>("Audience");
            var exipretime = jwtsetting.GetValue<int>("ExpiryMinutes");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier , userrole.Id.ToString()),
                new Claim(ClaimTypes.Email , userrole.Email),
                new Claim(ClaimTypes.Name , userrole.Name),
                new Claim(ClaimTypes.Role , userrole.RoleName)
            };

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(exipretime),
                signingCredentials : credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
