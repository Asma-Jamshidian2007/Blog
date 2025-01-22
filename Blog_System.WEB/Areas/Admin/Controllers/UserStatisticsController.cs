using Blog_System.DataLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog_System.WEB.Areas.Admin.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BlogContext _context;

        public UsersController(BlogContext context)
        {
            _context = context;
        }

        [HttpGet("GetRegistrationCount")]
        public async Task<IActionResult> GetRegistrationCount()
        {
            var count = await _context.Users.CountAsync();
            return Ok(count);
        }
    }
}
