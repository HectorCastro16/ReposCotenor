using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
   public class entRegLamadas
    {
        public int rll_id { get; set; }
        public entCliente_Telefono cliente_telef { get; set; }
        public String rll_resultado { get; set; }
        public entAccionComercial accioncomercial { get; set; }
        public entProducto producto { get; set; }
        public DateTime rll_fechahor_reg { get; set; }
        public String rll_estado { get; set; }
        public String rll_observaciones { get; set; }
        public entUsuario usuario { get; set; }
        public entAsigncionLlamadas assllamadas { get; set; }
    }
}
