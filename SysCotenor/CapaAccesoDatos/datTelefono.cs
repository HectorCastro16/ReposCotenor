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
    public class datTelefono
    {
        #region Singleton

        private static readonly datTelefono _Instancia = new datTelefono();
        public static datTelefono Instancia
        {
            get
            {
                return datTelefono._Instancia;
            }
        }

        #endregion Singleton
        #region metodos

        public int ValidaTelefono(String telefono)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            int i = 0;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spValidaTelefono", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmtIntTelefono", telefono);
                cn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Int32 val = Convert.ToInt32(dr["Resultado"]);
                    if (val > 0)
                    {
                        i = val;
                    }
                }
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
