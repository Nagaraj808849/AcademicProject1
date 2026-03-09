using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.BusinessLayer;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Controllers
{
    [ApiController]
    [Route("api/Menu")]
    public class MenuController : ControllerBase
    {
        private readonly BLMenu _blMenu;

        // Inject IConfiguration
        public MenuController(IConfiguration configuration)
        {
            _blMenu = new BLMenu(configuration);
        }

        // ==============================
        // GET ALL MENU ITEMS
        // ==============================
        [HttpGet]
        public IActionResult GetMenu()
        {
            var menu = _blMenu.GetMenu();
            return Ok(menu);
        }

        // ==============================
        // ADD MENU ITEM
        // ==============================
        [HttpPost("AddMenuItem")]
        public IActionResult AddMenuItem([FromBody] MenuClass MenuNew)
        {
            if (MenuNew.image == null || MenuNew.image.Length == 0)
            {
                return BadRequest("No image uploaded.");
            }

            var result = _blMenu.AddMenuItem(MenuNew);

            return Ok(new { message = result });
        }

        // ==============================
        // UPDATE MENU ITEM
        // ==============================
        [HttpPut("UpdateMenuItem/{menuId}")]
        public IActionResult UpdateMenuItem(int menuId, [FromBody] MenuClass menu)
        {
            if (menu == null)
            {
                return BadRequest("Invalid data.");
            }

            var result = _blMenu.UpdateMenuItem(menuId, menu);

            if (result)
            {
                return Ok(new { message = "Menu item updated successfully" });
            }

            return BadRequest(new { message = "Update failed" });
        }

        // ==============================
        // DELETE MENU ITEM
        // ==============================
        [HttpDelete("DeleteMenuItem/{menuId}")]
        public IActionResult DeleteMenuItem(int menuId)
        {
            var result = _blMenu.DeleteMenuItem(menuId);

            if (result)
            {
                return Ok(new { message = "Menu item deleted successfully" });
            }

            return NotFound(new { message = "Menu item not found" });
        }
    }
}