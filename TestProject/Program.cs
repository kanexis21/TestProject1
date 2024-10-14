using System;
using TestProject.Domain;
using TestProject.LoadHandler.CsvControl;
using TestProject.LoadHandler.Encording.Implimentations;
using TestProject.LoadHandler.Encording.Interfaceses;
using TestProject.LoadHandler.InterfaceCsv;

class Program
{
    static string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
    static string DataCsvFilePath = Path.Combine(projectDirectory, "Тестовые данные.CSV");
    static void Main(string[] args)
    {
        try
        {
            using (var context = new AppDbContext())
            {
                var encodingStrategy = new Windows1251EncodingStrategy();
                var csvReaderFactory = new CsvReaderFactory(encodingStrategy);

                var recordProcessor = new CsvRecordProcessor(context);
                var loader = new CsvLoader(context);

                loader.LoadCsvData(DataCsvFilePath, csvReaderFactory, recordProcessor);
            }

            Console.WriteLine("Данные успешно загружены.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
        }
    }

}

