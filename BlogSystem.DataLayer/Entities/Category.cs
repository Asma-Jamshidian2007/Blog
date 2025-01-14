using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog_System.DataLayer.Entities
{
    // Represents a Category entity in the blog system
    public class Category : BaseEntity
    {
        // Title of the category (required for identification)
        [Required]
        public string Title { get; set; } = string.Empty;

        // Slug for URL-friendly version of the title (required for routing)
        [Required]
        public string Slug { get; set; } = string.Empty;

        // Meta tag to define keywords for SEO (optional)
        public string MetaTag { get; set; } = string.Empty;

        // Meta description to describe the category (optional, for SEO)
        public string MetaDescription { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        #region Relationships


        // Required collection of related posts (A category can have multiple posts)
        public ICollection<Post> Posts { get; set; }

        #endregion
    }
}
