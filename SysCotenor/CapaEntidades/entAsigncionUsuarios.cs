using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entAsigncionUsuarios
    {
        public String AsiUsu_Id { get; set; }
        public entUsuario UsuarioSuper { get; set; }
        public entUsuario UsuarioTrabajador { get; set; }
        public Boolean AsiUsu_Estado { get; set; }
        public DateTime AsiUsu_FechaRegistro { get; set; }
        public String AsiUsu_UsuarioRegistro { get; set; }
        public DateTime AsiUsu_FechaModificacion { get; set; }
        public String AsiUsu_UsuarioModificacion { get; set; }
    }
}
