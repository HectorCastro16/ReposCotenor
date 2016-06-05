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
   public class datArbolUbigeo
    {
        #region singleton
        private static readonly datArbolUbigeo _intancia = new datArbolUbigeo();
        public static datArbolUbigeo Instancia{
            get { return datArbolUbigeo._intancia; }
        }
        #endregion singleton

        #region metodos

        public List<entDistrito> ListaDist(Int32 idprov)
        {

            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entDistrito> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListDistXprov", cn);
                cmd.Parameters.AddWithValue("@idProv", idprov);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entDistrito>();
                while (dr.Read())
                {
                    entDistrito d = new entDistrito();
                    d.idDist = Convert.ToInt32(dr["idDist"]);
                    d.distrito = dr["distrito"].ToString();
                    entProvincia p = new entProvincia();
                    p.idProv = Convert.ToInt32(dr["idProv"]);
                    Lista.Add(d);

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally { cmd.Connection.Close(); }
            return Lista;
        }



        public List<entProvincia> ListaProv(Int32 iddepa){

            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entProvincia> Lista= null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListProvXdept", cn);
                cmd.Parameters.AddWithValue("@idDepa", iddepa);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entProvincia>();
                while (dr.Read())
                {
                    entProvincia p = new entProvincia();
                    p.idProv = Convert.ToInt32(dr["idProv"]);
                    p.provincia = dr["provincia"].ToString();
                    entDepartamento d = new entDepartamento();
                    d.idDepa = Convert.ToInt32(dr["idDepa"]);
                    p.departamento = d;
                    Lista.Add(p);
                }
            }
            catch (Exception){
                throw;
            }
            finally { cmd.Connection.Close(); }
            return Lista;
        }
        
        public List<entDepartamento> ListaDep(){
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entDepartamento> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListdepartamentos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entDepartamento>();
                while (dr.Read())
                {
                    entDepartamento d = new entDepartamento();
                    d.idDepa = Convert.ToInt32(dr["idDepa"]);
                    d.departamento = dr["departamento"].ToString();
                    Lista.Add(d);
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
