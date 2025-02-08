using Blog_System.CoreLayer.DTOs.Posts;
using Blog_System.CoreLayer.Utilities.OperationResult;
using System.Linq;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Blog_System.CoreLayer.Services.Posts
{
    public interface IPostService
    {
        OperationResult CreatePost(CreatePostDto postDto);
        OperationResult EditPost(EditPostDto postDto);
        PostDto GetPostById(int id);
        PostDto GetPostBySlug(string slug);

        PostFilterDto GetPostByFilter(PostFilterParams filterParams);
        bool IsSlugExist(string slug);
    }
}
