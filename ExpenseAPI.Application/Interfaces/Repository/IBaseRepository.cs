using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Application.Interfaces.Repository
{
    public interface IBaseRepository<Entity> where Entity : class
    {
        Task<IEnumerable<Entity>> GetAllAsync();
        
        Task<Entity> GetByIdAsync(int id);

        Task AddAsync(Entity entity);
        
        Task UpdateAsync(Entity entity);

        Task DeleteAsync(Entity entity);

        Task SaveAsync();        
    }
}
