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
                return OperationResult.Error("عنوان و اسلاگ نمی‌توانند خالی باشند.");

            var slug = createto.Slug.ToSlug();
            if (IsSlugExist(slug))
                return OperationResult.Error("اسلاگ تکراری است.");

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
                return OperationResult.Success("دسته‌بندی با موفقیت ایجاد شد.");
            }
            catch (DbUpdateException ex)
            {
                return OperationResult.Error($"خطا در به‌روزرسانی پایگاه داده: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public OperationResult EditCategory(EditCategoryDto editDto)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Id == editDto.Id);
            if (category == null)
                return OperationResult.NotFound("دسته‌بندی یافت نشد.");

            var slug = editDto.Slug.ToSlug();
            if (slug != category.Slug && IsSlugExist(slug))
                return OperationResult.Error("اسلاگ تکراری است.");

            if (string.IsNullOrEmpty(editDto.Title) || string.IsNullOrEmpty(slug))
                return OperationResult.Error("عنوان و اسلاگ نمی‌توانند خالی باشند.");

            if (_context.Categories.Any(c => c.Slug == slug && c.Id != editDto.Id))
                return OperationResult.Error("اسلاگ باید یکتا باشد.");

            category.MetaDescription = editDto.MetaDescription ?? string.Empty;
            category.Slug = slug;
            category.Title = editDto.Title;
            category.MetaTag = editDto.MetaTag;

            try
            {
                _context.SaveChanges();
                return OperationResult.Success("دسته‌بندی با موفقیت ویرایش شد.");
            }
            catch (DbUpdateException ex)
            {
                return OperationResult.Error($"خطا در به‌روزرسانی پایگاه داده: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public OperationResult DeleteCategory(int id)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null)
                return OperationResult.NotFound("دسته‌بندی یافت نشد.");

            var childCategories = _context.Categories.Where(c => c.ParentId == id).ToList();
            if (childCategories.Any())
                return OperationResult.Error("این دسته‌بندی دارای زیر دسته‌ها می‌باشد. ابتدا زیر دسته‌ها را حذف کنید.");

            var posts = _context.Posts.Where(p => p.CategoryId == id).ToList();
            if (posts.Any())
                return OperationResult.Error("این دسته‌بندی دارای پست‌های مرتبط می‌باشد. ابتدا پست‌ها را حذف یا جابه‌جا کنید.");

            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return OperationResult.Success("دسته‌بندی با موفقیت حذف شد.");
            }
            catch (DbUpdateException ex)
            {
                return OperationResult.Error($"خطا در به‌روزرسانی پایگاه داده: {ex.InnerException?.Message ?? ex.Message}");
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
            return _context.Categories.Select(category => CategoryMapper.Map(category)).ToList();
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
