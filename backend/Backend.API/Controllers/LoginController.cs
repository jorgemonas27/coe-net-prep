using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace Backend.API.Controllers
{
    [ApiController, Route("api/data")]
    public class LoginController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public object GetData()
        {
            var identity = (ClaimsIdentity)User.Identity!;
            return Ok($"Hello {identity.Name}");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("admin/")]
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
        [Route("user/")]
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
        [Route("it/")]
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
