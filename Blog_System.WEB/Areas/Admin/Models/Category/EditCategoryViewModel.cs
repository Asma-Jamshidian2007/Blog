using System.ComponentModel.DataAnnotations;

namespace Blog_System.WEB.Areas.Admin.Models.Category
{
    public class EditCategoryViewModel
    {
        [Display(Name = "title")]
        [Required(ErrorMessage = "The title is essential")]
        [StringLength(100, ErrorMessage = "The title should not be mor than 100 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Slug The title is essential")]
        [StringLength(100, ErrorMessage = "The Slug should not be mor than 100 characters")]
        [RegularExpression(@"^[a-z0-9]+(?:-[a-z0-9]+)*$", ErrorMessage = "Slug should only contain small letters and number and use the dash(-)to separate sections.")]
        public string Slug { get; set; } = string.Empty;

        [StringLength(150, ErrorMessage = "The MetaTag should not be mor than 150 characters")]
        public string MetaTag { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "The MetaDescription should not be mor than 300 characters")]
        public string MetaDescription { get; set; } = string.Empty;

    }
}
