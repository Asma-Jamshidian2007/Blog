using Blog_System.DataLayer;
using Blog_System.CoreLayer.DTOs.Users;
using Blog_System.CoreLayer.Utilities.OperationResult;
using System;
using System.Collections.Generic;
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
        private readonly ILogger<UserService> _logger;

        public UserService(BlogContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        private User? GetUserByUsername(string userName)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public UserDto? UserLogin(LoginUserDto loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.UserName) || string.IsNullOrEmpty(loginDto.Password))
            {
                return null; 
            }

            var user = GetUserByUsername(loginDto.UserName);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return null;
            }

            return new UserDto()
            {
                UserName = user.UserName,
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
                return OperationResult.Error("The information entered is not valid.");
            }

            if (_context.Users.Any(u => u.UserName == registerDto.UserName))
            {
                return OperationResult.Error("The username is duplicate.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var newUser = new User()
            {
                FullName = registerDto.FullName,
                UserName = registerDto.UserName,
                IsDelete = false,
                Role = UserRole.User,
                CreationDate = DateTime.Now,
                Password = hashedPassword,
                LastModifiedDate = DateTime.Now
            };

            _context.Users.Add(newUser);

            try
            {
                _context.SaveChanges();
                return OperationResult.Success("User successfully registered.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error occurred while saving user.");
                return OperationResult.Error("Error in user registration: " + ex.Message);
            }
        }

        public List<UserDto> GetAllUsers()
        {
            var users = _context.Users
                .Where(u => !u.IsDelete)
                .Select(user => new UserDto
                {
                    UserId = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    RegisterDate = user.CreationDate
                })
                .ToList();

            return users;
        }

        public OperationResult Delete(int userId)
        {
            if (userId <= 0)
            {
                return OperationResult.Error("The user ID is not valid.");
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return OperationResult.Error("User not found.");
            }

            try
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return OperationResult.Success("User successfully deleted.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user.");
                return OperationResult.Error("Error deleting user: " + ex.Message);
            }
        }
    }
}
