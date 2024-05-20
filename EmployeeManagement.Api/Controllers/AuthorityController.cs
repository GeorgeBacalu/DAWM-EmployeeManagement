using EmployeeManagement.Core.Services;
using EmployeeManagement.Database.Dtos.Common;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorityController : ControllerBase
    {
        private readonly AuthorityService _authorityService;

        public AuthorityController(AuthorityService authorityService) => _authorityService = authorityService;

        [HttpGet] public IActionResult GetAll() => Ok(_authorityService.GetAll());

        [HttpGet("{id}")] public IActionResult GetById(int id) => Ok(_authorityService.GetById(id));

        [HttpPost] public IActionResult Add(AuthorityDto authorityDto)
        {
            AuthorityDto savedAuthorityDto = _authorityService.Add(authorityDto);
            return CreatedAtAction(nameof(GetById), new { id = savedAuthorityDto.Id }, savedAuthorityDto);
        }
    }
}