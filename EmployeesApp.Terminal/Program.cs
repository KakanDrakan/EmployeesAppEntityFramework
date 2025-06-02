using EmployeesApp.Application.Employees.Interfaces;
using EmployeesApp.Application.Employees.Services;
using EmployeesApp.Domain.Entities;
using EmployeesApp.Infrastructure.Persistance;
using EmployeesApp.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace EmployeesApp.Terminal;
internal class Program
{
    static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false)
        .Build();

        var services = new ServiceCollection();
        services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEmployeeService, EmployeeService>();

        using var serviceProvider = services.BuildServiceProvider();
        var employeeService = serviceProvider.GetRequiredService<IEmployeeService>();

        ListAllEmployees(employeeService);
        ListEmployee(1, employeeService);
    }

    private static void ListAllEmployees(IEmployeeService employeeService)
    {
        foreach (var item in employeeService.GetAll())
            Console.WriteLine(item.Name);

        Console.WriteLine("------------------------------");
    }

    private static void ListEmployee(int employeeID, IEmployeeService employeeService)
    {
        Employee? employee;

        try
        {
            employee = employeeService.GetById(employeeID);
            Console.WriteLine($"{employee?.Name}: {employee?.Email}");
            Console.WriteLine("------------------------------");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine($"EXCEPTION: {e.Message}");
        }
    }
}
