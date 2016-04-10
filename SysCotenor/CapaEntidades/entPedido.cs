using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entPedido
    {
        public String Ped_Id { get; set; }
        public entCliente Cliente { get; set; }
        public entUsuario Usuario { get; set; }
        public entEstado Estado { get; set; }
        public entAccionComercial AccionComercial { get; set; }
        public String Ped_Dir_Inst { get; set; }
        public Double Ped_Total { get; set; }
        public DateTime Ped_FechaRegistro { get; set; }
        public String Ped_IdUsuarioRegistro { get; set; }
        public DateTime Ped_FechaModificacion { get; set; }
        public String Ped_IdUsuarioModificacion { get; set; }

    }
}
