using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_System.DataLayer.Entities
{
    // Represents a PostComment entity in the blog system
    public class PostComments : BaseEntity
    {
        // User ID that represents the author of the comment (foreign key)
        public int UserId { get; set; }

        // Post ID that links the comment to a specific post (foreign key)
        public int PostId { get; set; }

        // Text of the comment (required for the content of the comment)
        [Required]
        public string Text { get; set; } = string.Empty;

        #region Relationships

        // Foreign key relationship with the Post entity (the post that the comment belongs to)
        [ForeignKey("PostId")]
        public  Post Post { get; set; } = new Post();

        #endregion
    }
}
