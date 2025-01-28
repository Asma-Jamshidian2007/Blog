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
        [StringLength(200, ErrorMessage = "عنوان نباید بیشتر از 200 کاراکتر باشد.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(5000, ErrorMessage = "توضیحات نباید بیشتر از 5000 کاراکتر باشد.")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "Slug نباید بیشتر از 100 کاراکتر باشد.")]
        public string Slug { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "نام تصویر نباید بیشتر از 255 کاراکتر باشد.")]
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

        #endregion
    }
}
