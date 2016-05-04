using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaAccesoDatos;
using System.Data;
namespace CapaNegocio
{
  public  class negAsLlamadas
    {
        #region singleton
        private static readonly negAsLlamadas _intancia = new negAsLlamadas();
        public static negAsLlamadas Instancia{
            get { return negAsLlamadas._intancia; }
        }
        #endregion singleton

        #region metodos

        public int GuardarAsLlamadas(DataTable dt,int tipoedicion,Int32 idasesor, Int32 iduser){
            try{
                String cadxml = "";
                foreach (DataRow dr in dt.Rows){
                    cadxml += "<asignacionllamads ";
                    cadxml += "idasesor='"+idasesor+"' ";         
                    cadxml += "telefono='" + dr["telefono"] +"' ";
                    cadxml += "cliente='" + dr["cliente"] + "' ";
                    cadxml += "f1='"+dr["f1"] +"' "; 
                    cadxml+= "f2='"+dr["f2"] +"' "; 
                    cadxml+= "f3='"+dr["f3"] +"' "; 
                    cadxml+= "sva='"+dr["sva"] +"' "; 
                    cadxml+= "fechainicio='" + dr["iniciovigencia"] +"' ";
                    cadxml += "tipoedicion='" + tipoedicion+ "' ";
                    cadxml += "usuarioregistro='" + iduser + "'/>"; 
                }
                cadxml = "<root>" + cadxml + "</root>";
                int i = datAsLlamadas.Instancia.GuardarAsLlamadas(cadxml);
                return i;
            }
            catch (Exception e) {
                throw e;
            }
        }








        #endregion metodos


    }
}
