using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace ExpenseAPI.Extensions
{
    public static class ServiceExtension
    {
        public static void AddSwagerExtension(this IServiceCollection service)
        {
            //Configuracion para UI
            service.AddSwaggerGen(option =>
            {
                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", searchOption: SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => option.IncludeXmlComments(xmlFile));

                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Store De La Cruz",
                    Description = "Esta API es de un Expense Tracker",
                    Contact = new OpenApiContact
                    {
                        Name = "Wilmer De La Cruz",
                        Email = "WilmerDeLaCruz@gmail.com"
                    }

                });

                option.DescribeAllParametersInCamelCase();
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here}"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        }, new List<string>()
                    },
                });

            });
        }
    }
}
