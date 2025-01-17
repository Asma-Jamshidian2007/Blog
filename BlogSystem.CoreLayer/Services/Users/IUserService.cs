using Blog_System.CoreLayer.DTOs.Users;
using Blog_System.CoreLayer.Utilities.OperationResult;
using Blog_System.CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Blog_System.CoreLayer.Services.Users
{
    public interface IUserService
    {
        OperationResult UserRegister(UserRegisterDto registerDto);
        UserDto UserLogin(LoginUserDto loginDto);
    }
}
