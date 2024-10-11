using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TestProject.Base.Domain;
using TestProject.Domain.Model;

namespace TestProject.Domain
{
    public class AppDbContext : DbContext
    {
        public DbSet<ProcessCategory> ProcessCategories { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=TestProject1;Integrated Security=True;AttachDbFilename=C:\\source\\repos\\TestProject\\TestProject\\TestProject.mdf;User Instance=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProcessCategory>().HasKey(c => c.CategoryID);
            modelBuilder.Entity<Process>().HasKey(p => p.ProcessID);
            modelBuilder.Entity<Department>().HasKey(d => d.DepartmentID);

            modelBuilder.Entity<Process>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Processes)
                .HasForeignKey(p => p.CategoryID);

            modelBuilder.Entity<Process>()
                .HasOne(p => p.Department)
                .WithMany(d => d.Processes)
                .HasForeignKey(p => p.OwnerDepartmentID);
        }
    }

}
