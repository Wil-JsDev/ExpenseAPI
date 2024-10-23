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
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ExpenseContext _expenseContext;
        public ExpenseRepository(ExpenseContext expenseContext)
        {
            _expenseContext = expenseContext;
        }

        public async Task AddAsync(Expense entity)
        {
          await _expenseContext.Expenses.AddAsync(entity);
        }

        public async Task DeleteAsync(Expense entity)
        {
            _expenseContext.Expenses.Remove(entity);
        }

        public async Task<IEnumerable<Expense>> FilterAsync(DateTime filter)
        {
            var query = await _expenseContext.Expenses.AsQueryable()
                                       .Where(e => e.ExpenseDate < filter)
                                       .ToListAsync();
            return query;
        }

        public async Task<IEnumerable<Expense>> GetAllAsync() =>
            await _expenseContext.Expenses.ToListAsync();

        public async Task<Expense> GetByIdAsync(int id)
        {
            var expenseId = await _expenseContext.Expenses.FindAsync(id);
            return expenseId;
        }

        public async Task SaveAsync() =>
            await _expenseContext.SaveChangesAsync();

        public async Task UpdateAsync(Expense entity)
        {
            _expenseContext.Expenses.Attach(entity);
            _expenseContext.Expenses.Entry(entity).State = EntityState.Modified;
        }
    }
}
