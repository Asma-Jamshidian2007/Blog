using Blog_System.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Blog_System.CoreLayer.DTOs.Posts
{
    public class PostFilterDto
    {
        public int PageCount { get; set; }
        public List<PostDto> Posts { get; set; }=new List<PostDto>();
        public PostFilterParams filterParams { get; set; } = new PostFilterParams();
    }
    public class PostFilterParams
    {
        public string Title { get; set; } = string.Empty;
        public string CategorySlug { get; set; } = string.Empty;
        public int PageId { get; set; }
        public int Take { get; set; }
    }
}
