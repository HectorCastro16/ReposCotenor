using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entAccionComercial
    {
        public String Acc_Id { get; set; }
        public String Acc_Nombre { get; set; }
        public String Acc_Descripcion { get; set; }
        public DateTime Acc_FechaRegistro { get; set; }
        public String Acc_IdUsuarioRegistro { get; set; }
        public DateTime Acc_FechaModificacion { get; set; }
        public String Acc_IdUsuarioModificacion { get; set; }

    }
}
