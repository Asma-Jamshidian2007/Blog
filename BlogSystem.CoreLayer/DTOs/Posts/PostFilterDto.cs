using Blog_System.DataLayer.Entities;
using System;
using System.Collections.Generic;

namespace Blog_System.CoreLayer.DTOs.Posts
{
    public class PostFilterDto
    {
        public int PageCount { get; set; }

        public List<PostDto> Posts { get; set; } = new List<PostDto>();

        public PostFilterParams FilterParams { get; set; } = new PostFilterParams();
    }
}
