using EmployeesApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesApp.Infrastructure.Persistance;

// Denna konstruktor krävs för att konfigurationen i Program.cs ska fungera
public class ApplicationContext(DbContextOptions<ApplicationContext> options)
: DbContext(options)
{
    // Exponerar våra entiteter via properties av typen DbSet<T>
    public DbSet<Employee> Employees { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Anropen nedan är exempel på att använda Fluent API (till skillnad från att
        // använda attribut direkt på entiteterna)
        // Specificerar vilken datatyp databasen ska använda för en specifik kolumn
        //modelBuilder.Entity<Order>()
        //.Property(o => o.PriceTotal)
        //.HasColumnType(SqlDbType.Money.ToString());
        // Specificerar data som en specific tabell ska för-populeras med
        modelBuilder.Entity<Employee>().HasData(
        new Employee { Id = 1, Name = "Hans", Email = "Stockholm@gmail.com" },
        new Employee { Id = 2, Name = "Emma", Email = "test@test.com" },
        new Employee { Id = 3, Name = "hotmail", Email = "hotmail@hotmail.com" });
    }
}
