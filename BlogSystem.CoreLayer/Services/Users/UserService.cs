using Blog_System.DataLayer;
using Blog_System.CoreLayer.DTOs.Users;
using Blog_System.CoreLayer.Utilities.OperationResult;
using System;
using System.Linq;
using Blog_System.CoreLayer;
using Blog_System.DataLayer.Context;
using Blog_System.DataLayer.Entities;
using Blog_System.CoreLayer.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace Blog_System.CoreLayer.Services.Users
{
    public class UserService : IUserService
    {
        private readonly BlogContext _context;

        public UserService(BlogContext context)
        {
            _context = context;
        }

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

            var newUser = new User()
            {
                FullName = registerDto.FullName,
                UserName = registerDto.UserName,
                IsDelete = false,
                Role = UserRole.User,
                CreationDate = DateTime.Now,
                Password = hashedPassword
            };

            _context.Users.Add(newUser);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"خطا در ثبت کاربر: {ex.Message}");
            }

            return OperationResult.Success("کاربر با موفقیت ثبت شد");
        }
        public List<UserDto> GetAllUsers()
        {
            var users = _context.Users 
                .Select(user => new UserDto
                {
                    UserId = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName
                })
                .ToList();

            return users;
        }

    }
}
