using ExpenseAPI.Application.Interfaces.Service;
using ExpenseAPI.Domain.Settings;
using ExpenseAPI.Infraestructure.Shared.Service;
using Microsoft.Extensions.Configuration;
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
        public static void AddSharedService(this IServiceCollection services, IConfiguration configuration)
        {

            //services.AddHttpClient<ResendClient>();
            //services.AddOptions();
            //services.Configure<ResendClientOptions>(o =>
            //{
            //    o.ApiToken = Environment.GetEnvironmentVariable("RESEND_APITOKEN")!;
            //});
            //services.AddTransient<IResend, ResendClient>();
            #region Services
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddScoped<IEmailService, EmailService>();
            #endregion
        }
    }
}
