using EmployeeManagement.Core.Services;
using EmployeeManagement.Database.Dtos.Common;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorityController : BaseController
    {
        private readonly AuthorityService _authorityService;

        public AuthorityController(AuthorityService authorityService) => _authorityService = authorityService;

        [HttpGet] public IActionResult GetAll()
        {
            IList<AuthorityDto> result = _authorityService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")] public IActionResult GetById(int id)
        {
            AuthorityDto result = _authorityService.GetById(id);
            return Ok(result);
        }

        [HttpPost] public IActionResult Add(AuthorityDto authorityDto)
        {
            AuthorityDto addedAuthorityDto = _authorityService.Add(authorityDto);
            return CreatedAtAction(nameof(GetById), new { id = addedAuthorityDto.Id }, addedAuthorityDto);
        }
    }
}