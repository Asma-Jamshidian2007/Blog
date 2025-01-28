using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Blog_System.CoreLayer.DTOs.Posts
{
    public class EPostDto
    {
        [Required(ErrorMessage = "کاربر الزامی است.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "دسته‌بندی الزامی است.")]
        public int CategoryId { get; set; }

        public int? SubCategoryId { get; set; }

        [Required(ErrorMessage = "عنوان الزامی است.")]
        [StringLength(100, ErrorMessage = "عنوان نمی‌تواند بیشتر از 100 کاراکتر باشد.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "توضیحات الزامی است.")]
        [StringLength(1000, ErrorMessage = "توضیحات نمی‌تواند بیشتر از 1000 کاراکتر باشد.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Slug الزامی است.")]
        [StringLength(100, ErrorMessage = "Slug نمی‌تواند بیشتر از 100 کاراکتر باشد.")]
        public string Slug { get; set; } = string.Empty;

        [Required(ErrorMessage = "تصویر الزامی است.")]
        public IFormFile ImageFile { get; set; }
    }
}
