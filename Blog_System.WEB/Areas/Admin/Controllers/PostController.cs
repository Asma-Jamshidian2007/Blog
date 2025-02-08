using Blog_System.CoreLayer.Services.Posts;
using Microsoft.AspNetCore.Mvc;
using Blog_System.CoreLayer.DTOs.Posts;
using Blog_System.WEB.Areas.Admin.Models.Posts;
using Blog_System.CoreLayer.Utilities;
using Blog_System.CoreLayer.Utilities.OperationResult;
using System.Linq;
using Blog_System.DataLayer.Context;

namespace Blog_System.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly BlogContext _context;


        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public IActionResult Index(int pageId = 1, string title = "", string categorySlug = "")
        {
            var param = new PostFilterParams
            {
                CategorySlug = categorySlug,
                Title = title,
                PageId = pageId,
                Take = 2
            };
            var model = _postService.GetPostByFilter(param);
            return View(model);
        }

        public IActionResult Add() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CreatePostViewModel createViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = _postService.CreatePost(new CreatePostDto()
            {
                Title = createViewModel.Title,
                Description = createViewModel.Description,
                ImageFile = createViewModel.ImageFile,
                ImageName=createViewModel.ImageFile.Name,
                SubCategoryId = createViewModel.SubCategoryId == 0 ? null : createViewModel.SubCategoryId,
                Slug = createViewModel.Slug,
                CategoryId = createViewModel.CategoryId,
                UserId = User.GetUserId()

            });

            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(createViewModel.Slug), result.Message);
                return View(createViewModel);
            }

            return RedirectToAction("Index");
        }
        public IActionResult Edit() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, EditPostViewModel editViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = _postService.EditPost(new EditPostDto()
            {
                Title = editViewModel.Title,
                Description = editViewModel.Description,
                SubCategoryId = editViewModel.SubCategoryId == 0 ? null : editViewModel.SubCategoryId,
                Slug = editViewModel.Slug,
                CategoryId = editViewModel.CategoryId,
                PostId = Id

            });

            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(editViewModel.Slug), result.Message);
                return View(editViewModel);
            }

            return RedirectToAction("Index");
        }
    }
}