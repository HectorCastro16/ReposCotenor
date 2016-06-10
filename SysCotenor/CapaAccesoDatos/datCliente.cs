using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using System.Data;
using System.Data.SqlClient;

namespace CapaAccesoDatos
{
   public class datCliente {
        #region singleton
        private static readonly datCliente _instancia = new datCliente();
        public static datCliente Instancia{
            get { return datCliente._instancia;}
        }
        #endregion singleton

        #region metodos

        //public entCliente BuscaClienteVenta(String telefono,String dni,int intento){
        //    SqlCommand cmd = null;
        //    entCliente c = null;
        //    SqlDataReader dr = null;
        //    try {
        //        SqlConnection cn = Conexion.Instancia.Conectar();
        //        cmd = new SqlCommand("spBuscaClienteVenta", cn);
        //        cmd.Parameters.AddWithValue("@Telefono", telefono);
        //        cmd.Parameters.AddWithValue("@dni", dni);
        //        cmd.Parameters.AddWithValue("@intento", intento);

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cn.Open();
        //        dr = cmd.ExecuteReader();
        //        if (intento == 1)
        //        {
        //            if (dr.Read())
        //            {
        //                c = new entCliente();
        //                c.Cli_Id = Convert.ToInt32(dr["Cli_Id"]);
        //                c.Cli_Codigo = dr["Cli_Codigo"].ToString();
        //                c.Cli_Ruc = dr["Cli_Ruc"].ToString();
        //                c.Cli_Empresa = dr["Cli_Empresa"].ToString();
        //                entPersona_Telefono pt = new entPersona_Telefono();
        //                entTelefono t = new entTelefono();
        //                t.Tel_Id = Convert.ToInt32(dr["Tel_Id"]);
        //                t.Tel_Numero = dr["Tel_Numero"].ToString();
        //                pt.telefono = t;
        //                entPersona p = new entPersona();
        //                p.Per_Id = Convert.ToInt32(dr["Per_Id"]);
        //                p.Per_Codigo = dr["Per_Codigo"].ToString();
        //                p.Per_Nombres = dr["Per_Nombres"].ToString();
        //                p.Per_Apellidos = dr["Per_Apellidos"].ToString();
        //                p.Per_DNI = dr["Per_DNI"].ToString();
        //                p.Per_Direccion = dr["Per_Direccion"].ToString();
        //                p.Per_Celular = dr["Per_Celular"].ToString();
        //                p.Per_Correo = dr["Per_Correo"].ToString();
        //                p.Per_Foto = dr["Per_Foto"].ToString();
        //                p.Per_FechaNacimiento = Convert.ToDateTime(dr["Per_FechaNacimiento"].ToString());
        //                p.Per_LugarNacimiento = dr["Per_LugarNacimiento"].ToString();
        //                pt.persona = p;
        //                entDepartamento d = new entDepartamento();
        //                d.idDepa = Convert.ToInt32(dr["idDepa"]);
        //                d.departamento = dr["departamento"].ToString();
        //                c.Cli_Depardamento = d;
        //                entProvincia pr = new entProvincia();
        //                pr.idProv = Convert.ToInt32(dr["idProv"]);
        //                pr.provincia = dr["provincia"].ToString();
        //                c.Cli_Provincia = pr;
        //                entDistrito ds = new entDistrito();
        //                ds.idDist = Convert.ToInt32(dr["idDist"]);
        //                ds.distrito = dr["distrito"].ToString();
        //                c.Cli_Distrito = ds;
        //                c.Persona_telef = pt;
        //            }
        //        }
        //        else
        //        {
        //            if (dr.Read())
        //            {
        //                c = new entCliente();

        //                entPersona_Telefono pt = new entPersona_Telefono();
        //                entTelefono t = new entTelefono();
        //                pt.telefono = t;
        //                entPersona p = new entPersona();
        //                p.Per_Id = Convert.ToInt32(dr["Per_Id"]);
        //                p.Per_Codigo = dr["Per_Codigo"].ToString();
        //                p.Per_Nombres = dr["Per_Nombres"].ToString();
        //                p.Per_Apellidos = dr["Per_Apellidos"].ToString();
        //                p.Per_DNI = dr["Per_DNI"].ToString();
        //                p.Per_Direccion = dr["Per_Direccion"].ToString();
        //                p.Per_Celular = dr["Per_Celular"].ToString();
        //                p.Per_Correo = dr["Per_Correo"].ToString();
        //                p.Per_Foto = dr["Per_Foto"].ToString();
        //                p.Per_FechaNacimiento = Convert.ToDateTime(dr["Per_FechaNacimiento"].ToString());
        //                p.Per_LugarNacimiento = dr["Per_LugarNacimiento"].ToString();
        //                pt.persona = p;
        //                entDepartamento d = new entDepartamento();
        //                c.Cli_Depardamento = d;
        //                entProvincia pr = new entProvincia();
        //                c.Cli_Provincia = pr;
        //                entDistrito ds = new entDistrito();
        //                c.Cli_Distrito = ds;
        //                c.Persona_telef = pt;
        //            }
        //        }
        //    }
        //    catch (Exception){
        //    }finally { cmd.Connection.Close(); }
        //    return c;
        //}

        #endregion metodos


    }
}
