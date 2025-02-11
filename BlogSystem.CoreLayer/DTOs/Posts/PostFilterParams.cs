using System.ComponentModel.DataAnnotations;

namespace Blog_System.CoreLayer.DTOs.Posts
{
    public class PostFilterParams
    {
        [StringLength(200, ErrorMessage = "عنوان نمی‌تواند بیشتر از 200 کاراکتر باشد.")]
        public string Title { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Slug دسته‌بندی نمی‌تواند بیشتر از 100 کاراکتر باشد.")]
        public string CategorySlug { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "شماره صفحه باید بزرگتر از صفر باشد.")]
        public int PageId { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "تعداد نتایج در هر صفحه باید بین 1 و 100 باشد.")]
        public int Take { get; set; } = 10;
    }
}
