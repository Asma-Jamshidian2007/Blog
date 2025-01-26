using Blog_System.CoreLayer.DTOs.Categories;
using Blog_System.CoreLayer.Services.Categories;
using Blog_System.CoreLayer.Utilities.OperationResult;
using Blog_System.WEB.Areas.Admin.Models.Category;
using Microsoft.AspNetCore.Mvc;

namespace Blog_System.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();
            return View(categories);
        }

        [Route(template: "/Admin/Category/Add/{parentId?}")]
        public IActionResult Add(int? parentId)
        {
            if (parentId.HasValue && parentId.Value <= 0)
            {
                ModelState.AddModelError(nameof(parentId), "مقدار والد نامعتبر است.");
                return View();
            }
            return View();
        }

        [HttpPost(template: "/Admin/Category/Add/{parentId?}")]
        public IActionResult Add(int? parentId, CreateCategoryViewModel viewModel)
        {
            if (parentId.HasValue && parentId.Value <= 0)
            {
                ModelState.AddModelError(nameof(parentId), "مقدار والد نامعتبر است.");
                return View(viewModel);
            }

            viewModel.ParentId = parentId;
            var result = _categoryService.CreateCategory(viewModel.MapToDto());
            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(viewModel);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var category = _categoryService.GetCategoryBy(id);
            if (category == null)
                return RedirectToAction("Index");

            var model = new EditCategoryViewModel
            {
                Slug = category.Slug,
                MetaDescription = category.MetaDescription,
                MetaTag = category.MetaTag,
                Title = category.Title
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditCategoryViewModel editModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editModel);
            }

            var result = _categoryService.EditCategory(new EditCategoryDto
            {
                Slug = editModel.Slug,
                MetaDescription = editModel.MetaDescription,
                MetaTag = editModel.MetaTag,
                Title = editModel.Title,
                Id = id
            });

            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(editModel.Slug), result.Message);
                return View();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var result = _categoryService.DeleteCategory(id);
            if (result.Status != OperationResultStatus.Success)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("Index");
            }

            TempData["SuccessMessage"] = "دسته‌بندی با موفقیت حذف شد.";
            return RedirectToAction("Index");
        }

        public IActionResult GetChildCategories(int parentId)
        {
            var category = _categoryService.GetChildCategories(parentId);
            return new JsonResult(category);
        }
    }
}
