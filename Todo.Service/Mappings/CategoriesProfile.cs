using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models.Categories;
using Todo.Models.Entities;

namespace Todo.Service.Mappings
{
    public class CategoriesProfile : Profile
    {

        public CategoriesProfile()
        {
            CreateMap<CategoryAddRequestDto, Category>();
            CreateMap<CategoryUpdateRequestDto, Category>();
            CreateMap<Category, CategoryResponseDto>();
        }
    }
}
