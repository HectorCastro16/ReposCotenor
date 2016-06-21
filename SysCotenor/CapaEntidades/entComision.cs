using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
   public class entComision
    {
        public int detC_id { get; set; }
        public entAccionComercial acc_Comercial { get; set; }
        public entProducto producto { get; set; }
        public int detC_ValorUnidades { get; set; }
        public double detC_ValorSoles { get; set; }
    }
}
