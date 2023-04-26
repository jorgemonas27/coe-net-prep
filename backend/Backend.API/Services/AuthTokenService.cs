using Backend.API.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.API.Services
{
    public class AuthTokenService
    {
        private readonly JwtSettings _settings;

        public AuthTokenService(IOptionsMonitor<JwtSettings> optionsMonitor)
        {
            _settings = optionsMonitor.CurrentValue;
        }

        public JwtSecurityToken? GenerateToken(UserData data)
        {
            var claims = new List<Claim>();

            if (data.Username == "superAdmin" && data.Password == "pass")
            {
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
                GenerateClaims(data, claims);
            }
            else if (data.Username == "peter" && data.Password == "pass")
            {
                claims.Add(new Claim(ClaimTypes.Role, "user"));
                GenerateClaims(data, claims);
            }
            else if (data.Username == "charlie" && data.Password == "pass")
            {
                claims.Add(new Claim(ClaimTypes.Role, "it operator"));
                GenerateClaims(data, claims);
            }
            else
            {
                return null;
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                expires: DateTime.UtcNow.Add(_settings.Expire),
                claims: claims,
                signingCredentials: creds
            );
        }
        private void GenerateClaims(UserData data, List<Claim> claims)
        {
            claims.Add(new Claim("username", data.Username));
            claims.Add(new Claim(ClaimTypes.Name, $"User: {data.Username}"));
            claims.Add(new Claim("UserState", "Active"));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, data.Username));
            // this guarantees the token is unique
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        }
    }
}
