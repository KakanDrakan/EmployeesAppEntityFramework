using EmployeesApp.Application.Employees.Interfaces;
using EmployeesApp.Application.Employees.Services;
using EmployeesApp.Infrastructure.Persistance;
using EmployeesApp.Infrastructure.Persistance.Repositories;
using EmployeesApp.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeesApp.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddScoped<IEmployeeService, EmployeeService>();

        builder.Services.AddScoped<MyLogServiceFilterAttribute>();

        var connString = builder.Configuration
            .GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationContext>(
            o => o.UseSqlServer(connString));

        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}