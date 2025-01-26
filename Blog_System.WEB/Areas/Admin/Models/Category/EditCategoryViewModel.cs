using System.ComponentModel.DataAnnotations;

namespace Blog_System.WEB.Areas.Admin.Models.Category
{
    public class EditCategoryViewModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "عنوان ضروری است.")]
        [StringLength(100, ErrorMessage = "عنوان نباید بیشتر از 100 کاراکتر باشد.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Slug ضروری است.")]
        [StringLength(100, ErrorMessage = "Slug نباید بیشتر از 100 کاراکتر باشد.")]
        [RegularExpression(@"^[a-z0-9]+(?:-[a-z0-9]+)*$", ErrorMessage = "Slug باید فقط شامل حروف کوچک و اعداد باشد و از خط تیره (-) برای جدا کردن بخش‌ها استفاده کند.")]
        public string Slug { get; set; } = string.Empty;

        [StringLength(150, ErrorMessage = "MetaTag نباید بیشتر از 150 کاراکتر باشد.")]
        public string MetaTag { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "MetaDescription نباید بیشتر از 300 کاراکتر باشد.")]
        public string MetaDescription { get; set; } = string.Empty;
    }
}
