using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Domain
{
    public class DatabaseManager
    {
        private readonly AppDbContext _context;

        public DatabaseManager(AppDbContext context)
        {
            _context = context;
        }

        public void ClearDatabase()
        {
            _context.Processes.RemoveRange(_context.Processes);
            _context.Departments.RemoveRange(_context.Departments);
            _context.ProcessCategories.RemoveRange(_context.ProcessCategories);
            _context.SaveChanges();
        }
    }

}
