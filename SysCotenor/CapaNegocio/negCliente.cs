using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaAccesoDatos;

namespace CapaNegocio{
    public class negCliente {
        #region singleton

        private static readonly negCliente _intancia = new negCliente();
        public static negCliente Instancia {
            get { return negCliente._intancia; }
        }
        #endregion singleton

        #region metodos 

        public entCliente BuscaCliente(String telefono){
            try
            {
                return datCliente.Instancia.BuscaClienteVenta(telefono);
            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion metodos

    }
}
