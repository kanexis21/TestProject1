using CsvHelper.Configuration.Attributes;
using System.Diagnostics;
using TestProject.Domain;
using TestProject.Domain.Model;
using TestProject.LoadHandler.CsvControl.Record;
using TestProject.LoadHandler.InterfaceCsv;
using Process = TestProject.Domain.Model.Process;

namespace TestProject.LoadHandler.CsvControl
{
    public class CsvRecordProcessor : ICsvRecordProcessor<CsvRecordProcess>
    {
        private readonly AppDbContext _context;

        public CsvRecordProcessor(AppDbContext context)
        {
            _context = context;
        }

        public Process ProcessRecord(CsvRecordProcess record)
        {
            var process = new Process
            {
                ProcessCode = record.ProcessCode,
                ProcessName = record.ProcessName,
                CategoryName = record.CategoryName,
                OwnerDepartmentName = record.OwnerDepartmentName
            };

            return process;
        }

        public bool IsRecordValid(CsvRecordProcess record)
        {
            return !string.IsNullOrWhiteSpace(record.CategoryName) &&
                   !string.IsNullOrWhiteSpace(record.ProcessCode) &&
                   !string.IsNullOrWhiteSpace(record.ProcessName);
        }
    }


}
