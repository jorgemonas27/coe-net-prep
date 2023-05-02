using Backend.API.Models;
using Backend.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace Backend.API.Controllers
{
    [ApiController, Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private List<UserInformation> _users = new List<UserInformation>();

        public AuthController()
        {
            UsersGetter getter = new UsersGetter(new JsonUsersInformationService());
            _users = getter.GetUsers();
        }

        //endpoint to create get a token for a user.
        [AllowAnonymous, HttpPost("login/")]
        public object Authenticate([FromBody]UserLoginData data, [FromServices]AuthTokenService service)
        {
            // create a new token with token helper and add our claim
            // from `Westwind.AspNetCore`  NuGet Package
            var token = service.GenerateToken(data, _users);

            if (token == null) 
            {
                return HttpStatusCode.Forbidden;
            }

            return new
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(token),
                expires = token?.ValidTo
            };
        }
    }
}