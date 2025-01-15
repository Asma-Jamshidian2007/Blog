using Blog_System.CoreLayer.DTOs.Categories;
using Blog_System.CoreLayer.Utilities;
using Blog_System.DataLayer.Entities;

namespace Blog_System.CoreLayer.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto Map(Category category) => new CategoryDto
        {
            Id = category.Id,
            Title = category.Title,
            Slug = category.Slug?.ToSlug() ?? string.Empty, 
            MetaTag = category.MetaTag,
            MetaDescription = category.MetaDescription ?? string.Empty,
            ParentId = category.ParentId
        };
    }
}
