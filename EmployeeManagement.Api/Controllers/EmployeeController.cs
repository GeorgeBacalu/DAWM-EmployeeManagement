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

        [HttpGet] public IActionResult GetAll()
        {
            IList<EmployeeDto> result = _employeeService.GetAll();
            return Ok(new { loggerInUserId = GetUserId(), employees = result });
        }

        [HttpGet("{id}")] public IActionResult GetById(int id)
        {
            EmployeeDto result = _employeeService.GetById(id);
            return Ok(new { loggerInUserId = GetUserId(), employee = result });
        }

        [HttpPost] public IActionResult Add(EmployeeDto employeeDto)
        {
            EmployeeDto addedEmployeeDto = _employeeService.Add(employeeDto);
            return CreatedAtAction(nameof(GetById), new { id = addedEmployeeDto.Id }, addedEmployeeDto);
        }

        [HttpPut("{id}")] public IActionResult UpdateById(EmployeeDto employeeDto, int id)
        {
            EmployeeDto result = _employeeService.UpdateById(employeeDto, id);
            return Ok(new { loggerInUserId = GetUserId(), updatedEmployee = result });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DisableById(int id)
        {
            EmployeeDto result = _employeeService.DisableById(id);
            return Ok(new { loggerInUserId = GetUserId(), disabledEmployee = result });
        }
    }
}