using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_System.DataLayer.Entities
{
    // Represents a Post entity in the blog system
    public class Post : BaseEntity
    {
        // User ID that relates to the author of the post (foreign key)
        public int UserId { get; set; }

        // Category ID that defines the category of the post (foreign key)
        public int CategoryId { get; set; }

        // Title of the post (required for identification)
        [Required]
        public string Title { get; set; } = string.Empty;

        // Description of the post (required for content)
        [Required]
        public string Description { get; set; } = string.Empty;

        // Slug for URL-friendly version of the post title (required for routing)
        [Required]
        public string Slug { get; set; } = string.Empty;
        // Number of visits the post has received (default is 0)
        public string ImageName { get; set; } = string.Empty;

        public int Visit { get; set; }

        #region Relationships

        // Foreign key relationship with the User entity (the author of the post)
        [ForeignKey("UserId")]
        public User? User { get; set; }

        // Foreign key relationship with the Category entity (the category of the post)
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        // Collection of post comments related to the post
        public ICollection<PostComments>? PostComment { get; set; }

        #endregion
    }
}
