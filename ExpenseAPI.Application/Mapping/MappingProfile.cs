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
                #region Expense
                CreateMap<Expense, ExpenseDTO>()
                    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ExpenseId))
                    .ReverseMap();

                CreateMap<ExpenseDTO, Expense>()
                    .ForMember(dest => dest.ExpenseId, opt => opt.MapFrom(src => src.ID))
                    .ReverseMap();

                CreateMap<ExpenseCreateUpdateDTO, Expense>();
                CreateMap<Expense, ExpenseCreateUpdateDTO>();
                #endregion

                #region Category
                CreateMap<Category, CategoryDTO>()
                    .ForMember(opt => opt.ID, opt => opt.MapFrom(src => src.CategoryId))
                    .ReverseMap();

                CreateMap<CategoryDTO, Category>()
                    .ForMember(opt => opt.CategoryId, opt => opt.MapFrom(src => src.ID))
                    .ReverseMap();

                CreateMap<CategoryCreateUpdateDTO, Category>()
                    .ForMember(opt => opt.Name, opt => opt.MapFrom(src => src.name))
                    .ReverseMap();

                CreateMap<Category, CategoryCreateUpdateDTO>()
                    .ForMember(opt => opt.name, opt => opt.MapFrom(src => src.Name))
                    .ReverseMap();
                #endregion
            } 
    }
}

