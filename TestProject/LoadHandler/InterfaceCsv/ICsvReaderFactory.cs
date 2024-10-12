using CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.LoadHandler.InterfaceCsv
{
    public interface ICsvReaderFactory
    {
        CsvReader CreateCsvReader(string csvFilePath);
    }
}
