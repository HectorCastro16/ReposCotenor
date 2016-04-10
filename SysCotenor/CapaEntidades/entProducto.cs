using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entProducto
    {
        public String Pro_ID { get; set; }
        public entCategoria Categoria { get; set; }
        public entPrecio Precio { get; set; }
        public String Pro_Nombre { get; set; }
        public String Pro_Descripcion { get; set; }
        public String Pro_Imagen { get; set; }
        public DateTime Pro_FechaRegistro { get; set; }
        public String Pro_IdUsuarioRegistro { get; set; }
        public DateTime Pro_FechaModificacion { get; set; }
        public String Pro_IdUsuarioModificacion { get; set; }

    }
}
