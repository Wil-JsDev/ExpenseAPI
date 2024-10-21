using ExpenseAPI.Infraestructure.Persistence;
using ExpenseAPI.Application;
using Resend;
using ExpenseAPI.Infraestructure.Shared;
using Microsoft.AspNetCore.Identity;
using ExpenseAPI.Infraestructure.Identity.Entities;
using ExpenseAPI.Infraestructure.Identity.Seeds;
using ExpenseAPI.Infraestructure.Identity;
using ExpenseAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwagerExtension();

//DI
builder.Services.AddIdentityMethod(configuration);
builder.Services.AddPersistence(configuration);
builder.Services.AddServices();
builder.Services.AddSharedService(configuration);
//builder.Services.AddHttpClient<ResendClient>();

var app = builder.Build();


//Configuracion para crear los roles por default y son los que configure
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var rolManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedAsync(userManager, rolManager);
        await DefaultAdmin.SeedAsync(userManager, rolManager);
        await DefaultBasicUser.SeedAsync(userManager, rolManager);

    }
    catch (Exception ex)
    {
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwaggerExtension();

app.MapControllers();

app.Run();
