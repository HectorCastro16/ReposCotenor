using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entAccionComercial
    {
        public int Acc_Id { get; set; }        
        public String Acc_Codigo { get; set; }
        public String Acc_Nombre { get; set; }
        public String Acc_Descripcion { get; set; }
        public DateTime Acc_FechaRegistro { get; set; }
        public String Acc_UsuarioRegistro { get; set; }
        public DateTime Acc_FechaModificacion { get; set; }
        public String Acc_UsuarioModificacion { get; set; }
        public entComision comision { get; set; }
    }
}
