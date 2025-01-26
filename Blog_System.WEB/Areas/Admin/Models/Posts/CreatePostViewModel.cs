using System.ComponentModel.DataAnnotations;

namespace Blog_System.WEB.Areas.Admin.Models.Posts
{
    public class CreatePostViewModel
    {
        [Display(Name = "انتخاب دسته‌بندی")]
        [Required(ErrorMessage = "دسته‌بندی ضروری است.")]
        public int CategoryId { get; set; } = 0;

        [Display(Name = "انتخاب زیر دسته‌بندی")]
        public int? SubCategoryId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "عنوان ضروری است.")]
        [StringLength(200, ErrorMessage = "عنوان نباید بیشتر از 200 کاراکتر باشد.")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "توضیحات ضروری است.")]
        [UIHint("CKeditor")]
        [StringLength(5000, ErrorMessage = "توضیحات نباید بیشتر از 5000 کاراکتر باشد.")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Slug")]
        [Required(ErrorMessage = "Slug ضروری است.")]
        [StringLength(100, ErrorMessage = "Slug نباید بیشتر از 100 کاراکتر باشد.")]
        [RegularExpression(@"^[a-z0-9]+(?:-[a-z0-9]+)*$", ErrorMessage = "Slug باید فقط شامل حروف کوچک و اعداد باشد و از خط تیره (-) برای جدا کردن بخش‌ها استفاده کند.")]
        public string Slug { get; set; } = string.Empty;

        [Display(Name = "عکس")]
        [Required(ErrorMessage = "عکس ضروری است.")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "jpg,png,jpeg", ErrorMessage = "فقط فرمت‌های JPG، PNG و JPEG مجاز هستند.")]
        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "حجم فایل نباید بیشتر از 5 مگابایت باشد.")]
        public IFormFile ImageFile { get; set; }
    }

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly long _maxSize;

        public MaxFileSizeAttribute(long maxSize)
        {
            _maxSize = maxSize;
        }

        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            return file != null && file.Length <= _maxSize;
        }
    }
}
