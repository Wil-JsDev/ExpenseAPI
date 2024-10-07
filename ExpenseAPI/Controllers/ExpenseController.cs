using ExpenseAPI.Application.DTOs.Expense;
using ExpenseAPI.Application.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet("All")]
        public async Task<IEnumerable<ExpenseDTO>> GetAll() =>
           await _expenseService.GetAllAsync();

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ExpenseDTO>> GetbyId(int id)
        {
            var expenseId = await _expenseService.GetByIdAsync(id);

             return expenseId == null  ? NotFound() : Ok(expenseId);
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ExpenseDTO>> Add(ExpenseCreateUpdateDTO expense)
        {
            var expenseDto = await _expenseService.AddAsync(expense);

            return CreatedAtAction(nameof(GetbyId), new { id = expenseDto.ID }, expenseDto);
        }
    }
}
