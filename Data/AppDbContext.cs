using Microsoft.EntityFrameworkCore;
using TimeRecord.Models;

namespace TimeRecord.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Marking> Markings { get; set; }
        public DbSet<Company> Company { get; set; }
    }
}
