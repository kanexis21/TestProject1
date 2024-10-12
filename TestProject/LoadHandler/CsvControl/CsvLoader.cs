using System.Diagnostics;
using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;
using System.Linq;
using TestProject.Domain;
using TestProject.LoadHandler.InterfaceCsv;

namespace TestProject.LoadHandler.CsvControl
{
    public class CsvLoader : ICsvLoader
    {
        private readonly AppDbContext _context;
        private readonly CsvRecordProcessor _recordProcessor;
        private readonly ICsvReaderFactory _csvReaderFactory;

        public CsvLoader(AppDbContext context, CsvRecordProcessor recordProcessor, ICsvReaderFactory csvReaderFactory)
        {
            _context = context;
            _recordProcessor = recordProcessor;
            _csvReaderFactory = csvReaderFactory;
        }

        public void LoadCsvData(string csvFilePath)
        {
            using (var reader = _csvReaderFactory.CreateCsvReader(csvFilePath))
            {
                var records = reader.GetRecords<CsvRecord>().ToList();

                foreach (var record in records)
                {
                    if (!_recordProcessor.IsRecordValid(record))
                    {
                        continue;
                    }

                    var existingProcess = _context.Processes
                        .FirstOrDefault(p => p.ProcessCode == record.ProcessCode && p.ProcessName == record.ProcessName);

                    if (existingProcess != null)
                    {
                        continue;
                    }

                    var process = _recordProcessor.ProcessRecord(record);
                    _context.Processes.Add((TestProject.Domain.Model.Process)process);
                }

                _context.SaveChanges();
            }
        }
    }
}

