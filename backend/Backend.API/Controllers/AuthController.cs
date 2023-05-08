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
        //endpoint to create get a token for a user.
        [AllowAnonymous, HttpPost("login/")]
        public object Authenticate(UserLoginData data, [FromServices]AuthTokenService service, [FromServices] IUsersInformation getterService)
        {
            // create a new token with token helper and add our claim
            // from `Westwind.AspNetCore`  NuGet Package
            var token = service.GenerateToken(data, getterService.Users);

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