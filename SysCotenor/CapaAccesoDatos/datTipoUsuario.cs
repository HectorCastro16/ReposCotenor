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
    public class datTipoUsuario
    {
        #region Singleton

        private static readonly datTipoUsuario _Instancia = new datTipoUsuario();
        public static datTipoUsuario Instancia
        {
            get
            {
                return datTipoUsuario._Instancia;
            }
        }

        #endregion Singleton

        #region Metodos

        public List<entTipoUsuario> ListaTipoUsuarioxId(Int32 TipUsuId)
        {

            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entTipoUsuario> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListaTipoUsuarioxId", cn);
                cmd.Parameters.AddWithValue("@prmintIdTipUsu", TipUsuId);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entTipoUsuario>();

                if (dr.Read())
                {
                    entTipoUsuario t = new entTipoUsuario();
                    t.TipUsu_Id = Convert.ToInt32(dr["TipUsu_Id"]);
                    t.TipUsu_Codigo = dr["TipUsu_Codigo"].ToString();
                    t.TipUsu_Nombre = dr["TipUsu_Nombre"].ToString();
                    Lista.Add(t);
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

        public List<entTipoUsuario> ListaTipoUsuarioSupervisores()
        {

            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entTipoUsuario> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListaTipoUsuarioSupervisores", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entTipoUsuario>();

                while (dr.Read())
                {
                    entTipoUsuario t = new entTipoUsuario();
                    t.TipUsu_Id = Convert.ToInt32(dr["TipUsu_Id"]);
                    t.TipUsu_Codigo = dr["TipUsu_Codigo"].ToString();
                    t.TipUsu_Nombre = dr["TipUsu_Nombre"].ToString();
                    Lista.Add(t);
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


        #endregion Metodos
    }
}
