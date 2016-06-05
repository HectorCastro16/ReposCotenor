using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using System.Data;

namespace CapaAccesoDatos
{
   public class datPedido{

        #region singleton
        private static readonly datPedido _instancia = new datPedido();
        public static datPedido Instancia{
            get { return datPedido._instancia; }
        }
        #endregion singleton

        #region metodos

        public int RegistroPedido(String cadxml){
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spRegistroPedido", cn);
                cmd.Parameters.AddWithValue("@cadXML", cadxml);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                SqlParameter pr = new SqlParameter("@retorno", DbType.Int32);
                pr.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(pr);
                
                cmd.ExecuteNonQuery();
                Int32 i = Convert.ToInt32(cmd.Parameters["@retorno"].Value);
                return i;
            }
            catch (Exception) {
                throw;
            } finally { cmd.Connection.Close(); }
           
        }



        #endregion metodos


    }
}
