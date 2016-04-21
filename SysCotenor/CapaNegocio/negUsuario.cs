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

        public entUsuario VerificarUsuarioExiste(String prmstrLogin)
        {
            try
            {
                entUsuario u = null;
                u = datUsuario.Instancia.VerificarUsuarioExiste(prmstrLogin);
                return u;
            }
            catch (ApplicationException ae)
            {
                throw ae;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<entUsuario> ListaUsuarios(String UsuarioId,String TipoUsuario, String Sucursal)
        {
            try
            {
                return datUsuario.Instancia.ListaUsuarios(UsuarioId,TipoUsuario, Sucursal);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public entUsuario DetalleUsuario(String UsuarioId,String TipUsuId) {

            try
            {
                return datUsuario.Instancia.DetalleUsuario(UsuarioId, TipUsuId);
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }


        #endregion metodos
    }
}
