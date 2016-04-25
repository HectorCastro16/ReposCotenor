using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CapaEntidades
{
    public class entTipoUsuario
    {
        public int TipUsu_Id { get; set; }
        public String TipUsu_Codigo { get; set; }
        [Required]
        public String TipUsu_Nombre { get; set; }
        public String TipUsu_Descripcion { get; set; }
        public DateTime TipUsu_FechaRegistro { get; set; }
        public String TipUsu_UsuarioRegistro { get; set; }
        public DateTime TipUsu_FechaModificacion { get; set; }
        public String TipUsu_UsuarioModificacion { get; set; }


        //Atributos COncatendos
        public String CodNomTipUsu {
            get { return TipUsu_Codigo + " - " + TipUsu_Nombre; }
        }
    }
}
