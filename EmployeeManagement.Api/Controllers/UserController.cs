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

        [HttpGet] public IActionResult GetAll() => Ok(_userService.GetAll());

        [HttpGet("{id}")] public IActionResult GetById(int id) => Ok(_userService.GetById(id));

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register(RegisterRequest payload) => Ok(_userService.Register(payload));

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(LoginRequest payload) => Ok(new { token = _userService.Login(payload) });

        [HttpPut("{id}")] public IActionResult UpdateById(UserDto userDto, int id) => Ok(_userService.UpdateById(userDto, id));

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DisableById(int id) => Ok(_userService.DisableById(id));
    }
}