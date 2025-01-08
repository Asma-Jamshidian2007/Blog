using Blog_System.CoreLayer.DTOs.Users;
using Blog_System.CoreLayer.Utilities.OperationResult;
using Blog_System.CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_System.CoreLayer.Services.Users
{
    public interface IUserService
    {
        // Register a new user
        OperationResult UserRegister(UserRegisterDto registerDto);

        // Login an existing user
        UserDto UserLogin(LoginUserDto loginDto);
    }
}
