namespace emploTask2.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TeamId { get; set; }
    public int VacationPackageId {  get; set; }

    public Team Team { get; set; }
    public VacationPackage VacationPackage { get; set; }
    public ICollection<Vacation> Vacations { get; set; }
}
