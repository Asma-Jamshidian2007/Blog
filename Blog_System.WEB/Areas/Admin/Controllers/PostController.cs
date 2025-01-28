using Blog_System.CoreLayer.Services.Posts;
using Microsoft.AspNetCore.Mvc;
using Blog_System.CoreLayer.DTOs.Posts;
using Blog_System.WEB.Areas.Admin.Models.Posts;
using Blog_System.CoreLayer.Utilities;
using Blog_System.CoreLayer.Utilities.OperationResult;
using System.Linq;
using static Blog_System.WEB.Areas.Admin.Models.Posts.AllowedExtensionsAttribute;

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

        public IActionResult Index(int pageId = 1, string title = "", string categorySlug = "")
        {
            var param = new PostFilterParams
            {
                CategorySlug = categorySlug,
                Title = title,
                PageId = pageId,
                Take = 20
            };
            var model = _postService.GetPostByFilter(param);
            return View(model);
        }

        public IActionResult Add() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CreatePostViewModel createViewModel)
        {
            if (ModelState.IsValid)
            {
                if (createViewModel.CategoryId <= 0)
                {
                    ModelState.AddModelError("CategoryId", "دسته‌بندی معتبر انتخاب نشده است.");
                }

                if (createViewModel.SubCategoryId.HasValue && createViewModel.SubCategoryId.Value <= 0)
                {
                    ModelState.AddModelError("SubCategoryId", "زیر دسته‌بندی معتبر انتخاب نشده است.");
                }

                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "لطفاً تمامی فیلدها را به درستی پر کنید.";
                    return View(createViewModel);
                }

                var result = _postService.CreatePost(new EPostDto
                {
                    CategoryId = createViewModel.CategoryId,
                    Title = createViewModel.Title,
                    Description = createViewModel.Description,
                    ImageFile = createViewModel.ImageFile,
                    Slug = createViewModel.Slug,
                    SubCategoryId = createViewModel.SubCategoryId,
                    UserId = User.GetUserId()
                });

                if (result.Status != OperationResultStatus.Success)
                {
                    TempData["ErrorMessage"] = result.Message ?? "خطا در ایجاد پست. لطفاً دوباره تلاش کنید.";
                    return View(createViewModel);
                }

                TempData["SuccessMessage"] = "پست با موفقیت ایجاد شد!";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "لطفاً تمامی فیلدها را به درستی پر کنید.";
            TempData["ErrorDetails"] = string.Join(" ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return View(createViewModel);
        }
        public IActionResult Edit() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditPostViewModel editViewModel)
        {
            if (ModelState.IsValid)
            {
                if (editViewModel.CategoryId <= 0)
                {
                    ModelState.AddModelError("CategoryId", "دسته‌بندی معتبر انتخاب نشده است.");
                }

                if (editViewModel.SubCategoryId.HasValue && editViewModel.SubCategoryId.Value <= 0)
                {
                    ModelState.AddModelError("SubCategoryId", "زیر دسته‌بندی معتبر انتخاب نشده است.");
                }

                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "لطفاً تمامی فیلدها را به درستی پر کنید.";
                    return View(editViewModel);
                }

                var result = _postService.EditPost(new EditPostDto
                {
                    CategoryId = editViewModel.CategoryId,
                    Title = editViewModel.Title,
                    Description = editViewModel.Description,
                    ImageFile = editViewModel.ImageFile,
                    Slug = editViewModel.Slug,
                    SubCategoryId = editViewModel.SubCategoryId
                });

                if (result.Status != OperationResultStatus.Success)
                {
                    TempData["ErrorMessage"] = result.Message ?? "خطا در ایجاد پست. لطفاً دوباره تلاش کنید.";
                    return View(editViewModel);
                }

                TempData["SuccessMessage"] = "پست با موفقیت ایجاد شد!";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "لطفاً تمامی فیلدها را به درستی پر کنید.";
            TempData["ErrorDetails"] = string.Join(" ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return View(editViewModel);
        }
    }
}
