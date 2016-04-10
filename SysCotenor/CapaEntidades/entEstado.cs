using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entEstado
    {
        public String Est_Id { get; set; }
        public String Est_Nombre { get; set; }
        public String Est_Descripcion { get; set; }
        public DateTime Est_FechaRegistro { get; set; }
        public String Est_IdUsuarioRegistro { get; set; }
        public DateTime Est_FechaModificacion { get; set; }
        public String Est_IdUsuarioModificacion { get; set; }

    }
}
