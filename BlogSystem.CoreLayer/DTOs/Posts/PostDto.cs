using Blog_System.DataLayer.Entities;

namespace Blog_System.CoreLayer.DTOs.Posts
{
    public class PostDto
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public int PostId { get; set; }
        public string? ImageName { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public Category? Category { get; set; }
        public int Visit { get; set; }
        public bool IsDelete { get; internal set; }
    }
}
