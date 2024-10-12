using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.LoadHandler.InterfaceCsv;

namespace TestProject.LoadHandler.CsvControl
{
    public class CsvReaderFactory : ICsvReaderFactory
    {
        public CsvReader CreateCsvReader(string csvFilePath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Encoding = Encoding.GetEncoding("windows-1251"),
                HeaderValidated = null,
                MissingFieldFound = null,
                Delimiter = ";"
            };

            var reader = new StreamReader(csvFilePath, Encoding.GetEncoding("windows-1251"));
            return new CsvReader(reader, config);
        }
    }
}
