using DcoumentAPI.Domain.Dtos;
using DcoumentAPI.Domain.EntityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DcoumentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegistrationController(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("UserRegister")]
        public async Task<IActionResult> UserRegister([FromBody] UserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roleName = "User"; // Replace with your desired role name

            // Create the role if it doesn't exist
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var user = new Users
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Assign the user to the role
                await _userManager.AddToRoleAsync(user, roleName);

                return Ok("Registration successful!");
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("AdministrationRegister")]
        public async Task<IActionResult> AdministrationRegister([FromBody] UserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roleName = "Administration"; // Replace with your desired role name

            // Create the role if it doesn't exist
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var user = new Users
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Assign the user to the role
                await _userManager.AddToRoleAsync(user, roleName);

                return Ok("Registration successful!");
            }

            return BadRequest(result.Errors);
        }
    }
}
