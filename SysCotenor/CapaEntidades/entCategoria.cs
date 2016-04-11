using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entCategoria
    {
        public String  Cat_Id { get; set; }
        public String Cat_Nombre { get; set; }
        public String Cat_Descripcion { get; set; }
        public DateTime Cat_FechaRegistro { get; set; }
        public String Cat_UsuarioRegistro { get; set; }
        public DateTime Cat_FechaModificacion { get; set; }
        public String Cat_UsuarioModificacion { get; set; }

    }
}
