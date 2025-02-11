using Blog_System.CoreLayer.DTOs.Comments;
using Blog_System.CoreLayer.Utilities.OperationResult;
using Blog_System.DataLayer.Context;
using Blog_System.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog_System.CoreLayer.Services.Commets
{
    public class CommentService : ICommentService
    {
        private readonly BlogContext _context;

        public CommentService(BlogContext context)
        {
            _context = context;
        }

        public OperationResult CreateComment(CreateCommentDto commentDto)
        {
            var postExists = _context.Posts.Any(p => p.Id == commentDto.PostId);
            if (!postExists)
            {
                return OperationResult.Error("The post does not exist.");
            }

            var comment = new PostComments()
            {
                UserId = commentDto.UserId,
                PostId = commentDto.PostId,
                Text = commentDto.Text,
            };

            _context.PostComments.Add(comment);
            _context.SaveChanges();
            return OperationResult.Success("Comment Added Successfully");
        }



        public List<CommentDto> GetComments(int id)
        {
            return _context.PostComments.Where(pc => pc.PostId == id)
                .Include(pc => pc.User)
                .Select(comment => new CommentDto()
            {
                UserName = comment.User.FullName,
                PostId = comment.PostId,
                Text = comment.Text,
                CreationDate= comment.CreationDate
            }).ToList();
        }
    }
}
