using Blog_System.CoreLayer.DTOs.Users;
using Blog_System.CoreLayer.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace Blog_System.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var users = _userService.GetAllUsers();

            ViewData["Users"] = users;

            return View();
        }
    }
}
