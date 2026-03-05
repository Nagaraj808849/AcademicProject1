using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.BusinessLayer;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly BLRegistration _blRegistration;

        public RegistrationController(BLRegistration blRegistration)
        {
            _blRegistration = blRegistration;
        }

        [HttpPost("InsertRegisters")]
        public IActionResult Register([FromBody] RegistrationClass registration)
        {
            try
            {
                // Check if body is null
                if (registration == null)
                {
                    return BadRequest(new { message = "Invalid request data." });
                }

                // Validate required fields
                if (string.IsNullOrWhiteSpace(registration.FirstName) ||
                    string.IsNullOrWhiteSpace(registration.LastName) ||
                    string.IsNullOrWhiteSpace(registration.EmailId) ||
                    string.IsNullOrWhiteSpace(registration.Password))
                {
                    return BadRequest(new { message = "All fields are required." });
                }

                // Call Business Layer
                bool result = _blRegistration.RegisterValues(registration);

                if (result)
                {
                    return Ok(new
                    {
                        message = "User Registered Successfully"
                    });
                }

                return BadRequest(new
                {
                    message = "Registration Failed"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Server Error",
                    error = ex.Message
                });
            }
        }
    }
}