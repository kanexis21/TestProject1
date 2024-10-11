using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Base.Domain;

namespace TestProject.Domain.Model
{
    public class Process
    {
        public int ProcessID { get; set; }
        public string ProcessCode { get; set; }
        public string ProcessName { get; set; }
        public int CategoryID { get; set; }
        public ProcessCategory Category { get; set; }
        public int? OwnerDepartmentID { get; set; }
        public Department Department { get; set; }
    }
}
