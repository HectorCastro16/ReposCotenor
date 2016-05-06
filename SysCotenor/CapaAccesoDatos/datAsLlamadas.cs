using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using CapaEntidades;

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


        public List<entAsigncionLlamadas> ListaAsignacionesHoy(Int32 iduser){

            SqlCommand cmd = null;
            List<entAsigncionLlamadas> Lista = null;
            SqlDataReader dr = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarAsLlamdas", cn);
                cmd.Parameters.AddWithValue("@idUser", iduser);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entAsigncionLlamadas>();
                while (dr.Read())
                {
                    entAsigncionLlamadas call = new entAsigncionLlamadas();
                    call.Asi_Id = Convert.ToInt32(dr["Asi_Id"]);
                    call.Asi_NumTelf = dr["Asi_NumTelf"].ToString();
                    call.Cliente = dr["Asi_Cliente"].ToString();
                    call.Asi_F1 = Convert.ToDouble(dr["Asi_F1"]);
                    call.Asi_F2 = Convert.ToDouble(dr["Asi_F2"]);
                    call.Asi_F3 = Convert.ToDouble(dr["Asi_F3"]);
                    call.Asi_SVA = dr["Asi_SVA"].ToString();
                    call.Asi_FechInicioCliente = dr["Asi_FechInicioCliente"].ToString();
                    call.Asi_FechaRegistro = Convert.ToDateTime(dr["Asi_FechaRegistro"]);
                    entUsuario u = new entUsuario();
                    u.Usu_Id = Convert.ToInt32(dr["Asi_Usu_Id"]);
                    call.Usuario = u;
                    Lista.Add(call);
                }
            }
            catch (Exception){
                throw;
            }
            finally { cmd.Connection.Close(); }
            return Lista;
        }


        #endregion metodos


    }
}
