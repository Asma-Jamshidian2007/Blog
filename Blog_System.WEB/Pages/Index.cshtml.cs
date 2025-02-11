using Blog_System.CoreLayer.DTOs.Categories;
using Blog_System.CoreLayer.Services.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog_System.WEB.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public List<CategoryDto> Categories { get; set; }

        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public void OnGet()
        {
            Categories = _categoryService.GetAll();
        }
    }

}
