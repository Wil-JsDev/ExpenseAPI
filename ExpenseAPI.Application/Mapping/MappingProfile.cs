using AutoMapper;
using ExpenseAPI.Application.DTOs.Category;
using ExpenseAPI.Application.DTOs.Expense;
using ExpenseAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ExpenseDTO,Expense>().ReverseMap();
            CreateMap<ExpenseCreateUpdateDTO, Expense>().ReverseMap();

            CreateMap<CategoryCreateUpdateDTO, Category>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();
        }
    }
}
