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

        public int GuardarAsLlamadas(DataTable dt,Int32 idasesor, Int32 iduser){
            try{
                String cadxml = "";
                String f1, f2, f3;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Estadooedicion"].ToString() != "0")
                    {
                        f1 = dr["f1"].ToString(); f2 = dr["f2"].ToString(); f3 = dr["f3"].ToString();

                        Int32  tipoedicion =Convert.ToInt32(dr["Estadooedicion"]);
                        cadxml += "<asignacionllamads ";
                        cadxml += "idasLlamadas='" + dr["Asi_id"] + "' ";
                        cadxml += "idasesor='" + idasesor + "' ";
                        cadxml += "telefono='" + dr["telefono"] + "' ";
                        cadxml += "cliente='" + dr["cliente"] + "' ";
                        cadxml += "f1='" + f1.Replace(",",".") + "' ";
                        cadxml += "f2='" + f1.Replace(",", ".") + "' ";
                        cadxml += "f3='" + f1.Replace(",", ".") + "' ";
                        cadxml += "sva='" + dr["sva"] + "' ";
                        cadxml += "fechainicio='" + dr["iniciovigencia"] + "' ";
                        cadxml += "tipoedicion='" + tipoedicion + "' ";
                        cadxml += "usuarioregistro='" + iduser + "'/>";
                    }
                }
                cadxml = "<root>" + cadxml + "</root>";
                int i = datAsLlamadas.Instancia.GuardarAsLlamadas(cadxml);
                return i;
            }
            catch (Exception e) {
                throw e;
            }
        }

        public List<entAsigncionLlamadas> ListaLamadasAsig(Int32 idusuario){
            try {
                return datAsLlamadas.Instancia.ListaAsignacionesHoy(idusuario);
            }
            catch (Exception e){
                throw e;
            }

        }






        #endregion metodos


    }
}
