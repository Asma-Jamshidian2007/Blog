using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_System.DataLayer.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Slug { get; set; } = string.Empty;

        public string MetaTag { get; set; } = string.Empty;

        public string MetaDescription { get; set; } = string.Empty;

        public int? ParentId { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();

        public ICollection<Post> SubPosts { get; set; } = new List<Post>();
    }

}