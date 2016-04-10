using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entDetAcc_GruCom
    {
        public String Ag_Id { get; set; }
        public entDetalleAccionComercial DetalleAccionComercial { get; set; }
        public entGrupoComercial GrupoComercial { get; set; }
        public DateTime Ag_FechaRegistro { get; set; }
        public String Ag_IdUsuarioRegistro { get; set; }
        public DateTime Ag_FechaModificacion { get; set; }
        public String Ag_IdUsuarioModificacion { get; set; }

    }
}
