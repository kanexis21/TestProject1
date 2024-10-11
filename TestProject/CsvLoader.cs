using System.Diagnostics;
using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using TestProject.Domain;
using TestProject.Domain.Model;
using TestProject.Base.Domain;
using Microsoft.EntityFrameworkCore;
using CsvHelper.Configuration.Attributes;

public class CsvLoader
{
    public class CsvRecord
    {
        [Name("Категория")] 
        public string CategoryName { get; set; }

        [Name("Код процесса")] 
        public string ProcessCode { get; set; }

        [Name("Название процесса")]
        public string ProcessName { get; set; }

        [Name("Отдел")]
        public string OwnerDepartmentName { get; set; }
    }


private readonly AppDbContext _context;

    public CsvLoader(AppDbContext context)
    {
        _context = context;
    }

    public void LoadCsvData(string csvFilePath)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.GetEncoding("windows-1251"),
            HeaderValidated = null,
            MissingFieldFound = null, 
            Delimiter = ";" 
        };

        using (var reader = new StreamReader(csvFilePath, Encoding.GetEncoding("windows-1251")))
        using (var csv = new CsvReader(reader, config))
        {

            var records = csv.GetRecords<CsvRecord>().ToList();

            foreach (var record in records)
            {
                var category = _context.ProcessCategories
                    .FirstOrDefault(c => c.CategoryName == record.CategoryName)
                    ?? new ProcessCategory { CategoryName = record.CategoryName };

                var department = _context.Departments
                    .FirstOrDefault(d => d.DepartmentName == record.OwnerDepartmentName)
                    ?? new Department { DepartmentName = record.OwnerDepartmentName };

                var process = new TestProject.Domain.Model.Process
                {
                    ProcessCode = record.ProcessCode,
                    ProcessName = record.ProcessName,
                    Category = category,
                    Department = department
                };

                _context.Processes.Add(process);
            }

            _context.SaveChanges();
        }
    }

}
