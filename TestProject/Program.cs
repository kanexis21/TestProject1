using System;
using TestProject.Domain;
using TestProject.LoadHandler.CsvControl;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new AppDbContext())
        {
            var csvReaderFactory = new CsvReaderFactory();
            var recordProcessor = new CsvRecordProcessor(context);
            var loader = new CsvLoader(context, recordProcessor, csvReaderFactory);

            loader.LoadCsvData("Тестовые данные.CSV");
        }

        Console.WriteLine("Данные успешно загружены.");
    }
}

