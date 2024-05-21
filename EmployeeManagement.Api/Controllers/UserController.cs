using EmployeeManagement.Core.Services;
using EmployeeManagement.Database.Dtos.Common;
using EmployeeManagement.Database.Dtos.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly UserService _userService;

        public UserController(UserService userService) => _userService = userService;

        [HttpGet] public IActionResult GetAll()
        {
            IList<UserDto> result = _userService.GetAll();
            return Ok(new { loggedInUserId = GetUserId(), users = result });
        }

        [HttpGet("{id}")] public IActionResult GetById(int id)
        {
            UserDto result = _userService.GetById(id);
            return Ok(new { loggedInUserId = GetUserId(), user = result });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register(RegisterRequest payload)
        {
            UserDto? registeredUserDto = _userService.Register(payload);
            return CreatedAtAction(nameof(GetById), new { id = registeredUserDto?.Id }, registeredUserDto);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(LoginRequest payload)
        {
            string? result = _userService.Login(payload);
            return Ok(new { token = result });
        }

        [HttpPut("{id}")] public IActionResult UpdateById(UserDto userDto, int id)
        {
            UserDto result = _userService.UpdateById(userDto, id);
            return Ok(new { loggedInUserId = GetUserId(), updatedUser = result });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DisableById(int id)
        {
            UserDto result = _userService.DisableById(id);
            return Ok(new { loggedInUserId = GetUserId(), disabledUser = result });
        }
    }
}