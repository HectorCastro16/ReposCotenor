using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace CapaEntidades
{
    public class entPersona
    {
        public int Per_Id { get; set; }
        public String Per_Codigo { get; set; }
        //[Display(Name ="txtNombres")]
        public String Per_Nombres { get; set; }
        [Required]
        public String Per_Apellidos { get; set; }
        [Required]
        [StringLength(8)]
        public String Per_DNI { get; set; }
        public String Per_Celular { get; set; }
        //[DataType(DataType.EmailAddress)]
        public String Per_Correo { get; set; }
        //public String Per_Telefono { get; set; }
        [Required]
        public String Per_Direccion { get; set; }
        public String Per_Foto { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Per_FechaNacimiento { get; set; }
        [Required]
        public String Per_LugarNacimiento { get; set; }

        // Atributos de concatenados
        public String NombreCompleto
        {
            get { return Per_Nombres + " " + Per_Apellidos; }
        }

    }
}
