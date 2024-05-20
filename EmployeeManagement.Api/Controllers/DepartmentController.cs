using EmployeeManagement.Core.Services;
using EmployeeManagement.Database.Dtos.Common;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentService _departmentService;

        public DepartmentController(DepartmentService departmentService) => _departmentService = departmentService;

        [HttpGet] public IActionResult GetAll() => Ok(_departmentService.GetAll());

        [HttpGet("{id}")] public IActionResult GetById(int id) => Ok(_departmentService.GetById(id));

        [HttpPost] public IActionResult Add(DepartmentDto departmentDto)
        {
            DepartmentDto savedDepartmentDto = _departmentService.Add(departmentDto);
            return CreatedAtAction(nameof(GetById), new { id = savedDepartmentDto.Id }, savedDepartmentDto);
        }

        [HttpPut("{id}")] public IActionResult UpdateById(DepartmentDto departmentDto, int id) => Ok(_departmentService.UpdateById(departmentDto, id));

        [HttpDelete("{id}")] public IActionResult DisableById(int id) => Ok(_departmentService.DisableById(id));
    }
}