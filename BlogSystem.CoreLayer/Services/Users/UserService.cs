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
            // Hash the provided password using MD5
            var hashedPassword = loginDto.Password.EncodeToMd5();

            // Check if a user with the given username and hashed password exists
            var user = _context.Users.FirstOrDefault(u => u.UserName == loginDto.UserName && u.Password == hashedPassword);

            // Return null if user does not exist
            if (user == null)
            {
                return null;
            }

            // Map user entity to UserDto
            var userDto = new UserDto()
            {
                UserName = user.UserName,
                Password = user.Password,
                FullName = user.FullName,
                Role = user.Role,
                RegisterDate = user.CreationDate,
                UserId = user.Id
            };

            return userDto;
        }

        // Handles user registration logic
        public OperationResult UserRegister(UserRegisterDto registerDto)
        {
            // Check if the username already exists in the database
            var isUserNameExist = _context.Users.Any(u => u.UserName == registerDto.UserName);

            if (isUserNameExist)
            {
                return OperationResult.Error("نام کاربری تکراری است");
            }

            // Hash the user's password using MD5
            var hashedPassword = registerDto.Password.EncodeToMd5();

            // Add the new user to the database
            _context.Users.Add(new User()
            {
                FullName = registerDto.FullName,
                UserName = registerDto.UserName,
                IsDelete = false,
                Role = UserRole.User,
                CreationDate = DateTime.Now,
                Password = hashedPassword
            });

            // Save changes to the database
            _context.SaveChanges();

            return OperationResult.Success();
        }
    }
}
