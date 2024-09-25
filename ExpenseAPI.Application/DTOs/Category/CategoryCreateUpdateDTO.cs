using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Application.DTOs.Category
{
    public record CategoryCreateUpdateDTO
    (
        string name,
        string Description
    );
}
