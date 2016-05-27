using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
   public class entPersona_Telefono
    {
        public int PerTel_Id { get; set; }
        public entPersona persona { get; set; }
        public entTelefono telefono { get; set; }
        public DateTime PerTel_FechaRegistro { get; set; }
        public String PerTel_UsuarioRegistro { get; set; }
        
    }
}
