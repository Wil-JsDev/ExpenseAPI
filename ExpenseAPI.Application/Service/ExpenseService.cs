using AutoMapper;
using ExpenseAPI.Application.DTOs.Expense;
using ExpenseAPI.Application.Interfaces.Repository;
using ExpenseAPI.Application.Interfaces.Service;
using ExpenseAPI.Domain.Entities;
using ExpenseAPI.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Application.Service
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;

        public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task<ExpenseDTO> AddAsync(ExpenseCreateUpdateDTO expenseDTO)
        {
            var expense = _mapper.Map<Expense>(expenseDTO);

            if (expense != null)
            {
               await _expenseRepository.AddAsync(expense);
               await _expenseRepository.SaveAsync();
                
                var expenseDto = _mapper.Map<ExpenseDTO>(expense);

                return expenseDto;
            }

            return null;
        }

        public async Task<ExpenseDTO> DeleteAsync(int id)
        {
            var expenseID = await _expenseRepository.GetByIdAsync(id);

            if (expenseID != null)
            {
                await _expenseRepository.DeleteAsync(expenseID);
                await _expenseRepository.SaveAsync();

                var expenseDelete = _mapper.Map<ExpenseDTO>(expenseID);
                return expenseDelete;
            }

            return null;
        }

        public async Task<IEnumerable<ExpenseDTO>> GetAllAsync()
        {
            var expenseAll = await _expenseRepository.GetAllAsync();
            return expenseAll.Select(b => _mapper.Map<ExpenseDTO>(b));
        }

        public async Task<ExpenseDTO> GetByIdAsync(int id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);

            if (expense != null)
            {
                var expenseDto = _mapper.Map<ExpenseDTO>(expense);
                return expenseDto;
            }

            return null;
        }

        public async Task<ExpenseDTO> UpdateAsync(int id, ExpenseCreateUpdateDTO entity)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);

            if (expense != null)
            {
                expense = _mapper.Map<ExpenseCreateUpdateDTO, Expense>(entity,expense);
                await _expenseRepository.UpdateAsync(expense);
                await _expenseRepository.SaveAsync();

                var expenseDTO = _mapper.Map<ExpenseDTO>(expense);
                return expenseDTO;
            }

            return null;
        }

        public async Task<IEnumerable<ExpenseDTO>> FiltersAsync(FiltersOptions filtersOptions)
        {
            DateTime filter = DateTime.UtcNow;

            if (filtersOptions == FiltersOptions.PastWeek)
            {
               filter = filter.AddDays(-7);
                var week = await _expenseRepository.FilterAsync(filter);
                return week.Select(b => _mapper.Map<ExpenseDTO>(b)).ToList();
            } 

            else if (filtersOptions == FiltersOptions.Month)
            {
                filter = filter.AddMonths(-1);
                var pastMonth = await _expenseRepository.FilterAsync(filter);
                return pastMonth.Select(b => _mapper.Map<ExpenseDTO>(b)).ToList();
            }
            else if (filtersOptions == FiltersOptions.LastThreeMonth)
            {
                filter = filter.AddMonths(-3);
                var lastThreeMonth = await _expenseRepository.FilterAsync(filter);
                return lastThreeMonth.Select(b => _mapper.Map<ExpenseDTO>(b)).ToList();
            }
            else if (filtersOptions == FiltersOptions.PastDay)
            {
                filter = filter.AddDays(-1);
                var pastDay = await _expenseRepository.FilterAsync(filter);
                return pastDay.Select(b => _mapper.Map<ExpenseDTO>(b)).ToList();
            }

            return null;
        }        
    }
}
