using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaAccesoDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class negArbolVenta{
        #region singleton
        private static readonly negArbolVenta _instancia = new negArbolVenta();
        public static negArbolVenta Instancia{
            get { return negArbolVenta._instancia;}
        }
        #endregion singleton

        #region metodos 

        public List<entProducto> ListaprodCombo(Int32 id_Cat)
        {
            try
            {
                return datArbolVenta.Instancia.ListarProdcts(id_Cat);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<entCategoria> ListaCatComCbo(Int32 id_Grupo)
        {
            try
            {
                return datArbolVenta.Instancia.ListarCatGorias(id_Grupo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<entGrupoComercial> ListaGrupComCbo(Int32 id_accCom_Det){
            try{
                return datArbolVenta.Instancia.ListarGrup_Com(id_accCom_Det);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<entDetalleAccionComercial> ListaDetAccCbo(Int32 id_accCom){
            try{
                return datArbolVenta.Instancia.ListarDetAcc_Com(id_accCom);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<entAccionComercial> ListaAcCbo(){
            try{
                return datArbolVenta.Instancia.ListarCboAcc_Com();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion metodos
    }
}
