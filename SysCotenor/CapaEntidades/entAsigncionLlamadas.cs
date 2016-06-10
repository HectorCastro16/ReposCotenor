using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entAsigncionLlamadas
    {

        public int Asi_Id { get; set; }
        public entUsuario Asi_Usu_Id { get; set; }
        public entCliente_Telefono ClienteTelefono{ get; set; }
        public Boolean Asi_Estado { get; set; }
        public DateTime Asi_FechaRegistro { get; set; }
        public String Asi_UsuarioRegistro { get; set; }
        public DateTime Asi_FechaModificacion { get; set; }
        public String Asi_UsuarioModificacion { get; set; }
    }
}
