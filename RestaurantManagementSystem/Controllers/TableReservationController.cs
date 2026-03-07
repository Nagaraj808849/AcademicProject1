using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.BusinessLayer;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableReservationController : ControllerBase
    {
        private readonly BLTableReservation _blReservation;

        // Dependency Injection
        public TableReservationController(BLTableReservation blReservation)
        {
            _blReservation = blReservation;
        }

        [HttpPost("BookTable")]
        public IActionResult BookTable([FromBody] TableResevationClass reservation)
        {
            if (reservation == null)
            {
                return BadRequest("Invalid Data");
            }

            bool result = _blReservation.InsertReservation(reservation);

            if (result)
            {
                return Ok(new
                {
                    message = "Reservation Successful"
                });
            }

            return BadRequest(new
            {
                message = "Reservation Failed"
            });
        }
    }
}