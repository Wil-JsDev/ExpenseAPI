using ExpenseAPI.Application.DTOs.Category;
using ExpenseAPI.Application.DTOs.Expense;
using ExpenseAPI.Application.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            var categoryDto = await _categoryService.GetByIdAsync(id);

            return categoryDto == null ? NotFound() : Ok(categoryDto);

        }

        [HttpPost("Add")]
        public async Task<ActionResult<CategoryDTO>> AddAsync(CategoryCreateUpdateDTO category)
        {
            var categoryDto = await _categoryService.AddAsync(category);

            return CreatedAtAction(nameof(GetById), new { id = categoryDto.ID }, categoryDto);
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int Id)
        {
            var categoryId = await _categoryService.GetByIdAsync(Id);

            return categoryId == null ? NotFound() : Ok(categoryId);
        }

    }
}
