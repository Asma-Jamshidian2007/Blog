using Blog_System.CoreLayer.DTOs.Categories;
using Blog_System.CoreLayer.Utilities.OperationResult;
using System.Collections.Generic;

namespace Blog_System.CoreLayer.Services.Categories
{
    public interface ICategoryService
    {
        OperationResult CreateCategory(CreateCategoryDto createDto);
        OperationResult EditCategory(EditCategoryDto editDto);
        CategoryDto? GetCategoryBy(int id);
        CategoryDto? GetCategoryBy(string slug);
        List<CategoryDto> GetAll();
        List<CategoryDto> GetChildCategories(int parentId);
        bool IsSlugExist(string slug);
        OperationResult DeleteCategory(int id);
    }
}
