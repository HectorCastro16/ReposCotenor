using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entDetalleAccionComercial
    {
        public String DetAcc_ID { get; set; }
        public String DetAcc_Nombre { get; set; }
        public String DetAcc_Descripcion { get; set; }
        public DateTime DetAcc_FechaRegistro { get; set; }
        public String DetAcc_IdUsuarioRegistro { get; set; }
        public DateTime DetAcc_FechaModificacion { get; set; }
        public String DetAcc_IdUsuarioModificacion { get; set; }

    }
}
