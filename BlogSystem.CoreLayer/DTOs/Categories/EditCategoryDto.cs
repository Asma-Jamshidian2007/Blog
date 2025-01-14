using System.ComponentModel.DataAnnotations;

namespace Blog_System.CoreLayer.DTOs.Categories
{
    public class EditCategoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string MetaTag { get; set; } = string.Empty;
        public string MetaDescription { get;  set; } = string.Empty;
    }
}

