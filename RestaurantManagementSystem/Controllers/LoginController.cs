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
        public IActionResult Login([FromBody] LoginClass model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Invalid Data");

                if (string.IsNullOrWhiteSpace(model.EmailId) ||
                    string.IsNullOrWhiteSpace(model.Password))
                {
                    return BadRequest("All fields are required");
                }

                // ✅ Call business layer
                var user = _blLogin.ValidateLogin(model);

                if (user != null)
                {
                    return Ok(user); // Login success
                }

                return Unauthorized("Invalid Email or Password");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server Error: " + ex.Message);
            }
        }
    }
}