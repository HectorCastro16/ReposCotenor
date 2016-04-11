using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entGruCom_Categoria
    {
        public String Gc_Id { get; set; }
        public entGrupoComercial GrupoComercial { get; set; }
        public entCategoria Categoria { get; set; }
        public DateTime Gc_FechaRegistro { get; set; }
        public String Gc_UsuarioRegistro { get; set; }
        public DateTime Gc_FechaModificacion { get; set; }
        public String Gc_UsuarioModificacion { get; set; }

    }
}
