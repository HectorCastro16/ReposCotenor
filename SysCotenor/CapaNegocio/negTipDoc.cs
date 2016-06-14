using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaAccesoDatos;

namespace CapaNegocio
{
    public class negTipDoc
    {
        #region Singleton
        private static readonly negTipDoc _Instancia = new negTipDoc();
        public static negTipDoc Instancia
        {
            get
            {
                return negTipDoc._Instancia;
            }
        }
        #endregion Singleton

        #region metodos

        public List<entTipDoc> ListaTipDoc()
        {
            try
            {
                return datTipDoc.Instancia.ListaTipDoc();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        #endregion metodos
    }
}
