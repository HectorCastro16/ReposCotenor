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
    public class datProducto
    {
        #region Singleton

        private static readonly datProducto _Instancia = new datProducto();
        public static datProducto Instancia
        {
            get
            {
                return datProducto._Instancia;
            }
        }

        #endregion Singleton

        #region metodos

        public List<entProducto> ListaProductoSVA() {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entProducto> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListaProductoSVA", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entProducto>();
                while (dr.Read())
                {
                    entProducto p = new entProducto();
                    p.Pro_ID = Convert.ToInt32(dr["Pro_ID"]);
                    p.Pro_Codigo = dr["Pro_Codigo"].ToString();
                    p.Pro_Nombre = dr["Pro_Nombre"].ToString();
                    Lista.Add(p);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return Lista;

        }

        #endregion metodos
    }
}
