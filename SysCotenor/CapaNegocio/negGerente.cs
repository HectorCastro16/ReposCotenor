using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaAccesoDatos;

namespace CapaNegocio
{
    public class negGerente
    {
        #region Singleton
        private static readonly negGerente _Instancia = new negGerente();
        public static negGerente Instancia
        {
            get
            {
                return negGerente._Instancia;
            }
        }
        #endregion Singleton

        #region metodos

        public List<entUsuario> ListaSupervisores()
        {
            try
            {
                return datGerente.Instancia.ListaSupervisores();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public entUsuario DetalleSupervisor(Int32 UsuarioIdSuper)
        {

            try
            {
                entUsuario u = datGerente.Instancia.DetalleSupervisor(UsuarioIdSuper);
                if (u == null)
                {
                    throw new ApplicationException("Supervisor no Encontrado");
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
