using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaAccesoDatos;
namespace CapaNegocio
{
    public class negSucursal
    {
        #region singleton
        private static readonly negSucursal _instancia = new negSucursal();
        public static negSucursal Instancia
        {
            get { return negSucursal._instancia; }
        }
        #endregion singleton

        #region metodos

        public List<entSucursal> ListaSucursalxId(Int32 idsucursal)
        {
            try
            {
                return datSucursal.Instancia.ListaSucursalxId(idsucursal);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public List<entSucursal> ListaSucursal()
        {
            try
            {
                return datSucursal.Instancia.ListaSucursal();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        # endregion metodos




    }
}
