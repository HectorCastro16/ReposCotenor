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
    public class datSucursal
    {
        #region singleton
        public static readonly datSucursal _instancia = new datSucursal();
        public static datSucursal Instancia
        {
            get { return datSucursal._instancia; }
        }
        #endregion singleton

        #region metodos

        public List<entSucursal> ListaSucursalxId(Int32 SucursalId)
        {

            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entSucursal> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListaSucursalxId", cn);
                cmd.Parameters.AddWithValue("@prmIntIdSuc", SucursalId);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entSucursal>();

                while (dr.Read())
                {
                    entSucursal s = new entSucursal();
                    s.Suc_Id = Convert.ToInt32(dr["Suc_Id"]);
                    s.Suc_Codigo = dr["Suc_Codigo"].ToString();
                    s.Suc_Nombre = dr["Suc_Nombre"].ToString();
                    Lista.Add(s);
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
