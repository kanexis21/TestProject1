using System;
using TestProject.Domain;
using TestProject.LoadHandler.CsvControl;

class Program
{
    // Получаем путь к директории проекта
    static string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;

    // Формируем путь к файлу базы данных
    static string DataCsvFilePath = Path.Combine(projectDirectory, "Тестовые данные.CSV");
    static void Main(string[] args)
    {
        using (var context = new AppDbContext())
        {
            var csvReaderFactory = new CsvReaderFactory();
            var recordProcessor = new CsvRecordProcessor(context);
            var loader = new CsvLoader(context);


            loader.LoadCsvData(DataCsvFilePath);
        }

        Console.WriteLine("Данные успешно загружены.");
    }
}

