using ExpenseAPI.Application.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Application.Interfaces.Service
{
    public interface ICategoryService : IBaseService<CategoryDTO, CategoryCreateUpdateDTO>
    {

    }
}
