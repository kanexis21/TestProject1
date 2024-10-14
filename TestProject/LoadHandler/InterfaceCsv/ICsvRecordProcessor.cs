using TestProject.Domain.Model;
using TestProject.LoadHandler.CsvControl;

namespace TestProject.LoadHandler.InterfaceCsv
{
    public interface ICsvRecordProcessor<TRecord>
    {
        bool IsRecordValid(TRecord record);
        Process ProcessRecord(TRecord record);
    }
}