using Blog_System.CoreLayer.Services.Posts;
using Microsoft.AspNetCore.Mvc;
using Blog_System.CoreLayer.DTOs.Posts;

namespace Blog_System.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        public IActionResult Index(int pageId = 1,string title = "",string categorySlug = "")
        {
            var param= new PostFilterParams() {
                CategorySlug = categorySlug,
                Title = title,
                PageId = pageId,
                Take = 20 
            };
            var model = _postService.GetPostByFilter(param);
            return View(model);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(string s)
        {
            return View();
        }
    }
}
