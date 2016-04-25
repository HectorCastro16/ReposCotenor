using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entDetAcc_GruCom
    {
        public int Ag_Id { get; set; }
        public String Ag_Codigo { get; set; }
        public entDetalleAccionComercial DetalleAccionComercial { get; set; }
        public entGrupoComercial GrupoComercial { get; set; }
        public DateTime Ag_FechaRegistro { get; set; }
        public String Ag_UsuarioRegistro { get; set; }
        public DateTime Ag_FechaModificacion { get; set; }
        public String Ag_UsuarioModificacion { get; set; }

    }
}
