using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_System.CoreLayer.DTOs.Categories
{
        using System.ComponentModel.DataAnnotations;

        public class CreateCategoryDto
        {
            [Required(ErrorMessage = "عنوان الزامی است.")]
            [StringLength(100, ErrorMessage = "عنوان نمی‌تواند بیشتر از 100 کاراکتر باشد.")]
            public string Title { get; set; } = string.Empty;

            [Required(ErrorMessage = "Slug الزامی است.")]
            [StringLength(100, ErrorMessage = "Slug نمی‌تواند بیشتر از 100 کاراکتر باشد.")]
            public string Slug { get; set; } = string.Empty;

            public int? ParentId { get; set; }

            [StringLength(50, ErrorMessage = "MetaTag نمی‌تواند بیشتر از 50 کاراکتر باشد.")]
            public string MetaTag { get; set; } = string.Empty;

            [StringLength(200, ErrorMessage = "MetaDescription نمی‌تواند بیشتر از 200 کاراکتر باشد.")]
            public string MetaDescription { get; set; } = string.Empty;
            public int Id { get;  set; }
        }
    }

