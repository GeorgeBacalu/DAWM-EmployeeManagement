using EmployeeManagement.Core.Services;
using EmployeeManagement.Database.Dtos.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : BaseController
    {
        private readonly DepartmentService _departmentService;

        public DepartmentController(DepartmentService departmentService) => _departmentService = departmentService;

        [HttpGet] public IActionResult GetAll()
        {
            IList<DepartmentDto> result = _departmentService.GetAll();
            return Ok(new { loggedInUserId = GetUserId(), departments = result });
        }

        [HttpGet("{id}")] public IActionResult GetById(int id)
        {
            DepartmentDto result = _departmentService.GetById(id);
            return Ok(new { loggedInUserId = GetUserId(), department = result });
        }

        [HttpPost] public IActionResult Add(DepartmentDto departmentDto)
        {
            DepartmentDto addedDepartmentDto = _departmentService.Add(departmentDto);
            return CreatedAtAction(nameof(GetById), new { id = addedDepartmentDto.Id }, addedDepartmentDto);
        }

        [HttpPut("{id}")] public IActionResult UpdateById(DepartmentDto departmentDto, int id)
        {
            DepartmentDto result = _departmentService.UpdateById(departmentDto, id);
            return Ok(new { loggedInUserId = GetUserId(), updatedDepartment = result });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DisableById(int id)
        {
            DepartmentDto result = _departmentService.DisableById(id);
            return Ok(new { loggedInUserId = GetUserId(), disabledDepartment = result });
        }
    }
}