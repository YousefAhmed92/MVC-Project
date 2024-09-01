using Company.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.Data.Contexts
{
    public class CompanyDBContext : DbContext
    {
        public CompanyDBContext()
        {
        }

        public CompanyDBContext(DbContextOptions<CompanyDBContext> options) : base(options)
        {
        }

        // Define DbSets for your entities
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
