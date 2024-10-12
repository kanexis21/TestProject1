using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TestProject.Domain.Model;

namespace TestProject.Domain
{
    public class AppDbContext : DbContext
    {
        public DbSet<Process> Processes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;

            // Формируем путь к файлу базы данных
            string databaseFilePath = Path.Combine(projectDirectory, "TestProject.mdf");

            optionsBuilder.UseSqlServer($"Server=localhost;Database=TestProject;Integrated Security=True;AttachDbFilename={databaseFilePath};");
        }
    }

}
