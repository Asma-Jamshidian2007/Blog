using Blog_System.CoreLayer.DTOs.Posts;
using Blog_System.CoreLayer.Mappers;
using Blog_System.CoreLayer.Services.FileManager;
using Blog_System.CoreLayer.Utilities;
using Blog_System.CoreLayer.Utilities.OperationResult;
using Blog_System.DataLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog_System.CoreLayer.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly BlogContext _context;
        private readonly IFileManager _fileManager;

        public PostService(BlogContext context, IFileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }

        public OperationResult CreatePost(CreatePostDto createPostDto)
        {
            try
            {
                if (createPostDto.ImageFile == null)
                    return OperationResult.Error("لطفاً تصویر مربوط به پست را بارگذاری کنید.");

                if (IsSlugExist(createPostDto.Slug))
                    return OperationResult.Error("اسلاگ وارد شده قبلاً وجود دارد.");

                if (!_context.Categories.Any(c => c.Id == createPostDto.CategoryId))
                    return OperationResult.Error("دسته‌بندی وارد شده وجود ندارد.");

                var post = PostMapper.ToPostEntity(createPostDto);
                post.ImageFile = _fileManager.SaveAndReturnFileName(createPostDto.ImageFile, Directories.PostImage);
                post.ImageName = _fileManager.SaveAndReturnFileName(createPostDto.ImageFile, Directories.PostImage);
                _context.Posts.Add(post);
                int result = _context.SaveChanges();

                if (result > 0)
                    return OperationResult.Success("پست با موفقیت ایجاد شد.");

                return OperationResult.Error("خطا در ذخیره‌سازی پست.");
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

                var oldImage = post.ImageFile;
                post = PostMapper.ToUpdatedPostEntity(editPostDto, post);

                if (editPostDto.ImageFile != null)
                {
                    post.ImageFile = _fileManager.SaveAndReturnFileName(editPostDto.ImageFile, Directories.PostImage);
                    _fileManager.DeleteImage(oldImage, Directories.PostImage);
                }

                _context.Posts.Update(post);
                int result = _context.SaveChanges();

                if (result > 0)
                    return OperationResult.Success("پست با موفقیت ویرایش شد.");

                return OperationResult.Error("خطا در ذخیره‌سازی ویرایش پست.");
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"خطا در ویرایش پست: {ex.Message}");
            }
        }

        public PostDto GetPostById(int id)
        {
            var post = _context.Posts
                .Include(c => c.Category)
                .Include(c => c.SubCategory)
                .SingleOrDefault(c => c.Id == id);

            return post != null ? PostMapper.ToPostDto(post) : null;
        }

        public bool IsSlugExist(string slug)
        {
            return _context.Posts.Any(p => p.Slug == slug.ToSlug());
        }

        public PostFilterDto GetPostByFilter(PostFilterParams filterParams)
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

        PostDto IPostService.GetPostBySlug(string slug)
        {

            var post = _context.Posts
               .Include(c => c.Category)
               .Include(c=> c.User)
               .Include(c => c.SubCategory)
               .SingleOrDefault(c => c.Slug == slug);
            if (post == null)
                return null; if (post == null)
                return null;

            return post != null ? PostMapper.ToPostDto(post) : null;
        }
    }
}
