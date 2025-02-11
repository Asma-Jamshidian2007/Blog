using Blog_System.CoreLayer.DTOs.Posts;
using Blog_System.CoreLayer.Services.Posts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Blog_System.WEB.Pages
{
    public class SearchModel(IPostService postService) : PageModel
    {
        public PostFilterDto Filter { get; set; } = new PostFilterDto();
        private readonly IPostService _postService = postService;

        public void OnGet(int PageId = 1, string categorySlug = null,string query = null)
        {
            Filter = _postService.GetPostByFilter(new PostFilterParams()
            {
                CategorySlug = categorySlug,
                PageId = PageId,
                Take = 12,
                Title = query
            });
        }
    }
}
