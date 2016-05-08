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
        public String Asi_Codigo { get; set; }
        public entUsuario Usuario { get; set; }
        public String Asi_NumTelf { get; set; }
        //    public entCliente Cliente { get; set; }
        public String Cliente { get; set; }
        public double Asi_F1 { get; set; }
        public double Asi_F2 { get; set; }
        public double Asi_F3 { get; set; }
        public String Asi_SVA { get; set; }
        public String Asi_FechInicioCliente { get; set; }
        public Boolean Asi_Estado { get; set; }
        public DateTime Asi_FechaRegistro { get; set; }
        public String Asi_UsuarioRegistro { get; set; }
        public DateTime Asi_FechaModificacion { get; set; }
        public String Asi_UsuarioModificacion { get; set; }
    }
}
