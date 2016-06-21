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
        public List<entEstado> ListEstados(){
            try {
                return datPedido.Instancia.ListaEstados();
            }catch (Exception)
            {

                throw;
            }
        }


        public List<entPedido> ListaComisiones(int idasesor,String desde,String hasta,int idestado){
            try
            {
                return datPedido.Instancia.ListpedidoComision(idasesor, desde, hasta,idestado);
            }
            catch (Exception)
            {

                throw;
            }
        }



        public int InsUpdPedido(entPedido p, Int32 TipoEdicion) {
            try
            {
                String cadXml = "";
                cadXml += "<Ped ";
                cadXml += "Ped_Usu_Id='"+p.Usuario.Usu_Id + "' ";
                cadXml += "Ped_Acc_Id='" + p.AccionComercial.Acc_Id + "' ";
                cadXml += "Ped_Pro_Id='" + p.Producto.Pro_ID + "' ";
                cadXml += "Ped_Distrito_Id='" + p.Distrito.idDist + "' ";
                cadXml += "Ped_Provincia_Id='" + p.Provincia.idProv + "' ";
                cadXml += "Ped_Depardamento_Id='" + p.Departamento.idDepa + "' ";
                cadXml += "Ped_Dir_Inst='" + p.Ped_Dir_Inst + "' ";
                cadXml += "Ped_Cod_Experto='" + p.Ped_Cod_Experto + "' ";
                cadXml += "Ped_Observaciones='" + p.Ped_Observaciones + "' ";
                cadXml += "Ped_UsuarioRegistro='" + p.Ped_UsuarioRegistro + "' ";
                cadXml += "TipoEdicion='" + TipoEdicion + "'>";
                
                    cadXml += "<CliTel ";
                    cadXml += "CliTel_UsuarioRegistro='" + p.Ped_UsuarioRegistro + "' ";
                    cadXml += "AsiUsu='" + p.ClienteTelefono.AsiUsu + "' ";
                    cadXml += "TipoEdicion='" + TipoEdicion + "'>";

                        cadXml += "<Cli ";
                        cadXml += "Cli_Id='" + p.ClienteTelefono.Cliente.Cli_Id+ "' ";
                        cadXml += "Cli_TipDoc_Id='" + p.ClienteTelefono.Cliente.TipDoc.td_id + "' ";
                        cadXml += "Cli_Nombre='" + p.ClienteTelefono.Cliente.Cli_Nombre + "' ";
                        cadXml += "Cli_RazonSocial='" + p.ClienteTelefono.Cliente.Cli_RazonSocial + "' ";
                        cadXml += "Cli_Seg_Id='" + p.ClienteTelefono.Cliente.Segmento.Seg_Id + "' ";
                        cadXml += "Cli_Numero_Documento='" + p.ClienteTelefono.Cliente.Cli_Numero_Documento + "' ";
                        cadXml += "Cli_FechaNacimiento='" + p.ClienteTelefono.Cliente.Cli_FechaNacimiento.ToString("yyyy/MM/dd") + "' ";
                        cadXml += "Cli_LugarNacimiento='" + p.ClienteTelefono.Cliente.Cli_LugarNacimiento + "' ";
                        cadXml += "Cli_Correo='" + p.ClienteTelefono.Cliente.Cli_Correo + "' ";
                        cadXml += "Cli_Telefono_Referencia='" + p.ClienteTelefono.Cliente.Cli_Telefono_Referencia + "' ";                
                        cadXml += "Cli_UsuarioRegistro='" + p.ClienteTelefono.Cliente.Cli_UsuarioRegistro + "' ";
                        cadXml += "TipoEdicion='" + TipoEdicion + "'/>";

                        cadXml += "<Tel ";
                        cadXml += "Tel_Numero='" + p.ClienteTelefono.Telefono.Tel_Numero + "' ";
                        //cadXml += "Tel_Producto='" + p.ClienteTelefono.Telefono.Tel_Producto + "' ";
                        cadXml += "Tel_Direccion='" + p.ClienteTelefono.Telefono.Tel_Direccion + "' ";
                        cadXml += "TipoEdicion='" + TipoEdicion + "'/>";

                    cadXml += "</CliTel>";

                cadXml += "</Ped>";
                cadXml = "<root>" + cadXml + "</root>";

                int i = datPedido.Instancia.InsUpdDelBloActPedido(cadXml);

                if (TipoEdicion == 1)
                {
                    if (i <= 0)
                    {
                        throw new ApplicationException("No se Pudo Insertar el Pedido");
                    }
                }
                if (TipoEdicion == 2)
                {
                    if (i <= 0)
                    {
                        throw new ApplicationException("No se Pudo Editar el Pedido");
                    }
                }
                return i;
            }
            catch (ApplicationException ae)
            {
                throw ae;
            }
            catch (Exception e)
            {

                throw e;
            }
        }




        //public int RegistroPedido(entPedido p,int idprod,Int32 tipedicion,Int32 tipedper){

        //    try {
        //        String cadxml = "";
        //        cadxml += "<pedido ";
        //        cadxml+= "usuario='"+p.Usuario.Usu_Id+"' ";
        //        cadxml+= "accioncomercial='"+p.AccionComercial.Acc_Id+"' ";
        //        cadxml += "direccioninstalacion='"+p.Ped_Dir_Inst+"' ";
        //        cadxml += "total='" + p.Ped_Total + "' ";
        //        cadxml += "usuarioR='"+p.Usuario.Usu_Login+ "' ";
        //        cadxml += "producto='" + idprod + "'>";

        //        cadxml += "<cliente ";
        //        cadxml+= "idcliente='"+p.Cliente.Cli_Id+"' ";
        //        cadxml +="segmento='"+p.Cliente.Segmento.Seg_Id+"' ";
        //        cadxml += "ruc='" + p.Cliente.Cli_Ruc + "' ";
        //        cadxml+="distrito='"+p.Cliente.Cli_Distrito.idDist+"' ";
        //        cadxml += "provincia='" + p.Cliente.Cli_Provincia.idProv + "' ";
        //        cadxml += "departamento='" + p.Cliente.Cli_Depardamento.idDepa + "' ";
        //        cadxml += "usuarioR='" + p.Usuario.Usu_Login + "' ";
        //        cadxml += "empresa='" + p.Cliente.Cli_Empresa + "' ";
        //        cadxml += "tipoedicion='" + tipedicion + "'>";

        //        cadxml += "<pertelef ";
        //        cadxml += "usuarioR='" + p.Usuario.Usu_Login + "' ";
        //        cadxml += "tipoedicion='" + tipedicion + "'>";

        //        cadxml += "<persona "; 
        //        cadxml+= "perid='"+p.Cliente.Persona_telef.persona.Per_Id+"' ";
        //        cadxml += "nombres='" + p.Cliente.Persona_telef.persona.Per_Nombres + "' ";
        //        cadxml += "apellidos='" + p.Cliente.Persona_telef.persona.Per_Apellidos + "' ";
        //        cadxml += "dni='" + p.Cliente.Persona_telef.persona.Per_DNI + "' ";
        //        cadxml += "celular='" + p.Cliente.Persona_telef.persona.Per_Celular + "' ";
        //        cadxml += "correo='" + p.Cliente.Persona_telef.persona.Per_Correo + "' ";
        //        cadxml += "direccion='" + p.Cliente.Persona_telef.persona.Per_Direccion + "' ";
        //        cadxml += "fechanacimiento='" + p.Cliente.Persona_telef.persona.Per_FechaNacimiento + "' ";
        //        cadxml += "lugarnacimiento='" + p.Cliente.Persona_telef.persona.Per_LugarNacimiento + "' ";
        //        cadxml += "tipoedicion='" + tipedper+ "'/>";

        //        cadxml += "<telefono ";
        //        cadxml += "telefono='" + p.Cliente.Persona_telef.telefono.Tel_Numero + "' ";
        //        cadxml += "tipoedicion='" + tipedicion + "'/>";

        //        cadxml += "</pertelef>";
        //        cadxml += "</cliente>";             
        //        cadxml += "</pedido>";

        //        cadxml = "<root>" + cadxml + "</root>";
        //        int i =0;//datPedido.Instancia.RegistroPedido(cadxml);
        //    }
        //    catch (Exception){

        //        throw;
        //    }return 0;

        //}




        #endregion metodos



    }
}
