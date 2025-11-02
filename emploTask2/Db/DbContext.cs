using Microsoft.EntityFrameworkCore;
using emploTask2.Models;

namespace emploTask2.Db;

public class AppDbContext : DbContext
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<VacationPackage> VacationPackages => Set<VacationPackage>();
    public DbSet<Vacation> Vacations => Set<Vacation>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}