using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entSegmento
    {
        public String Seg_Id { get; set; }
        public String Seg_Nombre { get; set; }
        public String Seg_Descripcion { get; set; }
        public DateTime Seg_FechaRegistro { get; set; }
        public String Seg_IdUsuarioRegistro { get; set; }
        public DateTime Seg_FechaModificacion { get; set; }
        public String Seg_IdUsuarioModificacion { get; set; }

    }
}
