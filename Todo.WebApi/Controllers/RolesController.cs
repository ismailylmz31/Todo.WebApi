using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Todo.Service.Abstract;

namespace Todo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddRole(string roleName)
        {
            var result = await _rolesService.AddRoleAsync(roleName);
            return StatusCode(result.Status, result);
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignRoleToUser(string userId, string roleName)
        {
            var result = await _rolesService.AssignRoleToUserAsync(userId, roleName);
            return StatusCode(result.Status, result);
        }

        [HttpGet("list")]
        public IActionResult ListRoles()
        {
            var result = _rolesService.ListRoles();
            return StatusCode(result.Status, result);
        }
    }
}
