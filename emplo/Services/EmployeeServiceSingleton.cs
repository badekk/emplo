using emplo.Models;

namespace emplo.Services;

public sealed class EmployeeServiceSingleton
{
    private static readonly Lazy<EmployeeServiceSingleton> _instance = new(() => new EmployeeServiceSingleton());
    public static EmployeeServiceSingleton Instance => _instance.Value;

    private List<EmployeeStructure> _employeesStructure = new();

    private EmployeeServiceSingleton() { }

    public void Initialize(List<Employee> employees)
    {
        if (employees == null || employees.Count == 0)
            throw new ArgumentNullException(nameof(employees), "Employee list cannot be null or empty.");

        if (_employeesStructure.Any())
            return;

        _employeesStructure = FillEmployeesStructure(employees);
    }

    private List<EmployeeStructure> FillEmployeesStructure(List<Employee> employees)
    {
        var structures = new List<EmployeeStructure>();

        foreach (var employee in employees)
        {
            var level = 1;
            var superior = employee.Superior;

            while (superior != null)
            {
                structures.Add(new EmployeeStructure
                {
                    EmployeeId = employee.Id,
                    SuperiorId = superior.Id,
                    RelationLevel = level
                });

                level++;
                superior = superior.Superior;
            }
        }

        return structures;
    }

    public int? GetSuperiorRowOfEmployee(int employeeId, int superiorId)
    {
        var relation = _employeesStructure
            .FirstOrDefault(r => r.EmployeeId == employeeId && r.SuperiorId == superiorId);

        return relation?.RelationLevel;
    }

    public IReadOnlyList<EmployeeStructure> GetAllRelations() => _employeesStructure.AsReadOnly();
}
