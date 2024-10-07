using ExpenseAPI.Application.Interfaces.Service;
using ExpenseAPI.Application.Mapping;
using ExpenseAPI.Application.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Application
{
    public static class AddApplication
    {
        public static void AddServices(this IServiceCollection services)
        {
            #region Mapper
            services.AddAutoMapper(typeof(MappingProfile));
            #endregion

            #region Services
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<ICategoryService, CategoryService>();
            #endregion
        }
    }
}
