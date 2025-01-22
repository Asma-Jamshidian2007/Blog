using Blog_System.CoreLayer.DTOs.Posts;
using Blog_System.CoreLayer.Mappers;
using Blog_System.CoreLayer.Utilities;
using Blog_System.CoreLayer.Utilities.OperationResult;
using Blog_System.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Blog_System.CoreLayer.Services.Posts
{
    public interface IPostService
    {
        OperationResult CreatePost(CreatePostDto postDto);
        OperationResult EditPost(EditPostDto postDto);
        PostDto GetPostById(int id);
        PostFilterDto GetPostByFilter(PostFilterParams filterParams);
        bool IsSlugExist(string slug);
    }

    public class PostService : IPostService
    {
        private readonly BlogContext _context;

        public PostService(BlogContext context)
        {
            _context = context;
        }

        public OperationResult CreatePost(CreatePostDto createPostDto)
        {
            if (IsSlugExist(createPostDto.Slug))
                return OperationResult.Error("Slug already exists.");

            var post = PostMapper.ToPostEntity(createPostDto);
            _context.Posts.Add(post);
            _context.SaveChanges();
            return OperationResult.Success("Post created successfully.");
        }

        public OperationResult EditPost(EditPostDto editPostDto)
        {
            var post = _context.Posts.SingleOrDefault(p => p.Id == editPostDto.PostId);
            if (post == null)
                return OperationResult.NotFound("Post not found.");

            if (IsSlugExist(editPostDto.Slug) && editPostDto.Slug != post.Slug)
                return OperationResult.Error("Slug already exists.");

            post = PostMapper.ToUpdatedPostEntity(editPostDto, post);
            _context.Posts.Update(post);
            _context.SaveChanges();
            return OperationResult.Success("Post updated successfully.");
        }

        public PostDto GetPostById(int id)
        {
            var post = _context.Posts.SingleOrDefault(p => p.Id == id);
            return PostMapper.ToPostDto(post);
        }

        public bool IsSlugExist(string slug)
        {
            return _context.Posts.Any(p => p.Slug == slug.ToSlug());
        }

        PostFilterDto IPostService.GetPostByFilter(PostFilterParams filterParams)
        {
            var result =_context.Posts.OrderByDescending(d => d.CreationDate).AsQueryable();
            if (string.IsNullOrWhiteSpace(filterParams.CategorySlug))
                result = result.Where(p => p.Category.Slug==filterParams.CategorySlug);
            if (!string.IsNullOrWhiteSpace(filterParams.Title))
                result = result.Where(p => p.Title.Contains(filterParams.Title));
            var skip = (filterParams.PageId - 1) * filterParams.Take;
            var pageCount=result.Count() / filterParams.Take;

            return new PostFilterDto()
            {
                Posts = result.Skip(skip).Take(filterParams.Take).Select(post => PostMapper.ToPostDto(post)).ToList(),
                filterParams = filterParams,
                PageCount = pageCount
            };
        }
    }
}
