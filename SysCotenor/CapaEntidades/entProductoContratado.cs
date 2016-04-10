using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entProductoContratado
    {
        public String ProCon_Id { get; set; }
        public entCliente Cliente { get; set; }
        public entProducto Producto { get; set; }
        public Double ProCon_Precio { get; set; }
        public String ProCon_Estado { get; set; }
        public DateTime ProCon_InicioVigencia { get; set; }
        public DateTime ProCon_FinVigencia { get; set; }
        public DateTime ProCon_FechaRegistro { get; set; }
        public String ProCon_IdUsuarioRegistro { get; set; }
        public DateTime ProCon_FechaModificacion { get; set; }
        public String ProCon_IdUsuarioModificacion { get; set; }

    }
}
