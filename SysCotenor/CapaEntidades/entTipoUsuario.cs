using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entTipoUsuario
    {
        public String TipUsu_Id { get; set; }
        public String TipUsu_Nombre { get; set; }
        public String TipUsu_Descripcion { get; set; }
        public DateTime TipUsu_FechaRegistro { get; set; }
        public String TipUsu_UsuarioRegistro { get; set; }
        public DateTime TipUsu_FechaModificacion { get; set; }
        public String TipUsu_UsuarioModificacion { get; set; }

    }
}
