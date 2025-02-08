using Blog_System.CoreLayer.DTOs.Comments;
using Blog_System.CoreLayer.DTOs.Posts;
using Blog_System.CoreLayer.Services.Commets;
using Blog_System.CoreLayer.Services.Posts;
using Blog_System.CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Blog_System.WEB.Pages
{
    public class postSingleModel : PageModel
    {
       private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        public postSingleModel(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }
        public PostDto Post { get; set; }
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        [Required]
        [StringLength(1000, ErrorMessage = "Comment text should not exceed 1000 characters.")]
        public string Text { get; set; }
        List<CommentDto> Comments {  get; set; }
        public IActionResult OnGet(string slug)
        {
            Post=_postService.GetPostBySlug(slug);
            if (Post == null)
                  return NotFound();

            Comments = _commentService.GetComments(Post.PostId);
            return Page();
        
        }
        public IActionResult OnPost(string slug)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("Post", new { slug });
            if (!ModelState.IsValid)
            {
                Post = _postService.GetPostBySlug(slug);
                return Page();
            }
            _commentService.CreateComment(new CreateCommentDto()
            {
                PostId= Id,
                Text= Text,
                UserId = User.GetUserId()
            });
            return RedirectToPage("Post", new { slug });

        }
    }
}
