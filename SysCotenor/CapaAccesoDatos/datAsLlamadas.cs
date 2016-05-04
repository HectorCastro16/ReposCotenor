using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CapaAccesoDatos
{
  public  class datAsLlamadas{
        #region singleton
        private static readonly datAsLlamadas _instancia = new datAsLlamadas();
        public static datAsLlamadas Instancia{
            get{
                return datAsLlamadas._instancia;
            }
        }
        #endregion singleton

        #region metodos

        public int GuardarAsLlamadas(String xml){
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarAsLlamadas", cn);
                cmd.Parameters.AddWithValue("@xml", xml);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pr = new SqlParameter("@retorno", DbType.Int32);
                pr.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(pr);
                cn.Open();
                cmd.ExecuteNonQuery();

                Int32 i = Convert.ToInt32(cmd.Parameters["@retorno"].Value);
                return i;
            }
            catch (Exception) {
                throw;
            }
            finally { cmd.Connection.Close();}
        }





        #endregion metodos


    }
}
