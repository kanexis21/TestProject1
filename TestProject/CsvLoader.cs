using System.Diagnostics;
using System.Formats.Asn1;
using System.Globalization;
using System.Text;
using System;

public class CsvLoader
{
    private readonly AppDbContext _context;

    public CsvLoader(AppDbContext context)
    {
        _context = context;
    }

    public void LoadCsvData(string csvFilePath)
    {
        using (var reader = new StreamReader(csvFilePath, Encoding.GetEncoding("windows-1251")))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Configuration.HeaderValidated = null;
            csv.Configuration.MissingFieldFound = null;

            var records = csv.GetRecords<CsvRecord>().ToList();

            foreach (var record in records)
            {
                var category = _context.ProcessCategories
                    .FirstOrDefault(c => c.CategoryName == record.CategoryName)
                    ?? new ProcessCategory { CategoryName = record.CategoryName };

                var department = _context.Departments
                    .FirstOrDefault(d => d.DepartmentName == record.OwnerDepartmentName)
                    ?? new Department { DepartmentName = record.OwnerDepartmentName };

                var process = new Process
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

public class CsvRecord
{
    public string CategoryName { get; set; }
    public string ProcessCode { get; set; }
    public string ProcessName { get; set; }
    public string OwnerDepartmentName { get; set; }
}
