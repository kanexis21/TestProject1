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
        [Name("Категория процесса")]
        public string CategoryName { get; set; }

        [Name("Код процесса")]
        public string ProcessCode { get; set; }

        [Name("Наименование процесса")]
        public string ProcessName { get; set; }

        [Name("Подразделение-владелец процесса")]
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

        ClearDatabase();

        using (var reader = new StreamReader(csvFilePath, Encoding.GetEncoding("windows-1251")))
        using (var csv = new CsvReader(reader, config))
        {
            var records = csv.GetRecords<CsvRecord>().ToList();

            foreach (var record in records)
            {
                Debug.WriteLine($"Category: {record.CategoryName}, ProcessCode: {record.ProcessCode}, ProcessName: {record.ProcessName}, OwnerDepartmentName: {record.OwnerDepartmentName}");

                // Проверка на пустые значения
                if (string.IsNullOrWhiteSpace(record.CategoryName) ||
                    string.IsNullOrWhiteSpace(record.ProcessCode) ||
                    string.IsNullOrWhiteSpace(record.ProcessName))
                {
                    Debug.WriteLine($"Пропуск записи: {record.CategoryName}, {record.ProcessCode}, {record.ProcessName}");
                    continue; 
                }

                var existingProcess = _context.Processes
                    .FirstOrDefault(p => p.ProcessCode == record.ProcessCode && p.ProcessName == record.ProcessName);

                if (existingProcess != null)
                {
                    Debug.WriteLine($"Запись уже существует: {existingProcess.ProcessCode}");
                    continue; 
                }

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

    private void ClearDatabase()
    {
        _context.Processes.RemoveRange(_context.Processes);
        _context.Departments.RemoveRange(_context.Departments);
        _context.ProcessCategories.RemoveRange(_context.ProcessCategories);

        _context.SaveChanges();
    }

}
