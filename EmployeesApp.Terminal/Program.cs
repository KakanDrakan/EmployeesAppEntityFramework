using EmployeesApp.Application.Employees.Services;
using EmployeesApp.Infrastructure.Persistance;
using EmployeesApp.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace EmployeesApp.Terminal;
internal class Program
{
    // static readonly EmployeeService employeeService = new(new EmployeeRepository();


    static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .Build();
        string connectionString = configuration.GetConnectionString("DefaultConnection");

        // Use the connection string
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlServer(connectionString)
            .Options;

        using ApplicationContext context = new(new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlServer(connectionString)
            .Options);

        EmployeeRepository employeeRepository = new(context);
        EmployeeService employeeService = new(employeeRepository);
        ListAllEmployees(employeeService);
        // ListEmployee(562);
    }

    private static void ListAllEmployees(EmployeeService employeeService)
    {
        foreach (var item in employeeService.GetAll())
            Console.WriteLine(item.Name);

        Console.WriteLine("------------------------------");
    }

    //private static void ListEmployee(int employeeID)
    //{
    //    Employee? employee;

    //    try
    //    {
    //        employee = employeeService.GetById(employeeID);
    //        Console.WriteLine($"{employee?.Name}: {employee?.Email}");
    //        Console.WriteLine("------------------------------");
    //    }
    //    catch (ArgumentException e)
    //    {
    //        Console.WriteLine($"EXCEPTION: {e.Message}");
    //    }
    //}
}
