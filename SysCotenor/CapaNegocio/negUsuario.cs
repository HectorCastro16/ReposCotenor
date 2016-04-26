using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaAccesoDatos;

namespace CapaNegocio
{
    public class negUsuario
    {
        #region Singleton
        private static readonly negUsuario _Instancia = new negUsuario();
        public static negUsuario Instancia
        {
            get
            {
                return negUsuario._Instancia;
            }
        }
        #endregion Singleton

        #region metodos
      
        public entUsuario VerificarAccesoIntranet(String prmstrLogin, String prmstrPassw)
        {
            try
            {
                entUsuario u = null;
                u = datUsuario.Instancia.VerificarAccesoIntranet(prmstrLogin, prmstrPassw);

                if (u == null)
                {
                    throw new ApplicationException("Usuario y/o Contaseña No Validos");
                }
                if (u.Usu_Estado == "Bloqueado")
                {
                    throw new ApplicationException("Usuario Bloqueado - Comunicarse con el Departamento de Sistemas");
                }
                if (u.Usu_Estado == "Eliminado")
                {
                    throw new ApplicationException("Usuario Eliminado del Sistema");
                }
                DateTime fechaHoy = DateTime.Now;
                if (DateTime.Compare(fechaHoy, u.Usu_FechaHasta) > 0)
                {
                    throw new ApplicationException("Ha Expirado su Tiempo de Actividad en el Sistema");
                }

                return u;
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

        public List<entUsuario> ListaUsuarios(Int32 UsuarioId,  Int32 SucursalId)
        {
            try
            {
                return datUsuario.Instancia.ListaUsuarios(UsuarioId, SucursalId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public entUsuario DetalleUsuario(Int32 UsuarioId, Int32 UsuarioIdSuper)
        {

            try
            {
                return datUsuario.Instancia.DetalleUsuario(UsuarioId, UsuarioIdSuper);
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }


        public int  RegUsuarioSecurity(entUsuario u, Int16 TipoEdicion,String IpAcceso) {
            try
            {
                String cadXml = "";
                cadXml += "<UsuarioSecurity ";
                cadXml += "UsuarioID='" + u.Usu_Id.ToString() + "' ";
                cadXml += "Username='" + u.Usu_Login + "' ";
                cadXml += "IPAcceso='" + IpAcceso + "' ";
                cadXml += "TipoEdicion='" + TipoEdicion + "'/>";

                cadXml = "<root>" + cadXml + "</root>";

                int i = datUsuario.Instancia.RegUsuarioSecurity(cadXml);
                if (i == -2) { throw new ApplicationException("Problema 1"); }
                if (i == -3) { throw new ApplicationException("Problema 2"); }
                return i;
            }
            catch (Exception e)
            {
                
                throw e;
            }
        
        }






        //public int InsUpdUsuario(entPersona p, Int16 TipoEdicion) {

        //    try
        //    {
        //        String cadXml = "";
        //        cadXml += "<Pedido ";
        //        cadXml += "idCliente='" + p.Cliente.idCliente + "' ";
        //        cadXml += "idTarjetaCredito='" + p.idTarjetaCredito + "' ";
        //        cadXml += "UsuarioTarjeta='" + p.UsuarioTarjeta + "' ";
        //        cadXml += "NumeroTarjeta='" + p.NumeroTarjeta + "' ";
        //        cadXml += "Total='" + p.Total + "'>";
        //        foreach (entUsuario d in p.DPedido)
        //        {
        //            cadXml += "<DPedido ";
        //            cadXml += "idProducto='" + d.idProducto + "' ";
        //            cadXml += "Cantidad='" + d.Cantidad + "' ";
        //            cadXml += "Precio='" + d.Precio + "'/>";
        //        }
        //        cadXml += "</Pedido>";
        //        cadXml = "<root>" + cadXml + "</root>";
        //        // variable i llega el resultado
        //        int i = datUsuario.Instancia.InsUpdDelBloAct(cadXml);
        //        if (i <= 0)
        //        {
        //            throw new ApplicationException("No se Pudo insertar a el trabajador");
        //        }
        //        return i;

        //    }
        //    catch (Exception e)
        //    {

        //        throw e;
        //    }

        //}

        #endregion metodos
    }
}
