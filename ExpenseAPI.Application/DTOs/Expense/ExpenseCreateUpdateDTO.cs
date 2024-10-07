using ExpenseAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Application.DTOs.Expense
{
    public class ExpenseCreateUpdateDTO
    {
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
    }
}
