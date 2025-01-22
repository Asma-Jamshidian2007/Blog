using Blog_System.CoreLayer.DTOs.Posts;
using Blog_System.DataLayer.Entities;

namespace Blog_System.CoreLayer.Mappers
{
    public static class PostMapper
    {
        public static Post ToPostEntity(CreatePostDto createDto)
        {
            return new Post
            {
                Title = createDto.Title,
                Slug = createDto.Slug ?? string.Empty,  
                Description = createDto.Description ?? string.Empty,  
                CategoryId = createDto.CategoryId,
                UserId = createDto.UserId,
                Visit = 0,
                IsDelete = false
            };
        }

       
        public static Post ToUpdatedPostEntity(EditPostDto editDto, Post existingPost)
        {
            existingPost.Title = editDto.Title;
            existingPost.Slug = editDto.Slug ?? string.Empty;  
            existingPost.Description = editDto.Description ?? string.Empty;  
            existingPost.CategoryId = editDto.CategoryId;
            return existingPost;
        }

        public static PostDto ToPostDto(Post post)
        {
            return new PostDto
            {
                PostId = post.Id,
                Title = post.Title,
                Slug = post.Slug,
                Description = post.Description,
                CategoryId = post.CategoryId,
                UserId = post.UserId,
                Visit = post.Visit,
                CreationDate = post.CreationDate,
                Category = post.Category,
                ImageName = post.ImageName
            };
        }
    }
}
