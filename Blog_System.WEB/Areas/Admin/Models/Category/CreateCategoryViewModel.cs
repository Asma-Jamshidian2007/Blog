using Blog_System.CoreLayer.DTOs.Categories;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog_System.WEB.Areas.Admin.Models.Category
{
    public class CreateCategoryViewModel
    {
        [Display(Name ="عنوان")]
        public string Title { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public string MetaTag { get; set; } = string.Empty;
        public string MetaDescription { get; set; } = string.Empty;
        public CreateCategoryDto MapToDto()
        {
            return new CreateCategoryDto() { 
            Title = Title,
            Slug = Slug,
            ParentId = ParentId,
            MetaTag = MetaTag,
            MetaDescription = MetaDescription
            };
        }
    }
}
