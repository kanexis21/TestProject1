using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.LoadHandler.Encording.Interfaceses;

namespace TestProject.LoadHandler.Encording.Implimentations
{
    public class Windows1251EncodingStrategy : ICsvEncodingStrategy
    {
        public Encoding GetEncoding()
        {
            return Encoding.GetEncoding("windows-1251");
        }
    }
}
