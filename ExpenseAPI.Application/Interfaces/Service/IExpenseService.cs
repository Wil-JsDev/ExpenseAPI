using ExpenseAPI.Application.DTOs.Expense;
using ExpenseAPI.Domain.Entities;
using ExpenseAPI.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Application.Interfaces.Service
{
    public interface IExpenseService : IBaseService<ExpenseDTO, ExpenseCreateUpdateDTO>
    {
        Task<IEnumerable<ExpenseDTO>> FiltersAsync(FiltersOptions filtersOptions);

    }
}
