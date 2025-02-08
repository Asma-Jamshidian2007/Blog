using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Blog_System.WEB.Areas.Admin.Models.Posts
{
    public class CreatePostViewModel
    {
        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Category required")]
        public int CategoryId { get; set; } = 0;

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
        [Required(ErrorMessage = "slug is required")]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = "Just JPG, PNG, JPEG are valid.")]
        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "File size must not be more than 5 MB")]
        public IFormFile ImageFile { get; set; }
    }

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly long _maxSize;

        public MaxFileSizeAttribute(long maxSize) => _maxSize = maxSize;

        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            return file != null && file.Length > _maxSize
                ? new ValidationResult(ErrorMessage)
                : ValidationResult.Success;
        }
    }

    public partial class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions) => _extensions = extensions;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (!_extensions.Contains(extension))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }

    }
}
