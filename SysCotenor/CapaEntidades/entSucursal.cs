using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entSucursal
    {
        public String Id { get; set; }
        public String Suc_Nombre { get; set; }
        public String Suc_Telefono { get; set; }
        public String Suc_Direccion { get; set; }
        public String Suc_Ciudad { get; set; }
        public Boolean Suc_Estado { get; set; }
        public DateTime Suc_FechaRegistro { get; set; }
        public String Suc_UsuarioRegistro { get; set; }
        public DateTime Suc_FechaModificacion { get; set; }
        public String Suc_UsuarioModificacion { get; set; }

    }
}
