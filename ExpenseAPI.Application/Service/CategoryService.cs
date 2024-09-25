using AutoMapper;
using ExpenseAPI.Application.DTOs.Category;
using ExpenseAPI.Application.Interfaces.Repository;
using ExpenseAPI.Application.Interfaces.Service;
using ExpenseAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Application.Service
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> AddAsync(CategoryCreateUpdateDTO entity)
        {
            var category = _mapper.Map<Category>(entity);
            if (category != null)
            {
                await _categoryRepository.AddAsync(category);
                await _categoryRepository.SaveAsync();

                var categoryDto = _mapper.Map<CategoryDTO>(category);
                return categoryDto;
            }

            return null;
        }

        public async Task<CategoryDTO> DeleteAsync(int id)
        {
            var categoryId = await _categoryRepository.GetByIdAsync(id);
            if (categoryId != null)
            {
                await _categoryRepository.DeleteAsync(categoryId);
                await _categoryRepository.SaveAsync();

                var categoryDto = _mapper.Map<CategoryDTO>(categoryId);
                return categoryDto;
            }
            return null;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var categroyList = await _categoryRepository.GetAllAsync();
            return categroyList.Select(b => _mapper.Map<CategoryDTO>(b));
        }

        public async Task<CategoryDTO> GetByIdAsync(int id)
        {
            var categoryId = await _categoryRepository.GetByIdAsync(id);
            if (categoryId != null)
            {
                var categoryDto = _mapper.Map<CategoryDTO>(categoryId);
                return categoryDto;
            }
            return null;
        }

        public async Task<CategoryDTO> UpdateAsync(int id, CategoryCreateUpdateDTO entity)
        {
            var categoryId = await _categoryRepository.GetByIdAsync(id);
            if (categoryId != null)
            {
                var categoryUpdate = _mapper.Map<CategoryCreateUpdateDTO, Category>(entity, categoryId);
                await _categoryRepository.UpdateAsync(categoryUpdate);
                await _categoryRepository.SaveAsync();

                var categoryDto = _mapper.Map<CategoryDTO>(categoryUpdate);
                return categoryDto;
            }

            return null;
        }
    }
}
