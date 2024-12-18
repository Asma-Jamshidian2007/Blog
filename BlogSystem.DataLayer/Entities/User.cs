using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_System.DataLayer.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string UserName { get; set; }=string.Empty;
        public string FullName { get; set; }= string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; }

        #region relations
        public ICollection<Post> Posts { get; set; }=new List<Post>();
        #endregion
    }
    public enum UserRole
    {
        Admin,
        User,
        Author
    }
}
