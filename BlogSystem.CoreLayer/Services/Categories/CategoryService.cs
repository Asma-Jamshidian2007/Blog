using Blog_System.CoreLayer.DTOs.Categories;
using Blog_System.CoreLayer.Mappers;
using Blog_System.CoreLayer.Utilities;
using Blog_System.CoreLayer.Utilities.OperationResult;
using Blog_System.DataLayer.Context;
using Blog_System.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations;


namespace Blog_System.CoreLayer.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly BlogContext _context;

        public CategoryService(BlogContext context)
        {
            _context = context;
        }

        public OperationResult CreateCategory(CreateCategoryDto createto)
        {
            if (IsSlugExist(createto.Slug))
                return OperationResult.Error("Slug is exist!");

            if (string.IsNullOrEmpty(createto.Title) || string.IsNullOrEmpty(createto.Slug))
                return OperationResult.Error("Title and Slug cannot be empty.");

            var category = new Category
            {
                Title = createto.Title,
                IsDelete = false,
                ParentId = createto.ParentId,
                MetaDescription = createto.MetaDescription ?? "",
                MetaTag = createto.MetaTag,
                Slug = createto.Slug.ToSlug()
            };

            if (_context.Categories.Any(c => c.Slug == createto.Slug))
                return OperationResult.Error("Slug must be unique.");

            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return OperationResult.Success("Category Created");
            }
            catch (DbUpdateException ex)
            {
                return OperationResult.Error($"Database update failed: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public OperationResult EditCategory(EditCategoryDto editDto)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Id == editDto.Id);
            if (category == null)
                return OperationResult.NotFound("Category not found.");

            if (editDto.Slug.ToSlug() != category.Slug && IsSlugExist(editDto.Slug))
                return OperationResult.Error("Slug is exist!");

            if (string.IsNullOrEmpty(editDto.Title) || string.IsNullOrEmpty(editDto.Slug))
                return OperationResult.Error("Title and Slug cannot be empty.");

            if (_context.Categories.Any(c => c.Slug == editDto.Slug && c.Id != editDto.Id))
                return OperationResult.Error("Slug must be unique.");

            category.MetaDescription = editDto.MetaDescription ?? "";
            category.Slug = editDto.Slug.ToSlug();
            category.Title = editDto.Title;
            category.MetaTag = editDto.MetaTag;

            try
            {
                _context.SaveChanges();
                return OperationResult.Success("Category edited");
            }
            catch (DbUpdateException ex)
            {
                return OperationResult.Error($"Database update failed: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public OperationResult DeleteCategory(int id)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null)
                return OperationResult.NotFound("Category not found.");

            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return OperationResult.Success("Category permanently deleted.");
            }
            catch (DbUpdateException ex)
            {
                return OperationResult.Error($"Database update failed: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public List<CategoryDto> GetCategoryBy()
        {
            return _context.Categories.Select(CategoryMapper.Map).ToList();
        }

        public CategoryDto? GetCategoryBy(string slug)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Slug == slug);
            return category == null ? null : CategoryMapper.Map(category);
        }

        public CategoryDto? GetCategoryById(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            return category == null ? null : CategoryMapper.Map(category);
        }

        public bool IsSlugExist(string slug)
        {
            return _context.Categories.Any(c => c.Slug == slug.ToSlug());
        }

        public List<CategoryDto> GetAll()
        {
            return _context.Categories
                           .Where(c => !c.IsDelete)
                           .Select(CategoryMapper.Map)
                           .ToList();
        }

        public CategoryDto? GetCategoryBy(int id)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Id == id && !c.IsDelete);
            return category == null ? null : CategoryMapper.Map(category);
        }
    }
}
