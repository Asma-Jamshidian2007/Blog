﻿using System.ComponentModel.DataAnnotations;

namespace Blog_System.WEB.Areas.Admin.Models.Category
{
    public class EditCategoryViewModel
    {
        [Display(Name = "عنوان")]
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string MetaTag { get; set; } = string.Empty;
        public string MetaDescription { get; set; } = string.Empty;


    }
}
