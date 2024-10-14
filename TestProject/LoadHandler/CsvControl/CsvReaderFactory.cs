using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.LoadHandler.InterfaceCsv;
using TestProject.LoadHandler.Encording.Interfaceses;

namespace TestProject.LoadHandler.CsvControl
{
    public class CsvReaderFactory : ICsvReaderFactory
    {
        private readonly ICsvEncodingStrategy _encodingStrategy;

        public CsvReaderFactory(ICsvEncodingStrategy encodingStrategy)
        {
            _encodingStrategy = encodingStrategy;
        }

        public CsvReader CreateCsvReader(string csvFilePath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Encoding = _encodingStrategy.GetEncoding(),
                HeaderValidated = null,
                MissingFieldFound = null,
                Delimiter = ";"
            };

            var reader = new StreamReader(csvFilePath, _encodingStrategy.GetEncoding());
            return new CsvReader(reader, config);
        }
    }

}
