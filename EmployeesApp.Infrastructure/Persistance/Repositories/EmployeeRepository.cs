using EmployeesApp.Application.Employees.Interfaces;
using EmployeesApp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EmployeesApp.Infrastructure.Persistance.Repositories
{
    public class EmployeeRepository(ApplicationContext context) : IEmployeeRepository
    {
        public void Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
        }

        //Classic C# syntax for GetAll()
        public Employee[] GetAll()
        {
            return [.. context.Employees];
        }

        public Employee? GetById(int id) => context.Employees
            .SingleOrDefault(e => e.Id == id);
    }
}