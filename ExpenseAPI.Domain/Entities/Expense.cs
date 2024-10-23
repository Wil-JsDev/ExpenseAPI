using ExpenseAPI.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Domain.Entities
{
    public class Expense
    {
        public int ExpenseId { get; set; }

        public decimal Amount { get; set; }

        public DateTime ExpenseDate { get; set; } = DateTime.UtcNow;

        // Foreign Key to Category
        public int CategoryId { get; set; }  // Foreign Key

        public Category Category { get; set; }  // Navigation property

        public string Description { get; set; }

        public DateTime Created_at { get; set; } = DateTime.UtcNow;

    }
}
