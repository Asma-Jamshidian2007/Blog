using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Blog_System.CoreLayer.DTOs.Posts
{
    public class EditPostDto: CreatePostDto
    {
        public int PostId { get; set; }
    }
}
