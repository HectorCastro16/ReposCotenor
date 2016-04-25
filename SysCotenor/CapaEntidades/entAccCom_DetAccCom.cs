using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entAccCom_DetAccCom
    {
        public int Ad_ID { get; set; }
        public String Ad_Codigo { get; set; }
        public entAccionComercial AccionComercial { get; set; }
        public entDetalleAccionComercial DetalleAccionComercial { get; set; }
        public DateTime Ad_FechaRegistro { get; set; }
        public String Ad_UsuarioRegistro { get; set; }
        public DateTime Ad_FechaModificacion { get; set; }
        public String Ad_UsuarioModificacion { get; set; }

    }
}
