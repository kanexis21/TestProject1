using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.LoadHandler.CsvControl.Record
{
    public abstract class CsvRecordProcess
    {
        [Name("Категория процесса")]
        public abstract string CategoryName { get; set; }

        [Name("Код процесса")]
        public abstract string ProcessCode { get; set; }

        [Name("Наименование процесса")]
        public abstract string ProcessName { get; set; }

        [Name("Подразделение-владелец процесса")]
        public abstract string OwnerDepartmentName { get; set; }
    }
}
