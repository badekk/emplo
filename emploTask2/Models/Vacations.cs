namespace emploTask2.Models;

public class Vacation
{
    public int Id { get; set; }
    public DateTime DateSince { get; set; }
    public DateTime DateUntil { get; set; }
    public int NumberOfHours { get; set; }
    public bool IsPartialVacation { get; set; }
    public int EmployeeId { get; set; }

    public Employee Employee { get; set; }
}