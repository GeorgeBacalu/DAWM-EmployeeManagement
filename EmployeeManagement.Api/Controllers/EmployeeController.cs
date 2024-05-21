using EmployeeManagement.Core.Services;
using EmployeeManagement.Database.Dtos.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : BaseController
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService) => _employeeService = employeeService;

        [HttpGet] public IActionResult GetAll() => Ok(_employeeService.GetAll());

        [HttpGet("{id}")] public IActionResult GetById(int id) => Ok(_employeeService.GetById(id));

        [HttpPost] public IActionResult Add(EmployeeDto employeeDto)
        {
            EmployeeDto savedEmployeeDto = _employeeService.Add(employeeDto);
            return CreatedAtAction(nameof(GetById), new { id = savedEmployeeDto.Id }, savedEmployeeDto);
        }

        [HttpPut("{id}")] public IActionResult UpdateById(EmployeeDto employeeDto, int id) => Ok(_employeeService.UpdateById(employeeDto, id));

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DisableById(int id) => Ok(_employeeService.DisableById(id));
    }
}