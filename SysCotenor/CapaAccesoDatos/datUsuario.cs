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

        public int PublicaArticulo(entArticulo a){
            SqlCommand cmd = null;
            int i = 0;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spPubArticulo", cn);
                cmd.Parameters.AddWithValue("@iduser", a.usuario.Usu_Id);
                cmd.Parameters.AddWithValue("@image", a.Art_Image);
                cmd.Parameters.AddWithValue("@url", a.Art_Url);
                cmd.Parameters.AddWithValue("@titulo", a.Art_Titulo);
                cmd.Parameters.AddWithValue("@descripcion", a.Art_Descripcion);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter p = new SqlParameter("@retorno", DbType.Int32);
                p.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(p);
                cn.Open();
                cmd.ExecuteNonQuery();
               i  = Convert.ToInt32(cmd.Parameters["@retorno"].Value);

               
            }
            catch (Exception)
            {
                throw;
            }
            finally { cmd.Connection.Close(); }
            return i;
        }



        public List<entSecurity> UltimAcceso(int idUsuario){
            SqlCommand cmd = null;
            SqlDataReader dr = null;
           List<entSecurity> sl = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListaLogeo", cn);
                cmd.Parameters.AddWithValue("@iduser", idUsuario);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                sl = new List<entSecurity>();
                while (dr.Read())
                {
                    entSecurity s = new entSecurity();
                    s = new entSecurity();
                    s.UsuarioSecurity = Convert.ToInt32(dr["UsuarioSecurity"]);
                    s.UltimoAcceso = Convert.ToDateTime(dr["UltimoAcceso"].ToString());
                    s.IPAcceso = dr["IPAcceso"].ToString();
                    sl.Add(s);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { cmd.Connection.Close(); }return sl;
        }

        public List<entArticulo> ListaArticluos(){
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entArticulo> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListArticulos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entArticulo>();
                while (dr.Read())
                {
                    entArticulo a = new entArticulo();
                    a.Art_Id = Convert.ToInt32(dr["Art_Id"]);
                    a.Art_FechaPublicacion = Convert.ToDateTime(dr["Art_FechaPublicacion"]);
                    a.Art_Image = dr["Art_Image"].ToString();
                    a.Art_Url = dr["Art_Url"].ToString();
                    a.Art_Titulo = dr["Art_Titulo"].ToString();
                    a.Art_Descripcion = dr["Art_Descripcion"].ToString();
                    entUsuario u = new entUsuario();
                    entTipoUsuario tu = new entTipoUsuario();
                    tu.TipUsu_Nombre = dr["TipUsu_Nombre"].ToString();
                    u.TipoUsuario = tu;
                    entPersona p = new entPersona();
                    p.Per_Nombres = dr["Per_Nombres"].ToString();
                    p.Per_Apellidos = dr["Per_Apellidos"].ToString();
                    u.Persona = p;
                    a.usuario = u;
                    Lista.Add(a);

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { cmd.Connection.Close(); }
            return Lista;
        }
             


        public int ActualizaPass(Int32 iduser,String newpass,String actualpass){
            SqlCommand cmd = null;
            int i = 0;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spActualizarPass", cn);
                cmd.Parameters.AddWithValue("@idusuario", iduser);
                cmd.Parameters.AddWithValue("@nuevapass", newpass);
                cmd.Parameters.AddWithValue("@passactual", actualpass);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter p = new SqlParameter("@retorno", DbType.Int32);
                p.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(p);
                cn.Open();
                cmd.ExecuteNonQuery();
                i  = Convert.ToInt32(cmd.Parameters["@retorno"].Value);
            }
            catch (Exception){
                throw;
            }finally { cmd.Connection.Close();}
            return i;
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
                    u.usu_Config_Color = dr["usu_Config_Color"].ToString();
                    u.Usu_Telefono = dr["Usu_Telefono"].ToString();

                    entPersona p = new entPersona();
                    p.Per_Nombres = dr["Per_Nombres"].ToString();
                    p.Per_Apellidos = dr["Per_Apellidos"].ToString();
                    p.Per_DNI = dr["Per_DNI"].ToString();
                    p.Per_Celular = dr["Per_Celular"].ToString();
                    p.Per_Correo = dr["Per_Correo"].ToString();
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


        public List<entUsuario> ListarUusariosConAsignacionCalls(Int32 user){
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entUsuario> Lista = null;
            try{
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("sp_ListaAsLlamadas", cn);
                cmd.Parameters.AddWithValue("@idsuper", user);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entUsuario>();
                while (dr.Read()){
                    entUsuario u = new entUsuario();
                    u.Usu_Id = Convert.ToInt32(dr["Usu_Id"]);
                    u.Usu_Codigo = dr["Usu_Codigo"].ToString();
                    
                    entPersona p = new entPersona();
                    p.Per_Nombres = dr["Per_Nombres"].ToString();
                    p.Per_Apellidos = dr["Per_Apellidos"].ToString();   
                    u.Persona = p;

                    u.Contador = Convert.ToInt32(dr["Asgnadas"]);
                    Lista.Add(u);
                }
            }
            catch (Exception ex){
                throw ex;
            }finally { cmd.Connection.Close(); }
            return Lista;
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
                    p.Per_Correo = dr["Per_Correo"].ToString();
                    p.Per_Celular = dr["Per_Celular"].ToString();
                 //   p.Per_Telefono = dr["Per_Telefono"].ToString();
                    u.Persona = p;

                    u.Usu_Telefono = dr["Usu_Telefono"].ToString();
                    u.Usu_Estado = dr["Usu_Estado"].ToString();
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
                    //p.Per_Telefono = dr["Per_Telefono"].ToString();
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

                    u.Usu_Telefono = dr["Usu_Telefono"].ToString();
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


        public int InsUpdDelBloAct(String cadXML)
        {

            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spIsnUpdDelBloActUsuario", cn);
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

        public int InsUpdDelBloActSuper(String cadXML)
        {

            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spIsnUpdDelBloActSupervisor", cn);
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

        public int ValidaDni(Int32 dni) {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            int i = 0;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spValidaDNI", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmtIntDNI", dni);
                cn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read()) {
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
