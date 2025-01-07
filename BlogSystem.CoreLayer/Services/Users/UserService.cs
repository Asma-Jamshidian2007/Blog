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
        public UserService(BlogContext context)
        {
            _context = context;
        }

        public UserDto UserLogin(LoginUserDto loginDto)
        {
            var hashedPassword = loginDto.Password.EncodeToMd5();
            var User=_context.Users.FirstOrDefault(u => u.UserName==loginDto.UserName && u.Password==hashedPassword);
            if (User == null) {
                return null;
            }
            var userDto = new UserDto()
            {
                UserName = User.UserName,
                Password = User.Password,
                FullName = User.FullName,
                Role = User.Role,
                RegisterDate =User.CreationDate,
                UserId = User.Id

            };
            return userDto;
        }

        public OperationResult UserRegister(UserRegisterDto registerDto)
        {
            
            var isUserNameExist = _context.Users.Any(u => u.UserName == registerDto.UserName);
            if (isUserNameExist) {
               return OperationResult.Error("نام کاربری تکراری است");
            }
            var HashedPass=registerDto.Password.EncodeToMd5();
            _context.Users.Add(new User() {
            FullName = registerDto.FullName,
            UserName = registerDto.UserName,
            IsDelete=false,
             Role =UserRole.User,
             CreationDate = DateTime.Now,
             Password = HashedPass
            
            });
            _context.SaveChanges();
            return OperationResult.Success();   

        }
        
    }
}
