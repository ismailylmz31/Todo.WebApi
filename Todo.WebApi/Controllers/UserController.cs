using Microsoft.AspNetCore.Mvc;
using Todo.Models.Users;
using Todo.Service.Abstract;

namespace Todo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService _userService, IAuthenticationService _authenticationService) : ControllerBase
    {


        [HttpPost("creeate")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterRequestDto dto)
        {
            var result = await _authenticationService.RegisterByTokenAsync(dto);

            return Ok(result);
        }


        //[HttpGet("getbyemail")]
        //public async Task<IActionResult> GetByEmail([FromQuery] string email)
        //{
        //    var result = await _userService.GetByEmailAsync(email);
        //    return Ok(result);
        //}

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var result = await _authenticationService.LoginByTokenAsync(dto);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            var result = await _userService.DeleteAsync(id);
            return Ok(result);
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromQuery] string id, [FromBody] UpdateRequestDto dto)
        {
            var result = await _userService.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpPut("changepassword")]
        public async Task<IActionResult> ChangePassword(string id, ChangePasswordRequestDto dto)
        {
            var result = await _userService.ChangePasswordAsync(id, dto);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            var result = await _userService.CreateUserAsync(dto);
            return StatusCode(result.Status, result);
        }

        [HttpGet("getbyemail")]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            var result = await _userService.GetUserByEmailAsync(email);
            return StatusCode(result.Status, result);
        }

        [HttpPost("addrole")]
        public async Task<IActionResult> AddRole([FromQuery] string email, [FromQuery] string roleName)
        {
            var result = await _userService.AddUserToRoleAsync(email, roleName);
            return StatusCode(result.Status, result);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetUserRoles([FromQuery] string email)
        {
            var result = await _userService.GetUserRolesAsync(email);
            return StatusCode(result.Status, result);
        }

    }
}