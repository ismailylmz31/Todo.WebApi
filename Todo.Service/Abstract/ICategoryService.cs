using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Todo.Core.Entities;
using Todo.Models.Categories;

namespace Todo.Service.Abstract
{
    public interface ICategoryService
    {

        ReturnModel<List<CategoryResponseDto>> GetAllCategories();
        ReturnModel<CategoryResponseDto> GetById(int id);
        ReturnModel<Empty> Add(CategoryAddRequestDto dto);
        ReturnModel<Empty> Update(CategoryUpdateRequestDto dto);

        ReturnModel<Empty> Delete(int id);

    }
}
