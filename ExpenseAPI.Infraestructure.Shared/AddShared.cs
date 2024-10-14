using ExpenseAPI.Application.Interfaces.Service;
using ExpenseAPI.Infraestructure.Shared.Service;
using Microsoft.Extensions.DependencyInjection;
using Resend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Infraestructure.Shared
{
    public static class AddShared
    {
        public static void AddSharedService(this IServiceCollection services)
        {

            //services.AddHttpClient<ResendClient>();
            services.AddOptions();
            services.Configure<ResendClientOptions>(o =>
            {
                o.ApiToken = Environment.GetEnvironmentVariable("RESEND_APITOKEN")!;
            });
            services.AddTransient<IResend, ResendClient>();
            services.AddScoped<IEmailService, EmailService>();

        }
    }
}
