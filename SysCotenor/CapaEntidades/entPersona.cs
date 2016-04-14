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
        public String Per_Nombres { get; set; }
        public String Per_Apellidos { get; set; }
        [StringLength(8)]
        public String Per_DNI { get; set; }
        public String Per_Telefono { get; set; }
        public String Per_Direccion { get; set; }
        public String Per_Foto { get; set; }
        public DateTime Per_FechaNacimiento { get; set; }
        public String Per_LugarNacimiento { get; set; }

    }
}
