using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using System.Data.SqlClient;
using System.Data;

namespace CapaAccesoDatos
{
    public class datGerente
    {
        #region Singleton

        private static readonly datGerente _Instancia = new datGerente();
        public static datGerente Instancia
        {
            get
            {
                return datGerente._Instancia;
            }
        }

        #endregion Singleton

        #region metodos

        public List<entUsuario> ListaSupervisores()
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entUsuario> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListaSupervisores", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entUsuario>();

                while (dr.Read())
                {
                    entUsuario u = new entUsuario();
                    u.Usu_Id = Convert.ToInt32(dr["Usu_Id"]);

                    entPersona p = new entPersona();
                    p.Per_Nombres = dr["Per_Nombres"].ToString();
                    p.Per_Apellidos = dr["Per_Apellidos"].ToString();
                    p.Per_DNI = dr["Per_DNI"].ToString();
                    p.Per_Correo = dr["Per_Correo"].ToString();
                    p.Per_Celular = dr["Per_Celular"].ToString();
                    u.Persona = p;

                    entTipoUsuario t = new entTipoUsuario();
                    t.TipUsu_Nombre = dr["TipUsu_Nombre"].ToString();
                    u.TipoUsuario = t;

                    u.Usu_Telefono = dr["Usu_Telefono"].ToString();
                    u.Usu_Estado = dr["Usu_Estado"].ToString();

                    entSucursal s = new entSucursal();
                    s.Suc_Nombre = dr["Suc_Nombre"].ToString();
                    u.Sucursal = s;

                    u.Usu_FechaHasta = Convert.ToDateTime(dr["Usu_FechaHasta"]);
                    u.Contador = 0;
                    Lista.Add(u);
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
