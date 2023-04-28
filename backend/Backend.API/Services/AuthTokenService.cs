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

        public JwtSecurityToken? GenerateToken(UserLoginData data, List<UserInformation> users)
        {
            var claims = new List<Claim>();

            if (IsAdmin(data.Username, data.Password, users))
            {
                claims.Add(new Claim(ClaimTypes.Role, Properties.Resources.ADMIN));
                GenerateClaims(data, claims);
            }
            else if (IsUser(data.Username, data.Password, users))
            {
                claims.Add(new Claim(ClaimTypes.Role, Properties.Resources.USER));
                GenerateClaims(data, claims);
            }
            else if (IsItOperator(data.Username, data.Password, users))
            {
                claims.Add(new Claim(ClaimTypes.Role, Properties.Resources.ITOPERATOR));
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

        private void GenerateClaims(UserLoginData data, List<Claim> claims)
        {
            claims.Add(new Claim("username", data.Username));
            claims.Add(new Claim(ClaimTypes.Name, $"User: {data.Username}"));
            claims.Add(new Claim("UserState", "Active"));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, data.Username));
            // this guarantees the token is unique
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        }

        private bool IsAdmin(string username, string password, List<UserInformation> users) =>
             users
            .Where(user => user.Username == username && user.Password == password && user.Role == Properties.Resources.ADMIN)
            .FirstOrDefault() != null;

        private bool IsUser(string username, string password, List<UserInformation> users) =>
             users
            .Where(user => user.Username == username && user.Password == password && user.Role == Properties.Resources.USER)
            .FirstOrDefault() != null;
        
        private bool IsItOperator(string username, string password, List<UserInformation> users) =>
             users
            .Where(user => user.Username == username && user.Password == password && user.Role == Properties.Resources.ITOPERATOR)
            .FirstOrDefault() != null;
    }
}
