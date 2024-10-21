using ExpenseAPI.Application.DTOs.Category;
using ExpenseAPI.Application.DTOs.Expense;
using ExpenseAPI.Application.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Basic")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
       
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<CategoryDTO>> GetAll() =>
           await _categoryService.GetAllAsync();


        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            var categoryDto = await _categoryService.GetByIdAsync(id);

            return categoryDto == null ? NotFound() : Ok(categoryDto);

        }

        [HttpPost("add")]
        public async Task<ActionResult<CategoryDTO>> AddAsync(CategoryCreateUpdateDTO category)
        {
            var categoryDto = await _categoryService.AddAsync(category);

            return CreatedAtAction(nameof(GetById), new { id = categoryDto.ID }, categoryDto);
        }

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<CategoryCreateUpdateDTO>> Update(int id, CategoryCreateUpdateDTO updateDTO )
        {

            var categoryUpdate = await _categoryService.UpdateAsync(id, updateDTO);

            return categoryUpdate == null ? BadRequest() : NoContent();

        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int Id)
        {
            var categoryId = await _categoryService.GetByIdAsync(Id);

            return categoryId == null ? NotFound() : Ok(categoryId);
        }

    }
}
