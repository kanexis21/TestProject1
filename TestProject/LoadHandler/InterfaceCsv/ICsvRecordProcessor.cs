using TestProject.Domain.Model;
using TestProject.LoadHandler.CsvControl;

namespace TestProject.LoadHandler.InterfaceCsv
{
    public interface ICsvRecordProcessor
    {
        bool IsRecordValid(CsvRecord record);
        Process ProcessRecord(CsvRecord record);
    }
}