using System;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new AppDbContext())
        {
            var loader = new CsvLoader(context);
            loader.LoadCsvData("path_to_your_file.csv");
        }

        Console.WriteLine("Данные успешно загружены.");
    }
}
