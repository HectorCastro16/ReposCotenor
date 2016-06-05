using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entProvincia
    {
        public int idProv { get; set; }
        public String provincia { get; set; }
        public entDepartamento departamento { get; set; }

    }
}
