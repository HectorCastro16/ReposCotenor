using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entPrecio
    {
        public int Pre_ID { get; set; }
        public String Pre_Codigo { get; set; }
        public Double Pre_producto { get; set; }
        public DateTime Pre_FechaRegistro { get; set; }
        public String Pre_UsuarioRegistro { get; set; }
        public DateTime Pre_FechaModificacion { get; set; }
        public String Pre_UsuarioModificacion { get; set; }
    }
}
