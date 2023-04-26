using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace Backend.API.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("api/getData")]
        public object GetData()
        {
            var identity = (ClaimsIdentity)User.Identity!;
            return Ok($"Hello {identity.Name}");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("api/getDataAdmin")]
        public object GetDataForAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity!;
            var role = identity.Claims
                        .Where(claim => claim.Type == ClaimTypes.Role)
                        .Select(claim => claim.Value)
            .First();

            return Ok($"Hello {identity.Name} Role: {role}.");
        }

        [Authorize(Roles = "user")]
        [HttpGet]
        [Route("api/getDataUser")]
        public object GetDataForUser()
        {
            var identity = (ClaimsIdentity)User.Identity!;
            var role = identity.Claims
                        .Where(claim => claim.Type == ClaimTypes.Role)
                        .Select(claim => claim.Value)
                        .First();

            return Ok($"Hello {identity.Name} Role: {role}.");
        }

        [Authorize(Roles = "it operator")]
        [HttpGet]
        [Route("api/getDataIt")]
        public object GetDataForIt()
        {
            var identity = (ClaimsIdentity)User.Identity!;
            var role = identity.Claims
                        .Where(claim => claim.Type == ClaimTypes.Role)
                        .Select(claim => claim.Value)
                        .First();

            return Ok($"Hello {identity.Name} Role: {role}.");
        }
    }
}
