using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Blog_System.WEB.Areas.Admin.Models.Posts
{
    public class CreatePostViewModel
    {
        [Display(Name ="انتخاب دسته بندی")]
        [Required]
        public int CategoryId { get; set; }
        [Display(Name = "انتخاب دسته بندی")]
        public int? SubCategoryId { get; set; }
        [Display(Name = "عنوان")]
        [Required]
        public string Title { get; set; } = string.Empty;
        [Display(Name = "توضیحات")]
        [Required]
        [UIHint("CKeditor")]
        public string Description { get; set; } = string.Empty;
        [Display(Name = "Slug")]
        [Required]
        public string Slug { get; set; } = string.Empty;
        [Display(Name = "عکس")]
        [Required]
        public IFormFile ImageFile { get; set; }
    }
}
