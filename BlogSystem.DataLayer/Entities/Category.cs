using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_System.DataLayer.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(100, ErrorMessage = "عنوان دسته‌بندی نباید بیشتر از 100 کاراکتر باشد.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "Slug نباید بیشتر از 100 کاراکتر باشد.")]
        public string Slug { get; set; } = string.Empty;

        [StringLength(150, ErrorMessage = "MetaTag نباید بیشتر از 150 کاراکتر باشد.")]
        public string MetaTag { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "MetaDescription نباید بیشتر از 300 کاراکتر باشد.")]
        public string MetaDescription { get; set; } = string.Empty;

        public int? ParentId { get; set; }

        #region Relationships

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

        public virtual ICollection<Post> SubPosts { get; set; } = new List<Post>();

        #endregion
    }
}
