using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaAccesoDatos;

namespace CapaNegocio
{
    public class negTipoUsuario
    {

        #region Singleton
        private static readonly negTipoUsuario _Instancia = new negTipoUsuario();
        public static negTipoUsuario Instancia
        {
            get
            {
                return negTipoUsuario._Instancia;
            }
        }
        #endregion Singleton

        #region Metodos

        public List<entTipoUsuario> ListaTipoUsuarioxId(Int32 TipUsuId)
        {
            try
            {
                return datTipoUsuario.Instancia.ListaTipoUsuarioxId(TipUsuId);
            }
            catch (Exception e) { throw e; }
        }

        #endregion Metodos
    }
}
