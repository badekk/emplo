using emploTask2.Models;

namespace emploTask2.Services;

public class VacationServices
{
    public int CountFreeDaysForEmployee(Employee employee, List<Vacation> vacations, VacationPackage vacationPackage)
    {
        if (employee == null)
            throw new ArgumentNullException(nameof(employee));
        if (vacationPackage == null)
            throw new ArgumentNullException(nameof(vacationPackage));
        if (vacations == null)
            vacations = new List<Vacation>();

        var currentYear = DateTime.Now.Year;
        var endOfThisYear = new DateTime(DateTime.Now.Year, 12, 31, 23, 59, 59);

        var usedDays = vacations
            .Where(v => v.EmployeeId == employee.Id
                && v.DateSince.Year == currentYear 
                && v.DateUntil < endOfThisYear)
            .Sum(v => (int)(v.DateUntil - v.DateSince).TotalDays + 1);
            
        var remainingDays = vacationPackage.GrantedDays - usedDays;

        return remainingDays < 0 ? 0 : remainingDays;
    }

    public bool IfEmployeeCanRequestVacation(Employee employee, List<Vacation> vacations, VacationPackage vacationPackage)
    {
        var reminderDays = CountFreeDaysForEmployee(employee, vacations, vacationPackage);

        return reminderDays > 0;
    }
}
