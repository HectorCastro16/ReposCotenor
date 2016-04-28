using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entConfiguracion
    {
        public int ConfiguracionID { get; set; }
        public entUsuario Usu_Id { get; set; }
        public char configuracionTema { get; set; }
    }
}
