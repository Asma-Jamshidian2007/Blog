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

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CreateCategoryViewModel viewModel)
        {
            _categoryService.CreateCategory(viewModel.MapToDto());
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

            TempData["SuccessMessage"] = "Category deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
