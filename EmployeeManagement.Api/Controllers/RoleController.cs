using EmployeeManagement.Core.Services;
using EmployeeManagement.Database.Dtos.Common;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService) => _roleService = roleService;

        [HttpGet] public IActionResult GetAll()
        {
            IList<RoleDto> result = _roleService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")] public IActionResult GetById(int id)
        {
            RoleDto result = _roleService.GetById(id);
            return Ok(result);
        }

        [HttpPost] public IActionResult Add(RoleDto roleDto)
        {
            RoleDto addedRoleDto = _roleService.Add(roleDto);
            return CreatedAtAction(nameof(GetById), new { id = addedRoleDto.Id }, addedRoleDto);
        }
    }
}