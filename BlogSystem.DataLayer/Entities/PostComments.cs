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
        [StringLength(1000, ErrorMessage = "Comment text should not exceed 1000 characters.")]
        public string Text { get; set; } = string.Empty;

        #region Relationships
        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; } 
        public User User { get; set; }
        #endregion
    }

}
