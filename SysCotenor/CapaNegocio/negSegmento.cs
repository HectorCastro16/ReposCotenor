using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaAccesoDatos;

namespace CapaNegocio
{
    public class negSegmento
    {
        #region Singleton
        private static readonly negSegmento _Instancia = new negSegmento();
        public static negSegmento Instancia
        {
            get
            {
                return negSegmento._Instancia;
            }
        }
        #endregion Singleton

        #region metodos

        public List<entSegmento> ListaSegmento()
        {
            try
            {
                return datSegmento.Instancia.ListaSegmento();
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        #endregion metodos
    }
}
