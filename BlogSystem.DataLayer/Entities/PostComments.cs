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
        [StringLength(1000, ErrorMessage = "متن کامنت نباید بیشتر از 1000 کاراکتر باشد.")]
        public string Text { get; set; } = string.Empty;

        #region Relationships

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; } = new Post();

        #endregion
    }
}
