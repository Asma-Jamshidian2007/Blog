using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_System.CoreLayer.DTOs.Categories
{
    public class CreateCategoryDto
    {
        public string Title {  get; set; }=string.Empty;
        public string Slug { get; set; }=string.Empty;
        public int? ParentId { get; set; }
        public string MetaTag { get; set; } = string.Empty;
        public string MetaDescription { get; set; } = string.Empty;
      
    }
}

