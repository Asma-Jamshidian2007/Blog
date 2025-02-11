using Blog_System.CoreLayer.DTOs.Comments;
using Blog_System.CoreLayer.Utilities.OperationResult;
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
}
