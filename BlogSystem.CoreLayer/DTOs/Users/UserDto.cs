using Blog_System.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_System.CoreLayer.DTOs.Users
{
    public class UserDto
    {
        // Unique identifier for the user
        public int UserId { get; set; }

        // Username of the user
        public string UserName { get; set; } = string.Empty;

        // Full name of the user
        public string FullName { get; set; } = string.Empty;

        // Hashed password of the user
        public string Password { get; set; } = string.Empty;

        // Date of user registration
        public DateTime RegisterDate { get; set; } 

        // Role of the user in the system (e.g., Admin, User)
        public UserRole Role { get; set; }
    }
}
