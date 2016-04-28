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
        public static negSucursal Instancia {
            get { return  negSucursal._instancia; }
        }
        #endregion singleton

        #region metodos

        public List<entSucursal> ListarSucursales(Int32 idsucursal) {
            try {
                List<entSucursal> lista = datSucursal.Instancia.ListarSucursales();
                List<entSucursal> filtro = new List<entSucursal>();
                for (int i = 0; i < lista.Count; i++) {
                    if (lista[i].Suc_Id == idsucursal) {
                        filtro.Add(lista[i]);
                    }
                }return filtro;
            }catch (Exception){
                throw;
            }
           
        }


          # endregion metodos




    }
}
