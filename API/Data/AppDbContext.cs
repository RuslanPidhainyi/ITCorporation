using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeDetails> EmployeeDetails { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee_Project> Employee_Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1:1 Employee <-> EmployeeDetails
            modelBuilder.Entity<EmployeeDetails>()
                .HasIndex(ed => ed.EmployeeId)
                .IsUnique();

            modelBuilder.Entity<EmployeeDetails>()
                .HasOne(ed => ed.Employee)
                .WithOne(e => e.EmployeeDetails)
                .HasForeignKey<EmployeeDetails>(ed => ed.EmployeeId);

            // N:N Employee <-> Project (from Employee_Project)
            modelBuilder.Entity<Employee_Project>()
                .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

            modelBuilder.Entity<Employee_Project>()
                .HasOne(ep => ep.Employee)
                .WithMany(e => e.Employee_Projects)
                .HasForeignKey(ep => ep.EmployeeId);

            modelBuilder.Entity<Employee_Project>()
                .HasOne(ep => ep.Project)
                .WithMany(p => p.Employee_Projects)
                .HasForeignKey(ep => ep.ProjectId);
        }
    }
}