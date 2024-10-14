using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.LoadHandler.CsvControl.Record
{
    public abstract class CsvRecord
    {
        public abstract string CategoryName { get; set; }
        public abstract string ProcessCode { get; set; }
        public abstract string ProcessName { get; set; }
        public abstract string OwnerDepartmentName { get; set; }
    }
    public class CsvRecordProcess : CsvRecord
    {
        [Name("Категория процесса")]
        public override string CategoryName { get; set; }

        [Name("Код процесса")]
        public override string ProcessCode { get; set; }

        [Name("Наименование процесса")]
        public override string ProcessName { get; set; }

        [Name("Подразделение-владелец процесса")]
        public override string OwnerDepartmentName { get; set; }
    }
}
