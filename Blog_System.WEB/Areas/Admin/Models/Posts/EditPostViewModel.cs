using System.ComponentModel.DataAnnotations;

namespace Blog_System.WEB.Areas.Admin.Models.Posts
{

        public class EditPostViewModel
        {
        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Category required")]
        public int CategoryId { get; set; }

        [Display(Name = "Select a subcategory")]
        public int? SubCategoryId { get; set; }

        [Display(Name = "title")]
        [Required(ErrorMessage = "title required")]
        [StringLength(200, ErrorMessage = "title must not have upper than 200 charecters")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        [UIHint("CKeditor")]
        [StringLength(5000, ErrorMessage = "Description must not have upper than 200 charecters")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Slug")]
        [Required(ErrorMessage = "slug is required")]
        [StringLength(100, ErrorMessage = "Description must not have upper than 200 charecters")]
        [RegularExpression(@"^[a-z0-9]+(?:-[a-z0-9]+)*$", ErrorMessage = "Slug should only contain small letters and number and use the dash(-)to separate sections")]
        public string Slug { get; set; } = string.Empty;
        [Display(Name = "photo")]
        public IFormFile? ImageFile { get; set; }
        public int PostId { get; internal set; }
    }

    }

