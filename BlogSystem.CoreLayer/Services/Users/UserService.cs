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
using CodeYad_Blog.CoreLayer.Utilities;
using Blog_System.DataLayer.Entities;

namespace Blog_System.CoreLayer.Services.Users
{
    public class UserService : IUserService
    {
        private readonly BlogContext _context;
        public UserService(BlogContext context)
        {
            _context = context;
        }
        public OperationResult UserRegister(UserRegisterDto registerDto)
        {
            
            var isFullNameExist = _context.Users.Any(u => u.UserName == registerDto.UserName);
            if (isFullNameExist) {
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
