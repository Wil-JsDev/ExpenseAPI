using ExpenseAPI.Application.Interfaces.Repository;
using ExpenseAPI.Domain.Entities;
using ExpenseAPI.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Infraestructure.Persistence.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ExpenseContext _context;

        public CategoryRepository(ExpenseContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category entity)
        {
            await _context.Categories.AddAsync(entity);
        }

        public async Task DeleteAsync(Category entity)
        {
            _context.Categories.Remove(entity);
        }

        public async Task<IEnumerable<Category>> GetAllAsync() =>
            await _context.Categories.ToListAsync();    

        public async Task<Category> GetByIdAsync(int id)
        {
            var categoryId = await _context.Categories.FindAsync(id);
            return categoryId;
        }

        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();

        public async Task UpdateAsync(Category entity)
        {
            _context.Categories.Attach(entity);
            _context.Categories.Entry(entity).State = EntityState.Modified;
        }
    }
}
