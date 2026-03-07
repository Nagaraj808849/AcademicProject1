using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.BusinessLayer;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly BLLogin _blLogin;

        public LoginController(BLLogin blLogin)
        {
            _blLogin = blLogin;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginClass login)
        {
            try
            {
                // Check if request body is empty
                if (login == null)
                {
                    return BadRequest("Login data is required.");
                }

                // Validate email and password
                if (string.IsNullOrEmpty(login.EmailId) || string.IsNullOrEmpty(login.Password))
                {
                    return BadRequest("Email and Password are required.");
                }

                var user = _blLogin.ValidateLogin(login);

                if (user == null)
                {
                    return Unauthorized("Invalid Email or Password");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}