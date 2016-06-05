using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaAccesoDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class negArbolUbigeo
    {

        #region singleton

        private static readonly negArbolUbigeo _instancia = new negArbolUbigeo();
        public static negArbolUbigeo Instancia{
            get { return negArbolUbigeo._instancia; }
        }
        #endregion singleton

        #region metodos
        public List<entDistrito> ListarDist(Int32 idprov)
        {
            try
            {
                return datArbolUbigeo.Instancia.ListaDist(idprov);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<entProvincia> ListarProv(Int32 iddept)
        {
            try{
                return datArbolUbigeo.Instancia.ListaProv(iddept);
            }
            catch (Exception){
                throw;
            }
        }


        public List<entDepartamento> ListarDept(){

            try {
                return datArbolUbigeo.Instancia.ListaDep();
            }catch (Exception){
                throw;
            }
        }
        
        #endregion metodos

        
    }
}
