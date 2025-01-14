using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_System.CoreLayer.DTOs.Users
{
    public class LoginUserDto
    {
        // Username for login
        public string UserName { get; set; }

        // Password for login
        public string Password { get; set; } 
    }
}