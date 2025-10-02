using Microsoft.EntityFrameworkCore;
using RegistrarPonto.Models;

namespace RegistrarPonto.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Marking> Markings { get; set; }
    }
}
