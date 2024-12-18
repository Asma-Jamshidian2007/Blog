using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_System.DataLayer.Entities
{
    public class PostComments : BaseEntity
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        [Required]
        public string Text { get; set; } = string.Empty;

        #region relations
        [ForeignKey("PostId")]
        public required Post Post { get; set; }
        #endregion
    }
}
