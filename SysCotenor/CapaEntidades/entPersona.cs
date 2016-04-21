using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace CapaEntidades
{
    public class entPersona
    {
        public String Per_Id { get; set; }
        [Required]
        //[Display(Name ="txtNombres")]
        public String Per_Nombres { get; set; }
        [Required]
        public String Per_Apellidos { get; set; }
        [Required]
        [StringLength(8)]
        public String Per_DNI { get; set; }
        public String Per_Celular { get; set; }
        public String Per_Correo { get; set; }
        [Required]
        public String Per_Telefono { get; set; }
        [Required]
        public String Per_Direccion { get; set; }
        public String Per_Foto { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Per_FechaNacimiento { get; set; }
        [Required]
        public String Per_LugarNacimiento { get; set; }

        // Atributos de concatenados
        public String NombreCompleto {
            get { return Per_Nombres + " " + Per_Apellidos; }
        }

    }
}
