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
                if (prmstrLogin == null)
                {
                    throw new ApplicationException("Debe Ingresar el Usuario");
                }
                if (prmstrPassw == null)
                {
                    throw new ApplicationException("Debe Ingresar el Password");
                }
                entUsuario u = datUsuario.Instancia.VerificarAccesoIntranet(prmstrLogin, prmstrPassw);
                if (u == null)
                {
                    throw new ApplicationException("Usuario o password no valido");
                }
                if (u.Usu_Estado=="Bloqueado")
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

        #endregion metodos
    }
}
