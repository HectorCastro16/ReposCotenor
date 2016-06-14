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
    public class datTipDoc
    {
        #region Singleton

        private static readonly datTipDoc _Instancia = new datTipDoc();
        public static datTipDoc Instancia
        {
            get
            {
                return datTipDoc._Instancia;
            }
        }

        #endregion Singleton

        #region metodos

        public List<entTipDoc> ListaTipDoc()
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entTipDoc> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListaTipDoc", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entTipDoc>();

                while (dr.Read())
                {
                    entTipDoc td = new entTipDoc();
                    td.td_id = Convert.ToInt32(dr["td_id"]);
                    td.td_nombre = dr["td_nombre"].ToString();
                    td.td_Descripcion = dr["td_Descripcion"].ToString();
                    Lista.Add(td);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Lista;
        }

        #endregion metodos
    }
}
