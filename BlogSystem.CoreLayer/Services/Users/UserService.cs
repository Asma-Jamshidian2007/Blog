using Blog_System.DataLayer;
using Blog_System.CoreLayer.DTOs.Users;
using Blog_System.CoreLayer.Utilities.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog_System.CoreLayer;
using Blog_System.DataLayer.Context;
using Blog_System.DataLayer.Entities;
using Blog_System.CoreLayer.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;


namespace Blog_System.CoreLayer.Services.Users
{
    public class UserService : IUserService
    {
        private readonly BlogContext _context;

        // Constructor to inject the BlogContext dependency
        public UserService(BlogContext context)
        {
            _context = context;
        }

        // Handles user login logic
        public UserDto UserLogin(LoginUserDto loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.UserName) || string.IsNullOrEmpty(loginDto.Password))
            {
                return null;
            }

            var hashedPassword = loginDto.Password.EncodeToMd5();

            var user = _context.Users.FirstOrDefault(u => u.UserName == loginDto.UserName && u.Password == hashedPassword);

            if (user == null)
            {
                return null;
            }

            return new UserDto()
            {
                UserName = user.UserName,
                Password = hashedPassword,
                FullName = user.FullName,
                Role = user.Role,
                RegisterDate = user.CreationDate,
                UserId = user.Id
            };
        }


        public OperationResult UserRegister(UserRegisterDto registerDto)
        {
            if (registerDto == null || string.IsNullOrEmpty(registerDto.UserName) || string.IsNullOrEmpty(registerDto.Password))
            {
                return OperationResult.Error("اطلاعات وارد شده معتبر نیست");
            }

            var isUserNameExist = _context.Users.Any(u => u.UserName == registerDto.UserName);

            if (isUserNameExist)
            {
                return OperationResult.Error("نام کاربری تکراری است");
            }

            var hashedPassword = registerDto.Password.EncodeToMd5();

            _context.Users.Add(new User()
            {
                FullName = registerDto.FullName,
                UserName = registerDto.UserName,
                IsDelete = false,
                Role = UserRole.User,
                CreationDate = DateTime.Now,
                Password = hashedPassword
            });

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"خطا در ثبت کاربر: {ex.Message}");
            }

            return OperationResult.Success();
        }

    }
}
