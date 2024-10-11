using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Domain.Model;

namespace TestProject.Base.Domain
{
    public class ProcessCategory
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Process> Processes { get; set; }
    }

}
