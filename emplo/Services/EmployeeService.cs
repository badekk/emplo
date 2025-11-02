using emplo.Models;

namespace emplo.Services;

public class EmployeeService
{
    public List<EmployeeStructure> FillEmployeesStructure(List<Employee> employees)
    {
        if (employees == null || employees.Count == 0)
            throw new ArgumentNullException(nameof(employees), "Employee list cannot be null or empty.");

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

    public int? GetSupieriorRowOfEmployee(List<EmployeeStructure> employeeStructureList, int employeeId, int superiorId)
    {
        var relation = employeeStructureList.FirstOrDefault(x => x.EmployeeId == employeeId && x.SuperiorId == superiorId);
        return relation?.RelationLevel;
    }
}
