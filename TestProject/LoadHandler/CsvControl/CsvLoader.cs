using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;
using System.Linq;
using TestProject.Domain;
using TestProject.LoadHandler.InterfaceCsv;
using TestProject.LoadHandler.CsvControl.Record;

namespace TestProject.LoadHandler.CsvControl
{
    public class CsvLoader
    {
        private readonly AppDbContext _context;

        public CsvLoader(AppDbContext context)
        {
            _context = context;
        }

        public void LoadCsvData(string csvFilePath, ICsvReaderFactory csvReaderFactory, ICsvRecordProcessor<CsvRecordProcess> recordProcessor)
        {
            try
            {
                using (var csvReader = csvReaderFactory.CreateCsvReader(csvFilePath))
                {
                    var records = csvReader.GetRecords<CsvRecordProcess>().ToList();

                    foreach (var record in records)
                    {
                        if (!recordProcessor.IsRecordValid(record))
                        {
                            Console.WriteLine($"Пропуск записи: {record.ProcessCode}, {record.ProcessName}");
                            continue;
                        }

                        var process = recordProcessor.ProcessRecord(record);
                        _context.Processes.Add(process);
                    }

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке данных из CSV: {ex.Message}");
            }
        }

    }

}


