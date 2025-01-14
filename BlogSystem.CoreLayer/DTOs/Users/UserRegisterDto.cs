using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_System.CoreLayer.DTOs.Users
{
    public class UserRegisterDto
    {
        // Full name of the user to be registered
        public string FullName { get; set; }

        // Username for the new user
        public string UserName { get; set; }

        // Password for the new user
        public string Password { get; set; }
    }
}
