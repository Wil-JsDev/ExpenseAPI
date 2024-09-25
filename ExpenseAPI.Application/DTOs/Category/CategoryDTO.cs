using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Application.DTOs.Category
{
    public record CategoryDTO 
    (
        int ID,
        string name,
        string Description
    );
}
