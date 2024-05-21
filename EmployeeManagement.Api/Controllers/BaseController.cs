using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace EmployeeManagement.Api.Controllers
{
    public class BaseController : Controller
    {
        public BaseController() { }

        protected int GetUserId()
        {
            string? rawToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            string? token = rawToken?.Substring("Bearer ".Length).Trim();
            var parserToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            string? rawUserId = parserToken.Claims.First(claim => claim.Type == "userId").Value;
            int userId = Int32.Parse(rawUserId);
            return userId;
        }
    }
}