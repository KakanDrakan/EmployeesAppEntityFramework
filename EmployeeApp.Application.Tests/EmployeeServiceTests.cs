using EmployeesApp.Application.Employees.Interfaces;
using EmployeesApp.Application.Employees.Services;
using EmployeesApp.Domain.Entities;
using Moq;

namespace EmployeeApp.Application.Tests;

public class EmployeeServiceTests
{
    [Fact]
    public void AddEmployee_WithRightCredentials_WillAddEmployeeToListMoq()
    {
        // Arrange
        var mockRepo = new Mock<IEmployeeRepository>();
        var service = new EmployeeService(mockRepo.Object);

        var employee = new Employee
        {
            Name = "lisa",
            Email = "lisa@Ajax.com"
        };

        // Act
        service.Add(employee);

        // Assert
        mockRepo.Verify(o => o.Add(It.Is<Employee>(o =>
            o.Name == "Lisa" &&
            o.Email == "lisa@ajax.com")), Times.Once);
    }

    [Fact]
    public void GetById_WithValidId_ReturnsEmployee()
    {
        // Arrange
        var employee = new Employee { Id = 1, Name = "Ben", Email = "dover@hotmale.com" };
        var mockRepo = new Mock<IEmployeeRepository>();
        mockRepo
            .Setup(r => r.GetById(1))
            .Returns(employee);

        var service = new EmployeeService(mockRepo.Object);

        // Act
        var result = service.GetById(1);

        // Assert
        Assert.Equal(employee, result);
    }

    [Fact]
    public void GetById_WithInvalidId_ThrowsArgumentException()
    {
        // Arrange
        var mockRepo = new Mock<IEmployeeRepository>();
        mockRepo
            .Setup(r => r.GetById(999))
            .Returns((Employee?)null);

        var service = new EmployeeService(mockRepo.Object);

        //// Act & Assert
        //var ex = Assert.Throws<ArgumentException>(() => service.GetById(999));
        //Assert.Equal("Invalid parameter value: 999 (Parameter 'id')", ex.Message);

        // Act
        var result = Record.Exception(() => service.GetById(999));

        // Assert
        Assert.IsType<ArgumentException>(result);
    }

    [Theory]
    [InlineData("anders@mail.com", true)] // Anders is hardcoded as VIP in EmplooyeeService
    [InlineData("ANDERS@mail.com", true)] // Case insensitive check
    [InlineData("lisa@ajax.com", false)] // Not a VIP email
    [InlineData("LISA@ajax.com", false)] // Case insensitive check   
    public void CheckIsVIP_WithVIPEmail_ReturnsExpectedResult(string email, bool expected)
    {
        var employee = new Employee { Email = email };
        var service = new EmployeeService(Mock.Of<IEmployeeRepository>());

        var result = service.CheckIsVIP(employee);

        Assert.Equal(expected, result);
    }
}
