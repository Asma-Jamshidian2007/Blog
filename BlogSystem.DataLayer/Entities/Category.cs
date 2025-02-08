using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_System.DataLayer.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(100, ErrorMessage = "The category title should not exceed 100 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = " It should not be more than 100 characters.")]
        public string Slug { get; set; } = string.Empty;

        [StringLength(150, ErrorMessage = "It should not be more than 150 characters. MetaTag ")]
        public string MetaTag { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "MetaDescription It should not be more than 300 characters.")]
        public string MetaDescription { get; set; } = string.Empty;

        public int? ParentId { get; set; }

        #region Relationships

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

        public virtual ICollection<Post> SubPosts { get; set; } = new List<Post>();

        #endregion
    }
}
