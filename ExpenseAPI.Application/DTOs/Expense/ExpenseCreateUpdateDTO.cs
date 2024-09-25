using ExpenseAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Application.DTOs.Expense
{
    public record ExpenseCreateUpdateDTO 
    (
        decimal Amount,
        DateTime ExpenseDate,
        int CategoryId,
        string Description
    );
}
