using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entCliente
    {
        public String Cli_Id { get; set; }
        public entPersona Persona { get; set; }
        public entSegmento Segmento { get; set; }
        public String Cli_Ruc { get; set; }
        public String Cli_Distrito { get; set; }
        public String Cli_Provincia { get; set; }
        public String Cli_Depardamento { get; set; }
        public DateTime Cli_FechaRegistro { get; set; }
        public String Cli_IdUsuarioRegistro { get; set; }
        public DateTime Cli_FechaModificacion { get; set; }
        public String Cli_IdUsuarioModificacion { get; set; }

    }
}
