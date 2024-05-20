using EmployeeManagement.Core.Services;
using EmployeeManagement.Database.Dtos.Common;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService) => _userService = userService;

        [HttpGet] public IActionResult GetAll() => Ok(_userService.GetAll());

        [HttpGet("{id}")] public IActionResult GetById(int id) => Ok(_userService.GetById(id));

        [HttpPost] public IActionResult Add(UserDto userDto)
        {
            UserDto savedUserDto = _userService.Add(userDto);
            return CreatedAtAction(nameof(GetById), new { id = savedUserDto.Id }, savedUserDto);
        }

        [HttpPut("{id}")] public IActionResult UpdateById(UserDto userDto, int id) => Ok(_userService.UpdateById(userDto, id));

        [HttpDelete("{id}")] public IActionResult DisableById(int id) => Ok(_userService.DisableById(id));
    }
}