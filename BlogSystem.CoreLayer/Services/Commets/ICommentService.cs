using Blog_System.CoreLayer.DTOs.Comments;
using Blog_System.CoreLayer.Utilities.OperationResult;
using Blog_System.DataLayer.Context;
using Blog_System.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_System.CoreLayer.Services.Commets
{
    public interface ICommentService
    {
        OperationResult CreateComment(CreateCommentDto commentDto);
        List<CommentDto> GetComments(int id);
    }
    public class CommentService : ICommentService
    {
        private readonly BlogContext _context;
        public CommentService(BlogContext context)
        {
            _context = context;
        }
        public OperationResult CreateComment(CreateCommentDto commentDto)
        {
            var comment = new PostComments()
            {
                UserId = commentDto.UserId,
                PostId = commentDto.PostId,
                Text = commentDto.Text
            };
            _context.Add(comment);
            _context.SaveChanges();
            return OperationResult.Success("Comment Added Successfully");
        }

        public List<CommentDto> GetComments(int id)
        {
            return _context.PostComments.Where(pc => pc.PostId == id).Select(comment => new CommentDto()
            {
                 UserName = comment.UserName,
                PostId = comment.PostId,
                Text = comment.Text,
                Id = comment.Id
            }).ToList();
        }
    }
}
