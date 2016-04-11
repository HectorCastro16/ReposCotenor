using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entGrupoComercial
    {
        public String Gru_ID { get; set; }
        public String Gru_Nombre { get; set; }
        public String Gru_Descripcion { get; set; }
        public DateTime Gru_FechaRegistro { get; set; }
        public String Gru_UsuarioRegistro { get; set; }
        public DateTime Gru_FechaModificacion { get; set; }
        public String Gru_UsuarioModificacion { get; set; }

    }
}
