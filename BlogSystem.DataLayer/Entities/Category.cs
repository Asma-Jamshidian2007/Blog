using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_System.DataLayer.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public  string Slug { get; set; } = string.Empty;
        public string MetaTag { get; set; } = string.Empty;
        public string MetaDescription { get; set; } = string.Empty;

        #region relations
        public required ICollection<Post> Posts { get; set; }
        #endregion
    }
}
