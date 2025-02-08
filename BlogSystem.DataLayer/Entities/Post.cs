using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_System.DataLayer.Entities
{
    public class Post : BaseEntity
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The title should not exceed 200 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(5000, ErrorMessage = "Descriptions should not exceed 5,000 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "Slug It should not be more than 100 characters.")]
        public string Slug { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Image names should not exceed 255 characters.")]
        public string ImageName { get; set; } = string.Empty;

        public int Visit { get; set; } = 0; 
        #region Relationships

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        [ForeignKey("SubCategoryId")]
        public virtual Category? SubCategory { get; set; }

        public virtual ICollection<PostComments>? PostComment { get; set; }
        public string ImageFile { get; set; }

        #endregion
    }
}
