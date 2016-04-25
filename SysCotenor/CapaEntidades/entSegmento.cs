using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entSegmento
    {
        public int Seg_Id { get; set; }
        public String Seg_Codigo { get; set; }
        public String Seg_Nombre { get; set; }
        public String Seg_Descripcion { get; set; }
        public DateTime Seg_FechaRegistro { get; set; }
        public String Seg_UsuarioRegistro { get; set; }
        public DateTime Seg_FechaModificacion { get; set; }
        public String Seg_UsuarioModificacion { get; set; }

    }
}
