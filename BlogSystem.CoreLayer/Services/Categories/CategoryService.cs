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
            if (string.IsNullOrEmpty(createto.Title) || string.IsNullOrEmpty(createto.Slug))
                return OperationResult.Error("The title and slug cannot be empty.");

            var slug = createto.Slug.ToSlug();
            if (IsSlugExist(slug))
                return OperationResult.Error("The slug is repetitive.");

            var category = new Category
            {
                Title = createto.Title,
                IsDelete = false,
                ParentId = createto.ParentId,
                MetaDescription = createto.MetaDescription ?? string.Empty,
                MetaTag = createto.MetaTag,
                Slug = slug,
                Id = createto.Id
                
            };

            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return OperationResult.Success("Category created successfully.");
            }
            catch (DbUpdateException ex)
            {
                return OperationResult.Error($"Error updating database: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public OperationResult EditCategory(EditCategoryDto editDto)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Id == editDto.Id);
            if (category == null)
                return OperationResult.NotFound("Category not found.");

            var slug = editDto.Slug.ToSlug();
            if (slug != category.Slug && IsSlugExist(slug))
                return OperationResult.Error("The slug is repetitive.");

            if (string.IsNullOrEmpty(editDto.Title) || string.IsNullOrEmpty(slug))
                return OperationResult.Error("Title and slug cannot be empty.");

            if (_context.Categories.Any(c => c.Slug == slug && c.Id != editDto.Id))
                return OperationResult.Error("The slug must be unique.");

            category.MetaDescription = editDto.MetaDescription ?? string.Empty;
            category.Slug = slug;
            category.Title = editDto.Title;
            category.MetaTag = editDto.MetaTag;

            try
            {
                _context.SaveChanges();
                return OperationResult.Success("Category successfully edited.");
            }
            catch (DbUpdateException ex)
            {
                return OperationResult.Error($"Error updating database: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public OperationResult DeleteCategory(int id)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null)
                return OperationResult.NotFound("Category not found.");

            var childCategories = _context.Categories.Where(c => c.ParentId == id).ToList();
            if (childCategories.Any())
                return OperationResult.Error("This category has subcategories. First, delete the subcategories.");

            var posts = _context.Posts.Where(p => p.CategoryId == id).ToList();
            if (posts.Any())
                return OperationResult.Error("This category has related posts. Please delete or move the posts first.");

            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return OperationResult.Success("Category successfully deleted.");
            }
            catch (DbUpdateException ex)
            {
                return OperationResult.Error($"Error updating database: {ex.InnerException?.Message ?? ex.Message}");
            }
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
            return _context.Categories.Select(Category => CategoryMapper.Map(Category)).ToList();
        }
        public CategoryDto? GetCategoryBy(int id)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Id == id && !c.IsDelete);
            return category == null ? null : CategoryMapper.Map(category);
        }

        public List<CategoryDto> GetChildCategories(int parentId)
        {
            return _context.Categories.Where(c => c.ParentId == parentId).Select(CategoryMapper.Map).ToList();
        }
    }
}
