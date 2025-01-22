using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_System.DataLayer.Entities
{
    public class PostComments : BaseEntity
    {
        public int UserId { get; set; }

        public int PostId { get; set; }

        [Required]
        public string Text { get; set; } = string.Empty;

        #region Relationships

        // Foreign key relationship with the Post entity (the post that the comment belongs to)
        [ForeignKey("PostId")]
        public  Post Post { get; set; } = new Post();

        #endregion
    }
}
