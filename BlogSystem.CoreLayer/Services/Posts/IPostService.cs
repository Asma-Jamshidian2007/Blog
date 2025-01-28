using Blog_System.CoreLayer.DTOs.Posts;
using Blog_System.CoreLayer.Mappers;
using Blog_System.CoreLayer.Services.FileManager;
using Blog_System.CoreLayer.Utilities;
using Blog_System.CoreLayer.Utilities.OperationResult;
using Blog_System.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace Blog_System.CoreLayer.Services.Posts
{
    public interface IPostService
    {
        OperationResult CreatePost(EPostDto postDto);
        OperationResult EditPost(EditPostDto postDto);
        PostDto GetPostById(int id);
        PostFilterDto GetPostByFilter(PostFilterParams filterParams);
        bool IsSlugExist(string slug);
    }

    public class PostService : IPostService
    {
        private readonly BlogContext _context;
        private readonly IFileManager _fileManager;

        public PostService(BlogContext context, IFileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }

        public OperationResult CreatePost(EPostDto createPostDto)
        {
            try
            {
                if (createPostDto.ImageFile == null)
                    return OperationResult.Error("لطفاً تصویر مربوط به پست را بارگذاری کنید.");

                if (IsSlugExist(createPostDto.Slug))
                    return OperationResult.Error("اسلاگ وارد شده قبلاً وجود دارد.");

                var post = PostMapper.ToPostEntity(createPostDto);
                post.ImageName = _fileManager.SaveFile(createPostDto.ImageFile, Directories.PostImage);

                if (!_context.Categories.Any(c => c.Id == createPostDto.CategoryId))
                    return OperationResult.Error("دسته‌بندی وارد شده وجود ندارد.");

                _context.Posts.Add(post);
                _context.SaveChanges();
                return OperationResult.Success("پست با موفقیت ایجاد شد.");
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"خطا در ایجاد پست: {ex.Message}");
            }
        }

        public OperationResult EditPost(EditPostDto editPostDto)
        {
            try
            {
                var post = _context.Posts.SingleOrDefault(p => p.Id == editPostDto.PostId);
                if (post == null)
                    return OperationResult.NotFound("پست مورد نظر یافت نشد.");

                if (IsSlugExist(editPostDto.Slug) && editPostDto.Slug != post.Slug)
                    return OperationResult.Error("اسلاگ وارد شده قبلاً وجود دارد.");

                if (!_context.Categories.Any(c => c.Id == editPostDto.CategoryId))
                    return OperationResult.Error("دسته‌بندی وارد شده وجود ندارد.");

                post = PostMapper.ToUpdatedPostEntity(editPostDto, post);
                _context.Posts.Update(post);
                _context.SaveChanges();
                return OperationResult.Success("پست با موفقیت ویرایش شد.");
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"خطا در ویرایش پست: {ex.Message}");
            }
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

        public PostFilterDto GetPostByFilter(PostFilterParams filterParams)
        {
            try
            {
                var query = _context.Posts.AsQueryable();

                if (!string.IsNullOrWhiteSpace(filterParams.CategorySlug))
                    query = query.Where(p => p.Category.Slug == filterParams.CategorySlug);

                if (!string.IsNullOrWhiteSpace(filterParams.Title))
                    query = query.Where(p => p.Title.Contains(filterParams.Title));

                var skip = (filterParams.PageId - 1) * filterParams.Take;
                var pageCount = (int)Math.Ceiling((double)query.Count() / filterParams.Take);

                var posts = query.Skip(skip).Take(filterParams.Take)
                                 .Select(post => PostMapper.ToPostDto(post))
                                 .ToList();

                return new PostFilterDto
                {
                    Posts = posts,
                    FilterParams = filterParams,
                    PageCount = pageCount
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"خطا در فیلتر کردن پست‌ها: {ex.Message}");
            }
        }
    }
}
