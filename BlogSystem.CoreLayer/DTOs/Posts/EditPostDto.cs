namespace Blog_System.CoreLayer.DTOs.Posts
{
    public class EditPostDto
    {
        public int CategoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public int PostId { get; set; }

    }
}
