using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entUsuarioSecurity
    {
        public int UsuarioSecurity { get; set; }
        public String UsuarioID { get; set; }
        public String Username { get; set; }
        public DateTime UltimoAcceso { get; set; }
        public String IPAcceso { get; set; }

    }
}
