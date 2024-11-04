using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Todo.Core.Entities;
using Todo.Core.Exceptions;
using Todo.Models.Categories;
using Todo.Models.Entities;
using Todo.Repository.Repository.Abstract;
using Todo.Service.Abstract;

namespace Todo.Service.Concretes
{
    public class CategoryService(ICategoryRepository _categoryRepository, IMapper _mapper) : ICategoryService
    {
        public ReturnModel<Empty> Add(CategoryAddRequestDto dto)
        {
            Category category = _mapper.Map<Category>(dto);

            _categoryRepository.Add(category);

            return new ReturnModel<Empty>
            {
                Message = "Kategori Eklendi.",
                Status = 201,
                Success = true
            };

        }

        public ReturnModel<Empty> Delete(int id)
        {
            Category category = CheckGetById(id);

            _categoryRepository.Delete(category);

            return new ReturnModel<Empty>
            {
                Message = "Kategori silindi.",
                Status = 200,
                Success = true
            };
        }

        public ReturnModel<List<CategoryResponseDto>> GetAllCategories()
        {
            List<Category> categories = _categoryRepository.GetAll();
            List<CategoryResponseDto> responses = _mapper.Map<List<CategoryResponseDto>>(categories);

            return new ReturnModel<List<CategoryResponseDto>>
            {
                Data = responses,
                Status = 200,
                Success = true
            };

        }

        public ReturnModel<CategoryResponseDto> GetById(int id)
        {
            Category category = CheckGetById(id);
            CategoryResponseDto dto = _mapper.Map<CategoryResponseDto>(category);

            return new ReturnModel<CategoryResponseDto>
            {
                Data = dto,
                Status = 200,
                Success = true
            };

        }

        public ReturnModel<Empty> Update(CategoryUpdateRequestDto dto)
        {
            Category category = CheckGetById(dto.Id);
            category.Name = dto.Name;

            _categoryRepository.Update(category);

            return new ReturnModel<Empty>
            {
                Message = "Kategori Güncellendi.",
                Success = true,
                Status = 200
            };
        }
        
        private Category CheckGetById(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category is null)
            {
                throw new NotFoundException("İlgili id ye ait Kategori bulunamadı: " + id);
            }
            return category;
        }
    }
}
