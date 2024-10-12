using System.Diagnostics;
using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;
using System.Linq;
using TestProject.Domain;
using TestProject.LoadModul.CsvRecord;
using TestProject.LoadModul.InterfaceLoadModule;

public class CsvLoader : ICsvLoader
{
    private readonly AppDbContext _context;
    private readonly CsvRecordProcessor _recordProcessor;

    public CsvLoader(AppDbContext context)
    {
        _context = context;
        _recordProcessor = new CsvRecordProcessor(context);
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
                Debug.WriteLine($"Category: {record.CategoryName}, ProcessCode: {record.ProcessCode}, ProcessName: {record.ProcessName}, OwnerDepartmentName: {record.OwnerDepartmentName}");

                if (!_recordProcessor.IsRecordValid(record))
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

                var process = _recordProcessor.ProcessRecord(record);
                _context.Processes.Add(process);
            }

            _context.SaveChanges();
        }
    }
}
