
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaAccesoDatos;

namespace CapaNegocio
{
    public class negProducto
    {
        #region Singleton
        private static readonly negProducto _Instancia = new negProducto();
        public static negProducto Instancia
        {
            get
            {
                return negProducto._Instancia;
            }
        }
        #endregion Singleton

        #region metodos

        public List<entProducto> ListaProductoSVA() {
            try
            {
                return datProducto.Instancia.ListaProductoSVA();
            }
            catch (Exception e)
            {
                
                throw e;
            }
        
        }

        #endregion metodos
    }
}
