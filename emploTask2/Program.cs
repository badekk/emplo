using emploTask2.Db;
using emploTask2.Models;
using emploTask2.Services;
using Microsoft.EntityFrameworkCore;

namespace emploTask2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

            using var context = new AppDbContext(options);

            SeedData(context);
            Console.WriteLine("===========Zapytanie 2A============");

            var zapytanieA = context.Employees
                .Where(emp => emp.Team.Name == ".NET" &&
                    emp.Vacations.Any(v => v.DateSince.Year == 2019))
                .Select(x => x.Name)
                .ToList();

            foreach(var emp in zapytanieA)
            {
                Console.WriteLine(emp);
            }
            Console.WriteLine("===========Zapytanie 2B============");

            var currentYear = DateTime.Now.Year;
            var today = DateTime.Now;

            var zapytanieB = context.Employees
                .Select(e => new
                {
                    EmployeeName = e.Name,
                    UsedDays = e.Vacations
                        .Where(v => v.DateSince.Year == currentYear && v.DateUntil < today)
                        .Sum(v => (int)(v.DateUntil - v.DateSince).TotalDays + 1)
                })
                .ToList();
            foreach (var emp in zapytanieB)
            {
                Console.WriteLine(emp);
            }

            Console.WriteLine("===========Zapytanie 2C============");

            var zapytanieC = context.Teams
                .Where(t => !t.Employees
                    .Any(e => e.Vacations
                        .Any(v => v.DateSince.Year == 2019 || v.DateUntil.Year == 2019)))
                .Select(t => new { t.Name })
                .ToList();
            foreach (var team in zapytanieC)
            {
                Console.WriteLine(team);
            }

            Console.WriteLine("===========Zadanie 3 / 4============");

            var service = new VacationServices();

            var jan = context.Employees.First(e => e.Name == "Jan Kowalski");
            var lr9 = context.Employees.First(e => e.Name == "Julia Lewandowska");
            var remainingDaysJan = service.CountFreeDaysForEmployee(jan, context.Vacations.ToList(), jan.VacationPackage);
            var remainingDaysLewy = service.CountFreeDaysForEmployee(lr9, context.Vacations.ToList(), lr9.VacationPackage);
            var canRequestHolidayJan = service.IfEmployeeCanRequestVacation(jan, context.Vacations.ToList(), jan.VacationPackage);
            var canRequestHolidayLewy = service.IfEmployeeCanRequestVacation(lr9, context.Vacations.ToList(), lr9.VacationPackage);

            Console.WriteLine($"Jan Kowalski has {remainingDaysJan} days of holiday to use.");
            Console.WriteLine($"Can he request holiday? {(canRequestHolidayJan ? "Yes" : "Nope, nununu")}");
            Console.WriteLine($"Julia Lewandowska has {remainingDaysLewy} days of holiday to use.");
            Console.WriteLine($"Can she request holiday? {(canRequestHolidayLewy ? "Yes" : "Nope, nununu")}");

            Console.WriteLine("===========The End============");
        }

        private static void SeedData(AppDbContext context)
        {
            var teamDotNet = new Team { Id = 1, Name = ".NET" };
            var teamFrontend = new Team { Id = 2, Name = "Frontend" };
            var teamHr = new Team { Id = 3, Name = "HR" };

            var package = new VacationPackage { Id = 1, Name = "Standard", GrantedDays = 26, Year = 2025 };

            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "Jan Kowalski", Team = teamDotNet, VacationPackage = package },
                new Employee { Id = 2, Name = "Anna Nowak", Team = teamDotNet, VacationPackage = package },
                new Employee { Id = 3, Name = "Kamil Wójcik", Team = teamDotNet, VacationPackage = package },
                new Employee { Id = 4, Name = "Piotr Zieliński", Team = teamDotNet, VacationPackage = package },
                new Employee { Id = 5, Name = "Marta Król", Team = teamDotNet, VacationPackage = package },
                new Employee { Id = 6, Name = "Ewa Kaczmarek", Team = teamDotNet, VacationPackage = package },
                new Employee { Id = 7, Name = "Łukasz Adamski", Team = teamDotNet, VacationPackage = package },
                new Employee { Id = 8, Name = "Karol Górski", Team = teamFrontend, VacationPackage = package },
                new Employee { Id = 9, Name = "Julia Lewandowska", Team = teamFrontend, VacationPackage = package },
                new Employee { Id = 10, Name = "Tomasz Pawlak", Team = teamHr, VacationPackage = package }
            };

            var vacations = new List<Vacation>
            {
                new Vacation { Id = 1, Employee = employees[0], DateSince = new DateTime(2019, 5, 1), DateUntil = new DateTime(2019, 5, 5) },
                new Vacation { Id = 2, Employee = employees[0], DateSince = new DateTime(2019, 7, 10), DateUntil = new DateTime(2019, 7, 12) },
                new Vacation { Id = 3, Employee = employees[7], DateSince = new DateTime(2019, 8, 15), DateUntil = new DateTime(2019, 8, 20) },
                new Vacation { Id = 4, Employee = employees[1], DateSince = new DateTime(2020, 6, 10), DateUntil = new DateTime(2020, 6, 15) },
                new Vacation { Id = 5, Employee = employees[3], DateSince = new DateTime(2019, 12, 1), DateUntil = new DateTime(2019, 12, 5) },
                new Vacation { Id = 6, Employee = employees[4], DateSince = new DateTime(2023, 7, 1), DateUntil = new DateTime(2023, 7, 14) },
                new Vacation { Id = 7, Employee = employees[8], DateSince = new DateTime(2019, 9, 1), DateUntil = new DateTime(2019, 9, 5) },
                new Vacation { Id = 8, Employee = employees[5], DateSince = new DateTime(2024, 3, 10), DateUntil = new DateTime(2024, 3, 20) },
                new Vacation { Id = 9, Employee = employees[9], DateSince = new DateTime(2024, 4, 5), DateUntil = new DateTime(2024, 4, 10) },
                new Vacation { Id = 10, Employee = employees[3], DateSince = new DateTime(2024, 8, 1), DateUntil = new DateTime(2024, 8, 15) },
                new Vacation { Id = 11, Employee = employees[0], DateSince = new DateTime(2025, 6, 1), DateUntil = new DateTime(2025, 6, 10) },
                new Vacation { Id = 12, Employee = employees[2], DateSince = new DateTime(2025, 1, 15), DateUntil = new DateTime(2025, 1, 20) },
                new Vacation { Id = 13, Employee = employees[6], DateSince = new DateTime(2025, 10, 1), DateUntil = new DateTime(2025, 10, 5) },
                new Vacation { Id = 14, Employee = employees[8], DateSince = new DateTime(2025, 2, 10), DateUntil = new DateTime(2025, 2, 12) },
                new Vacation { Id = 15, Employee = employees[0], DateSince = new DateTime(2025, 10, 15), DateUntil = new DateTime(2025, 10, 22) },
                new Vacation { Id = 16, Employee = employees[2], DateSince = new DateTime(2025, 12, 1), DateUntil = new DateTime(2025, 12, 5) },
                new Vacation { Id = 17, Employee = employees[6], DateSince = new DateTime(2025, 11, 20), DateUntil = new DateTime(2025, 11, 27) },
                new Vacation { Id = 18, Employee = employees[8], DateSince = new DateTime(2025, 12, 23), DateUntil = new DateTime(2025, 12, 25) },
                new Vacation { Id = 19, Employee = employees[0], DateSince = new DateTime(2025, 11, 25), DateUntil = new DateTime(2025, 12, 2) }
            };

            context.Teams.AddRange(teamDotNet, teamFrontend, teamHr);
            context.VacationPackages.Add(package);
            context.Employees.AddRange(employees);
            context.Vacations.AddRange(vacations);

            context.SaveChanges();
        }
    }
}
