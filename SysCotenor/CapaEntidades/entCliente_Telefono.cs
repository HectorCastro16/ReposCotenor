using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace CapaEntidades
{
    public class entCliente_Telefono
    {
        public int CliTel_Id { get; set; }
        public entCliente Cliente { get; set; }
        public entTelefono Telefono { get; set; }
        public String CliTel_EstadoPertenencia { get; set; }
        public DateTime CliTel_FechaRegistro { get; set; }
        public String CliTel_UsuarioRegistro { get; set; }
        public DateTime CliTel_FechaModificacion { get; set; }
        public String CliTel_UsuarioModificacion { get; set; }

        // Para la Asignacion
        public bool IsSelected { get; set; }

        // numero asignado
        public int AsiUsu { get; set; }

    }
}
