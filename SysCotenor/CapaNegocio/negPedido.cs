using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaAccesoDatos;

namespace CapaNegocio
{
   public class negPedido{
        #region singleton
        private static readonly negPedido _intancia = new negPedido();
        public static negPedido Instancia{
            get { return negPedido._intancia; }
        }
        #endregion singleton


        #region metodos

        public int RegistroPedido(entPedido p,int idprod,Int32 tipedicion,Int32 tipedper){

            try {
                String cadxml = "";
                cadxml += "<pedido ";
                cadxml+= "usuario='"+p.Usuario.Usu_Id+"' ";
                cadxml+= "accioncomercial='"+p.AccionComercial.Acc_Id+"' ";
                cadxml += "direccioninstalacion='"+p.Ped_Dir_Inst+"' ";
                cadxml += "total='" + p.Ped_Total + "' ";
                cadxml += "usuarioR='"+p.Usuario.Usu_Login+ "' ";
                cadxml += "producto='" + idprod + "'>";

                cadxml += "<cliente ";
                cadxml+= "idcliente='"+p.Cliente.Cli_Id+"' ";
                cadxml +="segmento='"+p.Cliente.Segmento.Seg_Id+"' ";
                cadxml += "ruc='" + p.Cliente.Cli_Ruc + "' ";
                cadxml+="distrito='"+p.Cliente.Cli_Distrito.idDist+"' ";
                cadxml += "provincia='" + p.Cliente.Cli_Provincia.idProv + "' ";
                cadxml += "departamento='" + p.Cliente.Cli_Depardamento.idDepa + "' ";
                cadxml += "usuarioR='" + p.Usuario.Usu_Login + "' ";
                cadxml += "empresa='" + p.Cliente.Cli_Empresa + "' ";
                cadxml += "tipoedicion='" + tipedicion + "'>";

                cadxml += "<pertelef ";
                cadxml += "usuarioR='" + p.Usuario.Usu_Login + "' ";
                cadxml += "tipoedicion='" + tipedicion + "'>";

                cadxml += "<persona "; 
                cadxml+= "perid='"+p.Cliente.Persona_telef.persona.Per_Id+"' ";
                cadxml += "nombres='" + p.Cliente.Persona_telef.persona.Per_Nombres + "' ";
                cadxml += "apellidos='" + p.Cliente.Persona_telef.persona.Per_Apellidos + "' ";
                cadxml += "dni='" + p.Cliente.Persona_telef.persona.Per_DNI + "' ";
                cadxml += "celular='" + p.Cliente.Persona_telef.persona.Per_Celular + "' ";
                cadxml += "correo='" + p.Cliente.Persona_telef.persona.Per_Correo + "' ";
                cadxml += "direccion='" + p.Cliente.Persona_telef.persona.Per_Direccion + "' ";
                cadxml += "fechanacimiento='" + p.Cliente.Persona_telef.persona.Per_FechaNacimiento + "' ";
                cadxml += "lugarnacimiento='" + p.Cliente.Persona_telef.persona.Per_LugarNacimiento + "' ";
                cadxml += "tipoedicion='" + tipedper+ "'/>";

                cadxml += "<telefono ";
                cadxml += "telefono='" + p.Cliente.Persona_telef.telefono.Tel_Numero + "' ";
                cadxml += "tipoedicion='" + tipedicion + "'/>";
                
                cadxml += "</pertelef>";
                cadxml += "</cliente>";             
                cadxml += "</pedido>";

                cadxml = "<root>" + cadxml + "</root>";
                int i = datPedido.Instancia.RegistroPedido(cadxml);
            }
            catch (Exception){

                throw;
            }return 0;

        }




        #endregion metodos



    }
}
