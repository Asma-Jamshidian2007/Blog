using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_System.CoreLayer.DTOs.Comments
{
    public class CreateCommentDto
    {
        public int UserId { get; set; }
        public int PostId { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Comment text should not exceed 1000 characters.")]
        public string Text { get; set; } = string.Empty;
    }
}
