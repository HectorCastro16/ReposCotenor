using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entPlazoAtencion
    {
        public int Pla_Id { get; set; }        
        public String Pla_Codigo { get; set; }
        public entAccionComercial AccionComercial { get; set; }
        public entProducto Producto { get; set; }
        public DateTime Pla_Tiempo { get; set; }
        public String Pla_Descripcion { get; set; }
        public DateTime Pla_FechaRegistro { get; set; }
        public String Pla_UsuarioRegistro { get; set; }
        public DateTime Pla_FechaModificacion { get; set; }
        public String Pla_UsuarioModificacion { get; set; }

    }
}
