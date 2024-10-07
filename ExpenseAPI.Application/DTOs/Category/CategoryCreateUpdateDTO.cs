using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Application.DTOs.Category
{
    public class CategoryCreateUpdateDTO
    {
        public string name { get; set; }

        public string Description { get; set; }
    }
}
