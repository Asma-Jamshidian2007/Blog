using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Blog_System.CoreLayer.DTOs.Posts
{
    public class CreatePostDto
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "دسته‌بندی الزامی است.")]
        public int CategoryId { get; set; }

        public int? SubCategoryId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot be more than 100 charecters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, ErrorMessage = "Description cannot be more than 1000 charecters .")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Slug is required")]
        [StringLength(100, ErrorMessage = "Slug cannot be more than 100 charecters.")]
        public string Slug { get; set; } = string.Empty;
        public string ImageName { get; set; }
        [Required(ErrorMessage = "Image is required.")]
        public IFormFile ImageFile { get; set; }
    }
}
