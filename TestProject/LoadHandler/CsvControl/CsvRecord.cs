using CsvHelper.Configuration.Attributes;
using TestProject.Domain;
using TestProject.Domain.Model;

namespace TestProject.LoadHandler.CsvControl
{
    public class CsvRecord
    {
        [Name("Категория процесса")]
        public string CategoryName { get; set; }

        [Name("Код процесса")]
        public string ProcessCode { get; set; }

        [Name("Наименование процесса")]
        public string ProcessName { get; set; }

        [Name("Подразделение-владелец процесса")]
        public string OwnerDepartmentName { get; set; }
    }

    public class CsvRecordProcessor
    {
        private readonly AppDbContext _context;

        public CsvRecordProcessor(AppDbContext context)
        {
            _context = context;
        }

        // Метод для обработки одной записи CSV
        public Process ProcessRecord(CsvRecord record)
        {
            // Создаем объект процесса
            var process = new Process
            {
                ProcessCode = record.ProcessCode,
                ProcessName = record.ProcessName,
                CategoryName = record.CategoryName,
                OwnerDepartmentName = record.OwnerDepartmentName
            };

            return process;
        }

        // Метод для проверки валидности записи
        public bool IsRecordValid(CsvRecord record)
        {
            // Проверяем, что ключевые поля не пусты
            return !string.IsNullOrWhiteSpace(record.CategoryName) &&
                   !string.IsNullOrWhiteSpace(record.ProcessCode) &&
                   !string.IsNullOrWhiteSpace(record.ProcessName);
        }
    }

}
