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
    public class datUsuario
    {
        #region Singleton

        private static readonly datUsuario _Instancia = new datUsuario();
        public static datUsuario Instancia
        {
            get
            {
                return datUsuario._Instancia;
            }
        }

        #endregion Singleton

        #region metodos


        public entUsuario BuscarUsuario(Int32 id) {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            entUsuario u = null;
            try{
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spBuscaUsuarioEditar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iduser", id);
                cn.Open();
                dr = cmd.ExecuteReader();
               if (dr.Read()) {
                    u = new entUsuario();
                    u.Usu_Id = Convert.ToInt32(dr["Usu_Id"]);
                    u.Usu_Codigo = dr["Usu_Codigo"].ToString();
                    u.Usu_Login = dr["Usu_Login"].ToString();
                    u.Usu_Password = dr["Usu_Password"].ToString();
                    u.Usu_FechaHasta =Convert.ToDateTime(dr["Usu_FechaHasta"]);
                    u.Usu_FechaRegistro = Convert.ToDateTime(dr["Usu_FechaRegistro"]);
                        entPersona p = new entPersona();
                        p.Per_Nombres = dr["Per_Nombres"].ToString();
                        p.Per_Apellidos = dr["Per_Apellidos"].ToString();
                        p.Per_DNI = dr["Per_DNI"].ToString();
                        p.Per_Celular = dr["Per_Celular"].ToString();
                        p.Per_Correo = dr["Per_Correo"].ToString();
                        p.Per_Telefono = dr["Per_Telefono"].ToString();
                        p.Per_Direccion = dr["Per_Direccion"].ToString();
                        p.Per_Foto = dr["Per_Foto"].ToString();
                        p.Per_FechaNacimiento = Convert.ToDateTime(dr["Per_FechaNacimiento"]);
                        p.Per_LugarNacimiento = dr["Per_LugarNacimiento"].ToString();

                        entTipoUsuario t = new entTipoUsuario();
                        t.TipUsu_Id = Convert.ToInt32(dr["TipUsu_Id"]);
                        t.TipUsu_Codigo = dr["TipUsu_Codigo"].ToString();
                        t.TipUsu_Nombre = dr["TipUsu_Nombre"].ToString();
                        u.TipoUsuario = t;

                        entSucursal s = new entSucursal();
                        s.Suc_Id = Convert.ToInt32(dr["Suc_Id"]);
                        s.Suc_Codigo = dr["Suc_Codigo"].ToString();
                        s.Suc_Nombre = dr["Suc_Nombre"].ToString();
                        u.Sucursal = s;
                    u.Persona = p;
                    u.TipoUsuario = t;
                    u.Sucursal = s;

                }
            }catch (Exception){
                throw;
            }
            return u;

        }


        public entUsuario VerificarAccesoIntranet(String prmstrLogin, String prmstrPassw)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            entUsuario u = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spVerificarAccesoIntranet", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmstrLogin", prmstrLogin);
                cmd.Parameters.AddWithValue("@prmstrPassw", prmstrPassw);
                cn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    u = new entUsuario();
                    u.Usu_Id = Convert.ToInt32(dr["Usu_Id"]);
                    u.Usu_Codigo = dr["Usu_Codigo"].ToString();

                    entPersona p = new entPersona();
                    p.Per_Nombres = dr["Per_Nombres"].ToString();
                    p.Per_Apellidos = dr["Per_Apellidos"].ToString();
                    p.Per_DNI = dr["Per_DNI"].ToString();
                    p.Per_Celular = dr["Per_Celular"].ToString();
                    p.Per_Correo = dr["Per_Correo"].ToString();
                    p.Per_Telefono = dr["Per_Telefono"].ToString();
                    p.Per_Direccion = dr["Per_Direccion"].ToString();
                    p.Per_Foto = dr["Per_Foto"].ToString();
                    p.Per_FechaNacimiento = Convert.ToDateTime(dr["Per_FechaNacimiento"]);
                    p.Per_LugarNacimiento = dr["Per_LugarNacimiento"].ToString();
                    u.Persona = p;

                    entTipoUsuario t = new entTipoUsuario();
                    t.TipUsu_Id = Convert.ToInt32(dr["TipUsu_Id"]);
                    t.TipUsu_Codigo = dr["TipUsu_Codigo"].ToString();
                    t.TipUsu_Nombre = dr["TipUsu_Nombre"].ToString();
                    u.TipoUsuario = t;

                    entSucursal s = new entSucursal();
                    s.Suc_Id = Convert.ToInt32(dr["Suc_Id"]);
                    s.Suc_Codigo = dr["Suc_Codigo"].ToString();
                    s.Suc_Nombre = dr["Suc_Nombre"].ToString();
                    u.Sucursal = s;

                    u.Usu_Login = dr["Usu_Login"].ToString();
                    u.Usu_Estado = dr["Usu_Estado"].ToString();
                    u.Usu_FechaHasta = Convert.ToDateTime(dr["Usu_FechaHasta"]);
                    u.Usu_FechaRegistro = Convert.ToDateTime(dr["Usu_FechaRegistro"]);
                    u.Usu_UsuarioRegistro = dr["Usu_UsuarioRegistro"].ToString();
                    u.Usu_FechaModificacion = Convert.ToDateTime(dr["Usu_FechaModificacion"]);
                    u.Usu_UsuarioModificacion = dr["Usu_UsuarioModificacion"].ToString();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            finally { cmd.Connection.Close(); }
            return u;
        }


        public List<entUsuario> ListaUsuarios(Int32 UsuarioId,  Int32 SucursalId)
        {

            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entUsuario> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListaUsuariosXTipo", cn);
                cmd.Parameters.AddWithValue("@prmtIntIdUsu", UsuarioId);
                cmd.Parameters.AddWithValue("@prmtIntSucId", SucursalId);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entUsuario>();

                while (dr.Read())
                {
                    entUsuario u = new entUsuario();
                    u.Usu_Id = Convert.ToInt32(dr["Usu_Id"]);
                    u.Usu_Codigo = dr["Usu_Codigo"].ToString();

                    entPersona p = new entPersona();
                    p.Per_Nombres = dr["Per_Nombres"].ToString();
                    p.Per_Apellidos = dr["Per_Apellidos"].ToString();
                    p.Per_DNI = dr["Per_DNI"].ToString();
                    p.Per_Celular = dr["Per_Celular"].ToString();
                    p.Per_Telefono = dr["Per_Telefono"].ToString();
                    u.Persona = p;

                    u.Usu_Estado = dr["Usu_Estado"].ToString();
                    u.Usu_FechaHasta = Convert.ToDateTime(dr["Usu_FechaHasta"]);
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

        public entUsuario DetalleUsuario(Int32 UsuarioId, Int32 UsuarioIdSuper)
        {

            SqlCommand cmd = null;
            SqlDataReader dr = null;
            entUsuario u = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spDetalleUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmtIntIdUsu", UsuarioId);
                cmd.Parameters.AddWithValue("@prmtIntIdUsuSuper", UsuarioIdSuper);
                cn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read()) {

                    u = new entUsuario();
                    u.Usu_Id = Convert.ToInt32(dr["Usu_Id"]);
                    u.Usu_Codigo = dr["Usu_Codigo"].ToString();

                    entPersona p = new entPersona();
                    p.Per_Id = Convert.ToInt32(dr["Per_Id"]);
                    p.Per_Codigo = dr["Per_Codigo"].ToString();
                    p.Per_Nombres = dr["Per_Nombres"].ToString();
                    p.Per_Apellidos = dr["Per_Apellidos"].ToString();
                    p.Per_DNI = dr["Per_DNI"].ToString();
                    p.Per_Foto = dr["Per_Foto"].ToString();
                    p.Per_Celular = dr["Per_Celular"].ToString();
                    p.Per_Correo = dr["Per_Correo"].ToString();
                    p.Per_Telefono = dr["Per_Telefono"].ToString();
                    p.Per_Direccion = dr["Per_Direccion"].ToString();
                    p.Per_FechaNacimiento = Convert.ToDateTime(dr["Per_FechaNacimiento"]);
                    p.Per_LugarNacimiento = dr["Per_LugarNacimiento"].ToString();
                    u.Persona = p;

                    u.Usu_Estado = dr["Usu_Estado"].ToString();
                    u.Usu_FechaHasta = Convert.ToDateTime(dr["Usu_FechaHasta"]);

                    entTipoUsuario t = new entTipoUsuario();
                    t.TipUsu_Id = Convert.ToInt32(dr["TipUsu_Id"]);
                    t.TipUsu_Codigo = dr["TipUsu_Codigo"].ToString();
                    t.TipUsu_Nombre = dr["TipUsu_Nombre"].ToString();
                    u.TipoUsuario = t;

                    entSucursal s = new entSucursal();
                    s.Suc_Id = Convert.ToInt32(dr["Suc_Id"]);
                    s.Suc_Codigo = dr["Suc_Codigo"].ToString();
                    s.Suc_Nombre = dr["Suc_Nombre"].ToString();
                    s.Suc_Direccion = dr["Suc_Direccion"].ToString();
                    s.Suc_Ciudad = dr["Suc_Ciudad"].ToString();
                    s.Suc_Telefono = dr["Suc_Telefono"].ToString();
                    u.Sucursal = s;

                    u.Usu_UsuarioRegistro = dr["Usu_UsuarioRegistro"].ToString();
                    u.Usu_FechaRegistro = Convert.ToDateTime(dr["Usu_FechaRegistro"]);
                    u.Usu_FechaModificacion = Convert.ToDateTime(dr["Usu_FechaModificacion"]);
                    u.Usu_UsuarioModificacion = dr["Usu_UsuarioModificacion"].ToString();

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
            return u;
        }

        public int RegUsuarioSecurity(String cadXML)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsUsuarioSecurity", cn);
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

        

        //public int InsUpdDelBloAct(String cadXML) {

        //    SqlCommand cmd = null;
        //    try
        //    {
        //        SqlConnection cn = Conexion.Instancia.Conectar();
        //        cmd = new SqlCommand("spIsnUpdDelBloActUsuario", cn);
        //        cmd.Parameters.AddWithValue("@prmstrCadXML", cadXML);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        //creamos el parametro de retorno
        //        SqlParameter m = new SqlParameter("@retorno", DbType.Int32);
        //        m.Direction = ParameterDirection.ReturnValue;
        //        cmd.Parameters.Add(m);
        //        //fin parametro
        //        cn.Open();
        //        cmd.ExecuteNonQuery();

        //        int i = Convert.ToInt32(cmd.Parameters["@retorno"].Value);

        //        return i;

        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    finally
        //    {
        //        cmd.Connection.Close();
        //    }

        //}

        #endregion metodos
    }
}
