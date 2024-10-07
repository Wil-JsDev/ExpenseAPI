using ExpenseAPI.Application.Interfaces.Repository;
using ExpenseAPI.Infraestructure.Persistence.Context;
using ExpenseAPI.Infraestructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Infraestructure.Persistence
{
    public static class AddInfraestructure
    {

        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            #region Connection
            services.AddDbContext<ExpenseContext>(p =>
            {
                p.UseNpgsql(configuration.GetConnectionString("ExpenseAPIDB"),
                b => b.MigrationsAssembly("ExpenseAPI.Infraestructure.Persistence"));
            });
            #endregion

            #region
            services.AddTransient<IExpenseRepository, ExpenseRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            #endregion

        }
    }
}
