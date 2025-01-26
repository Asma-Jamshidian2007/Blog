using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_System.DataLayer.Entities
{
    public class Post : BaseEntity
    {
        public int UserId { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Slug { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;

        public int Visit { get; set; }

        #region Relationships

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        [ForeignKey("SubCategoryId")]
        public Category? SubCategory { get; set; }

        public ICollection<PostComments>? PostComment { get; set; }

        #endregion
    }
}
