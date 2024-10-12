using CsvHelper.Configuration.Attributes;
using TestProject.Base.Domain;
using TestProject.Domain.Model;
using TestProject.Domain;

namespace TestProject.LoadHandler.CsvControl
{
    public class CsvRecord
    {
        [Name("Категория процесса")]
        public string CategoryName { get; set; }

        [Name("Код процесса")]
        public string ProcessCode { get; set; }

        [Name("Наименование процесса")]
        public string ProcessName { get; set; }

        [Name("Подразделение-владелец процесса")]
        public string OwnerDepartmentName { get; set; }
    }

    public class CsvRecordProcessor
    {
        private readonly AppDbContext _context;

        public CsvRecordProcessor(AppDbContext context)
        {
            _context = context;
        }

        public TestProject.Domain.Model.Process ProcessRecord(CsvRecord record)
        {
            var category = _context.ProcessCategories
                .FirstOrDefault(c => c.CategoryName == record.CategoryName)
                ?? new ProcessCategory { CategoryName = record.CategoryName };

            Department department = null;
            if (!string.IsNullOrWhiteSpace(record.OwnerDepartmentName))
            {
                department = _context.Departments
                    .FirstOrDefault(d => d.DepartmentName == record.OwnerDepartmentName)
                    ?? new Department { DepartmentName = record.OwnerDepartmentName };
            }

            return new TestProject.Domain.Model.Process
            {
                ProcessCode = record.ProcessCode,
                ProcessName = record.ProcessName,
                Category = category,
                Department = department
            };
        }

        public bool IsRecordValid(CsvRecord record)
        {
            return !string.IsNullOrWhiteSpace(record.CategoryName) &&
                   !string.IsNullOrWhiteSpace(record.ProcessCode) &&
                   !string.IsNullOrWhiteSpace(record.ProcessName);
        }
    }
}
