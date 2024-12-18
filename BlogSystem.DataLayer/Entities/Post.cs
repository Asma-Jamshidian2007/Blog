using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_System.DataLayer.Entities
{
    public class Post : BaseEntity
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Slug { get; set; } = string.Empty;
        public int Visit { get; set; }

        #region relations
        [ForeignKey("UserId")]
        public required User User { get; set; }
        [ForeignKey("CategoryId")]
        public required Category Category{ get; set; }
        public required ICollection<PostComments> PostComment { get; set; }
        #endregion
    }
}
