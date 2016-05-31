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
        #region sinlgeton
        private static readonly negProducto _intancia = new negProducto();
        public static negProducto Instancia{
            get { return negProducto._intancia; }
        }
        #endregion singleton

        #region metodos


        public List<entPrecio> ListPrec()
        {
            try
            {
                return datProducto.Instancia.Listprec();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<entCategoria> ListCatego()
        {
            try
            {
                return datProducto.Instancia.Listacat();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<entProducto> ListProd(){
            try {
                return datProducto.Instancia.ListaProd();
            }
            catch (Exception){
                throw;
            }
        }


        #endregion metodos
        

    }
}
