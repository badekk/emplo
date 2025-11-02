namespace emplo.Models;

public class EmployeeStructure
{
    public int EmployeeId { get; set; }
    public int? SuperiorId { get; set; }
    public int RelationLevel { get; set; }
}
