using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entCliente
    {
        public int Cli_Id { get; set; }
        public String Cli_Codigo { get; set; }
        public entPersona_Telefono Persona_telef { get; set; }
        public entSegmento Segmento { get; set; }
        public String Cli_Ruc { get; set; }
        public String Cli_Empresa { get; set; }
        public entDistrito Cli_Distrito { get; set; }
        public entProvincia Cli_Provincia { get; set; }
        public entDepartamento Cli_Depardamento { get; set; }
        public DateTime Cli_FechaRegistro { get; set; }
        public String Cli_UsuarioRegistro { get; set; }
        public DateTime Cli_FechaModificacion { get; set; }
        public String Cli_UsuarioModificacion { get; set; }

    }
}
