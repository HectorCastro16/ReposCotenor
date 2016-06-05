using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
  public  class entArticulo
    {

        public int Art_Id { get; set; }
        public DateTime Art_FechaPublicacion { get; set; }
        public entUsuario usuario { get; set; }
        public String Art_Image { get; set; }
        public String Art_Url { get; set; }
        public String Art_Titulo { get; set; }
        public String Art_Descripcion { get; set; }
        

    }
}
