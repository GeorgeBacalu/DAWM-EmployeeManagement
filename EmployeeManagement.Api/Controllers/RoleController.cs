using EmployeeManagement.Core.Services;
using EmployeeManagement.Database.Dtos.Common;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService) => _roleService = roleService;

        [HttpGet] public IActionResult GetAll() => Ok(_roleService.GetAll());

        [HttpGet("{id}")] public IActionResult GetById(int id) => Ok(_roleService.GetById(id));

        [HttpPost] public IActionResult Add(RoleDto roleDto)
        {
            RoleDto savedRoleDto = _roleService.Add(roleDto);
            return CreatedAtAction(nameof(GetById), new { id = savedRoleDto.Id }, savedRoleDto);
        }
    }
}