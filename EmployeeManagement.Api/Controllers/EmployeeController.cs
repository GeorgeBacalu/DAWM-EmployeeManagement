using EmployeeManagement.Core.Services;
using EmployeeManagement.Database.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
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

        [HttpDelete("{id}")] public IActionResult DisableById(int id) => Ok(_employeeService.DisableById(id));
    }
}