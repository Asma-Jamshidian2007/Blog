using Blog_System.CoreLayer.DTOs.Categories;
using Blog_System.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_System.CoreLayer.Mappers
{
    public class CategoryMapper
    {
        public static CategoryDto Map(Category category) => new CategoryDto()
        {
            MetaTag = category.MetaTag,
            Title = category.Title,
            Slug = category.Slug,
            ParentId = category.ParentId,
            id = category.Id

        };

    }
}
