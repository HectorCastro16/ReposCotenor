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

        ////public entCliente BuscaCliente(String telefono,String dni){
        ////    try
        ////    {
        ////        int i = 1;
        ////        entCliente c = new entCliente();
        ////        c = datCliente.Instancia.BuscaClienteVenta(telefono, dni,i);
        ////        if (c == null){
        ////            i = 2;
        ////            c = datCliente.Instancia.BuscaClienteVenta(telefono, dni, i);
        ////        }
        ////        return c;     
        ////    }
        ////    catch (Exception)
        ////    {
        ////        throw;
        ////    }

        ////}

        #endregion metodos

    }
}
