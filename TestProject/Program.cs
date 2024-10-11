using System;
using TestProject.Domain;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new AppDbContext())
        {
            var loader = new CsvLoader(context);
            loader.LoadCsvData("C:\\Users\\Администратор\\source\\repos\\TestProject\\TestProject\\Тестовые данные.CSV");
        }

        Console.WriteLine("Данные успешно загружены.");
    }
}
