using ExpenseAPI.Application.DTOs.Expense;
using ExpenseAPI.Application.Interfaces.Service;
using ExpenseAPI.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<ExpenseDTO>> GetAll() =>
           await _expenseService.GetAllAsync();

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ExpenseDTO>> GetbyId(int id)
        {
            var expenseId = await _expenseService.GetByIdAsync(id);

             return expenseId == null  ? NotFound() : Ok(expenseId);
        }

        [HttpPost("add")]
        public async Task<ActionResult<ExpenseDTO>> Add(ExpenseCreateUpdateDTO expense)
        {
            var expenseDto = await _expenseService.AddAsync(expense);

            return CreatedAtAction(nameof(GetbyId), new { id = expenseDto.ID }, expenseDto);
        }

        [HttpGet("Filter")]
        public async Task<ActionResult<ExpenseDTO>> Filter([FromQuery] FiltersOptions filters)
        {
            var filter =  await _expenseService.FiltersAsync(filters);

            return filter == null ? NotFound() : Ok(filter); 
        }

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<ExpenseDTO>> Update(int id,[FromBody] ExpenseCreateUpdateDTO updateDTO )
        {
            var expenseUpdate = await _expenseService.UpdateAsync(id, updateDTO);

            return expenseUpdate == null ? BadRequest() : NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ExpenseDTO>> Delete(int id)
        {
            var expenseId = await _expenseService.DeleteAsync(id);

            return expenseId == null ? NotFound() : NoContent();
        }
    }
}
