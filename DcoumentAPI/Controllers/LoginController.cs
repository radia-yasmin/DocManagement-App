using DcoumentAPI.Domain.Dtos;
using DcoumentAPI.Domain.EntityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DcoumentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;

        public LoginController(SignInManager<Users> signInManager, UserManager<Users> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.UserName);

            if (user == null)
            {
                return BadRequest($"User with {model.UserName} doesn't exist.");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // You can customize the response as needed
                var roles = await _userManager.GetRolesAsync(user);
                var userData = new { Message = "Login successful!", Name = $"{user.FirstName} {user.LastName}", Email = user.Email, UserId = user.Id, UserRole=roles.FirstOrDefault() };
                return Ok(userData);
            }

            if (result.IsLockedOut)
            {
                return BadRequest("User account locked out.");
            }
            else
            {
                return BadRequest("Your Password is incorrect.");
            }
        }
    }
}
