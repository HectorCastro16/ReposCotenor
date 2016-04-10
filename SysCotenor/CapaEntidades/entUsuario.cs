using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entUsuario
    {
        public int Usu_Id { get; set; }
        public entPersona Persona { get; set; }
        public entTipoUsuario TipoUsuario { get; set; }
        public entSucursal Sucursal { get; set; }
        public String Usu_Login { get; set; }
        public String Usu_Password { get; set; }
        public String Usu_Estado { get; set; }
        public DateTime Usu_FechaHasta { get; set; }
        public DateTime Usu_FechaRegistro { get; set; }
        public String Usu_IdUsuarioRegistro { get; set; }
        public DateTime Usu_FechaModificacion { get; set; }
        public String Usu_IdUsuarioModificacion { get; set; }

    }
}
