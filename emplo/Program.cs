using emplo.Models;
using emplo.Services;
using System.Text.Json;

namespace emplo;

public class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("========== INITIALIZING EMPLOYEES ==========");

        // Creating employees
        var janKowalski = new Employee { Id = 1, Name = "Jan Kowalski" };
        var kamilNowak = new Employee { Id = 2, Name = "Kamil Nowak", Superior = janKowalski, SuperiorId = janKowalski.Id };
        var annaMariacka = new Employee { Id = 3, Name = "Anna Mariacka", Superior = janKowalski, SuperiorId = janKowalski.Id };
        var andrzejAbacki = new Employee { Id = 4, Name = "Andrzej Abacki", Superior = kamilNowak, SuperiorId = kamilNowak.Id };

        var employees = new List<Employee>
        {
            janKowalski,
            kamilNowak,
            annaMariacka,
            andrzejAbacki
        };

        // Singleton initialization
        var service = EmployeeServiceSingleton.Instance;
        service.Initialize(employees);

        var employeeService = new EmployeeService();

        // Method with sending list over to the method
        var employeeStructure = employeeService.FillEmployeesStructure(employees);
        var row1service = employeeService.GetSupieriorRowOfEmployee(employeeStructure, 2, 1);
        var row2service = employeeService.GetSupieriorRowOfEmployee(employeeStructure, 4, 3);
        var row3service = employeeService.GetSupieriorRowOfEmployee(employeeStructure, 4, 1);

        // Method using singleton and preserving state
        var row1singleton = EmployeeServiceSingleton.Instance.GetSuperiorRowOfEmployee(2, 1);
        var row2singleton = EmployeeServiceSingleton.Instance.GetSuperiorRowOfEmployee(4, 3);
        var row3singleton = EmployeeServiceSingleton.Instance.GetSuperiorRowOfEmployee(4, 1);
        string employeeListJson = JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented = true });
        string employeeStructureJson = JsonSerializer.Serialize(employeeStructure, new JsonSerializerOptions { WriteIndented = true });

        Console.WriteLine("==========Employee List==========");
        Console.WriteLine(employeeListJson);
        Console.WriteLine("==========Employee Structure========");
        Console.WriteLine(employeeStructureJson);
        Console.WriteLine("==========Supervisor Relation level========");
        Console.WriteLine($"Row1:{row1service}");
        Console.WriteLine($"Row2:{row2service}");
        Console.WriteLine($"Row3:{row3service}");
        Console.WriteLine("==========Supervisor Relation level singleton========");
        Console.WriteLine($"Row1:{row1singleton}");
        Console.WriteLine($"Row2:{row2singleton}");
        Console.WriteLine($"Row3:{row3singleton}");
        Console.WriteLine("==========END============");
    }
}
