using emploTask2.Models;
using emploTask2.Services;

namespace emploTask2.Tests;

[Category("Unit")]
[TestFixture]
public class VacationServiceTests
{
    private VacationServices _service;

    [SetUp]
    public void Setup()
    {
        _service = new VacationServices();
    }

    [Test]
    public void employee_can_request_vacation()
    {
        // Arrange
        var employee = new Employee { Id = 1, Name = "Jan Kowalski" };
        var package = new VacationPackage { GrantedDays = 26, Year = 2025 };
        var vacations = new List<Vacation>
            {
                new Vacation { EmployeeId = 1, DateSince = new DateTime(2025, 6, 1), DateUntil = new DateTime(2025, 6, 10) }
            };

        // Act
        var canRequest = _service.IfEmployeeCanRequestVacation(employee, vacations, package);

        // Assert
        Assert.IsTrue(canRequest);
    }

    [Test]
    public void employee_cant_request_vacation()
    {
        // Arrange
        var employee = new Employee { Id = 1, Name = "Jan Kowalski" };
        var package = new VacationPackage { GrantedDays = 5, Year = 2025 };
        var vacations = new List<Vacation>
            {
                new Vacation { EmployeeId = 1, DateSince = new DateTime(2025, 1, 1), DateUntil = new DateTime(2025, 1, 10) }
            };

        // Act
        var canRequest = _service.IfEmployeeCanRequestVacation(employee, vacations, package);

        // Assert
        Assert.IsFalse(canRequest);
    }
}