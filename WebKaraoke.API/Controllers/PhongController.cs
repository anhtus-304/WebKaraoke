using Microsoft.AspNetCore.Mvc;
using WebKaraoke.Business.Services;

namespace WebKaraoke.API.Controllers
{
    [ApiController]
    [Route("api/phong")] // This sets the base URL for this controller
    public class PhongController : ControllerBase
    {
        private readonly PhongService _phongService;

        public PhongController(PhongService phongService)
        {
            _phongService = phongService;
        }

        // This creates an endpoint at the URL: GET /api/phong/available
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableRooms()
        {
            var rooms = await _phongService.GetAvailableRoomsAsync();
            return Ok(rooms); // Returns a 200 OK status with the list of rooms
        }
    }
}