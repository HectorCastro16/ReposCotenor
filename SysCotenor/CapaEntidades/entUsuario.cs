using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace CapaEntidades
{
    public class entUsuario
    {
        public int Usu_Id { get; set; }
        public String Usu_Codigo { get; set; }
        [Required]
        public entPersona Persona { get; set; }
        public entTipoUsuario TipoUsuario { get; set; }
        public entSucursal Sucursal { get; set; }

        public String Usu_Telefono { get; set; }

        [Required(ErrorMessage = "Usuario Requerido")]
        public String Usu_Login { get; set; }

        [Required(ErrorMessage = "Contraseña Requerida")]
        public String Usu_Password { get; set; }
        public String Usu_Estado { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Usu_FechaHasta { get; set; }
        public DateTime Usu_FechaRegistro { get; set; }
        public String Usu_UsuarioRegistro { get; set; }
        public DateTime Usu_FechaModificacion { get; set; }
        public String Usu_UsuarioModificacion { get; set; }
        public String usu_Config_Color { get; set; }

        // ATRIBUTOS REQUERIDOS 

        public int Contador { get; set; }

    }
}
