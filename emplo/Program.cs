using emplo.Models;
using emplo.Services;

namespace emplo
{
    public class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("========== INITIALIZING EMPLOYEES ==========");

            // Tworzenie pracowników
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

            // Inicjalizacja singletona
            var service = EmployeeServiceSingleton.Instance;
            service.Initialize(employees);

            // Metoda z przekazaniem listy
            //var lista = employeeService.FillEmployeesStructure(employees);
            //var row1 = employeeService.GetSupieriorRowOfEmployee(lista,2,1);
            //var row2 = employeeService.GetSupieriorRowOfEmployee(lista,4,3);
            //var row3 = employeeService.GetSupieriorRowOfEmployee(lista,4,1);  
            // Metoda z singletonem i przechowaniem stanu
            var row1 = EmployeeServiceSingleton.Instance.GetSuperiorRowOfEmployee(2, 1);
            var row2 = EmployeeServiceSingleton.Instance.GetSuperiorRowOfEmployee(4, 3);
            var row3 = EmployeeServiceSingleton.Instance.GetSuperiorRowOfEmployee(4, 1);
            //string json = JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented = true });
            //string json2 = JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true });

            Console.WriteLine("==========START==========");
            //Console.WriteLine(json);
            Console.WriteLine("==========Continue========");
            //Console.WriteLine(json2);
            Console.WriteLine("==========Continue========");
            Console.WriteLine($"Row1:{row1}");
            Console.WriteLine($"Row2:{row2}");
            Console.WriteLine($"Row3:{row3}");
            Console.WriteLine("==========END============");


        }
    }
}
