using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Application.Interfaces.Service
{
    public interface IBaseService<T,TI> 
        where T : class       
        where TI : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task<T> AddAsync(TI entity);

        Task<T> UpdateAsync(int id,TI entity);

        Task<T> DeleteAsync(int id);
    }
}
