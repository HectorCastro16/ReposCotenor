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


        public int updateEstadoPedido(int idpedido,int idestado,String desc){
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            int result = 0;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spUpdateEstadoPed", cn);
                cmd.Parameters.AddWithValue("@ID_PEDIDO", idpedido);
                cmd.Parameters.AddWithValue("@ID_ESTADO", idestado);
                cmd.Parameters.AddWithValue("@DESCRIPCION", desc);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlParameter p = new SqlParameter("@retorno", DbType.Int32);
                p.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(p);

                cmd.ExecuteNonQuery();
               return result = Convert.ToInt32(cmd.Parameters["@retorno"].Value);
            }
            catch (Exception)
            {

                throw;
            }
        }



        public List<entEstado> ListaEstados(){
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List <entEstado> Lista= null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListaEstados", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entEstado>();
                while (dr.Read()){
                    entEstado e = new entEstado();
                    e.Est_Id = Convert.ToInt32(dr["Est_Id"]);
                    e.Est_Nombre = dr["Est_Nombre"].ToString();
                    Lista.Add(e);
                }
            }
            catch (Exception)
            {
                throw;
            }finally { cmd.Connection.Close(); }
            return Lista;
        }

        public List<entPedido> ListpedidoComision(int idasesor, String desde, String hasta,int idestado){
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entPedido> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spVistaComisiones", cn);
                cmd.Parameters.AddWithValue("@ID_USUARIO", idasesor);
                cmd.Parameters.AddWithValue("@DESDE", desde);
                cmd.Parameters.AddWithValue("@HASTA", hasta);
                cmd.Parameters.AddWithValue("@ID_ESTADO", idestado);

                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entPedido>();
                while (dr.Read())
                {
                    entPedido p = new entPedido();
                    p.Ped_Id = Convert.ToInt32(dr["Ped_Id"]);
                    p.Ped_Codigo = dr["Ped_Codigo"].ToString();
                    p.Ped_FechaRegistro = Convert.ToDateTime(dr["Ped_FechaRegistro"]);
                    entAccionComercial ac = new entAccionComercial();
                    ac.Acc_Nombre = dr["Acc_Nombre"].ToString();
                    entProducto pr = new entProducto();
                    pr.Pro_Nombre = dr["Pro_Nombre"].ToString();
                    entComision c = new entComision();
                    c.detC_ValorUnidades = Convert.ToInt32(dr["detC_ValorUnidades"]);
                    c.detC_ValorSoles = Convert.ToDouble(dr["detC_ValorSoles"]);
                    entEstado e = new entEstado();
                    e.Est_Nombre = dr["Est_Nombre"].ToString();
                    p.Estado = e;
                    pr.comision = c;
                    p.Producto = pr;
                    p.AccionComercial = ac;
                    Lista.Add(p);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally { cmd.Connection.Close(); }
            return Lista;
        }
        
        #region metodos
        public int InsUpdDelBloActPedido(String cadXML)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsUpdDelBloActPedido", cn);
                cmd.Parameters.AddWithValue("@prmstrCadXML", cadXML);
                cmd.CommandType = CommandType.StoredProcedure;

                //creamos el parametro de retorno
                SqlParameter m = new SqlParameter("@retorno", DbType.Int32);
                m.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(m);
                //fin parametro
                cn.Open();
                cmd.ExecuteNonQuery();

                int i = Convert.ToInt32(cmd.Parameters["@retorno"].Value);

                return i;

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cmd.Connection.Close();
            }

        }

        public int InsUpdDelBloActPedidoCampo(String cadXML)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsUpdDelBloActPedidoCampo", cn);
                cmd.Parameters.AddWithValue("@prmstrCadXML", cadXML);
                cmd.CommandType = CommandType.StoredProcedure;

                //creamos el parametro de retorno
                SqlParameter m = new SqlParameter("@retorno", DbType.Int32);
                m.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(m);
                //fin parametro
                cn.Open();
                cmd.ExecuteNonQuery();

                int i = Convert.ToInt32(cmd.Parameters["@retorno"].Value);

                return i;

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cmd.Connection.Close();
            }

        }


        #endregion metodos


    }
}
