using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entAsignacion
    {
        public String Asi_Id { get; set; }
        public entUsuario Usuario { get; set; }
        public entCliente Cliente { get; set; }
        public Boolean Asi_Estado { get; set; }
        public DateTime Asi_FechaRegistro { get; set; }
        public String Asi_UsuarioRegistro { get; set; }
        public DateTime Asi_FechaModificacion { get; set; }
        public String Asi_UsuarioModificacion { get; set; }

    }
}
