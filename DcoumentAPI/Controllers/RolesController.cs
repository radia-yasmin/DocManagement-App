using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DcoumentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            return Ok(await _roleManager.Roles.ToListAsync());
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Role name cannot be empty");
            }

            // Check if the role doesn't exist, then create it
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var newRole = new IdentityRole(roleName);
                await _roleManager.CreateAsync(newRole);

                return Ok($"Role '{roleName}' created successfully");
            }
            else
            {
                return BadRequest($"Role '{roleName}' already exists");
            }
        }
    }
}
