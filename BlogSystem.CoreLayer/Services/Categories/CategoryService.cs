using Blog_System.CoreLayer.DTOs.Categories;
using Blog_System.CoreLayer.Mappers;
using Blog_System.CoreLayer.Utilities;
using Blog_System.CoreLayer.Utilities.OperationResult;
using Blog_System.DataLayer.Context;
using Blog_System.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog_System.CoreLayer.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly BlogContext _context;

        public CategoryService(BlogContext context)
        {
            _context = context;
        }

        public OperationResult CreatCategory(CreateCategoryDto createto)
        {
            if (IsSlugExist(createto.Slug))
            {
                return OperationResult.Error("Slug Is exist!");
            }

            if (IsSlugExist(createto.Slug))
            {
                return OperationResult.Error($"The slug '{createto.Slug}' already exists. Please choose a different slug.");
            }
            if (string.IsNullOrEmpty(createto.Title) || string.IsNullOrEmpty(createto.Slug))
            {
                return OperationResult.Error("Title and Slug cannot be empty.");
            }

            var category = new Category()
            {
                Title = createto.Title,
                IsDelete = false,
                ParentId = createto.ParentId,
                MetaDescription = string.IsNullOrEmpty(createto.MetaDescription) ? "" : createto.MetaDescription, // اصلاح اینجا
                MetaTag = createto.MetaTag,
                Slug = createto.Slug.ToSlug()
            };

            if (_context.Categories.Any(c => c.Slug == createto.Slug))
            {
                return OperationResult.Error("Slug must be unique.");
            }

            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return OperationResult.Success();
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
            {
                return OperationResult.NotFound("Category not found.");
            }
            if (editDto.Slug.ToSlug() != category.Slug)
            {
                if (IsSlugExist(editDto.Slug))
                {
                    return OperationResult.Error("Slug is exist!");
                }
            }
            if (string.IsNullOrEmpty(editDto.Title) || string.IsNullOrEmpty(editDto.Slug))
            {
                return OperationResult.Error("Title and Slug cannot be empty.");
            }

            if (_context.Categories.Any(c => c.Slug == editDto.Slug && c.Id != editDto.Id))
            {
                return OperationResult.Error("Slug must be unique.");
            }

            category.MetaDescription = string.IsNullOrEmpty(editDto.MetaDescription) ? "" : editDto.MetaDescription; // اصلاح اینجا
            category.Slug = editDto.Slug.ToSlug();
            category.Title = editDto.Title;
            category.MetaTag = editDto.MetaTag;

            try
            {
                _context.SaveChanges();
                return OperationResult.Success();
            }
            catch (DbUpdateException ex)
            {
                return OperationResult.Error($"Database update failed: {ex.InnerException?.Message ?? ex.Message}");
            }
        }




        public List<CategoryDto> GetCategoryBy()
        {
            return _context.Categories.Select(category => CategoryMapper.Map(category)).ToList();
        }

        public CategoryDto GetCategoryBy(string slug)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Slug == slug);
            if (category == null)
            {
                return null;
            }
            return CategoryMapper.Map(category);
        }

        public CategoryDto GetCategoryBy(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return null;
            }
            return CategoryMapper.Map(category);
        }

        public bool IsSlugExist(string slug)
        {
            return _context.Categories.Any(c => c.Slug == slug.ToSlug());
        }
    }
}
