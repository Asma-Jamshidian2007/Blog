using Blog_System.DataLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace Blog_System.CoreLayer.DTOs.Posts
{
    public class PostDto
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "دسته‌بندی الزامی است.")]
        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "عنوان الزامی است.")]
        [StringLength(100, ErrorMessage = "عنوان نمی‌تواند بیشتر از 100 کاراکتر باشد.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "توضیحات الزامی است.")]
        [StringLength(1000, ErrorMessage = "توضیحات نمی‌تواند بیشتر از 1000 کاراکتر باشد.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Slug الزامی است.")]
        [StringLength(100, ErrorMessage = "Slug نمی‌تواند بیشتر از 100 کاراکتر باشد.")]
        public string Slug { get; set; } = string.Empty;

        public int PostId { get; set; }

        public string? ImageName { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow; 

        public Category? Category { get; set; }

        public int Visit { get; set; }

        public bool IsDelete { get; internal set; }
    }
}
