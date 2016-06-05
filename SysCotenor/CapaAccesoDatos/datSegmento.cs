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
    public class datSegmento
    {
        #region Singleton

        private static readonly datSegmento _Instancia = new datSegmento();
        public static datSegmento Instancia
        {
            get
            {
                return datSegmento._Instancia;
            }
        }

        #endregion Singleton

        #region metodos

        public List<entSegmento> ListaSegmento() {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entSegmento> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListaSegmento", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entSegmento>();
                while (dr.Read())
                {
                    entSegmento s = new entSegmento();
                    s.Seg_Id = Convert.ToInt32(dr["Seg_Id"]);
                    s.Seg_Codigo = dr["Seg_Codigo"].ToString();
                    s.Seg_Nombre = dr["Seg_Nombre"].ToString();
                    Lista.Add(s);
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
