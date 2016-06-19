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
    public class datAsigncionLlamadas
    {
        #region Singleton

        private static readonly datAsigncionLlamadas _Instancia = new datAsigncionLlamadas();
        public static datAsigncionLlamadas Instancia
        {
            get
            {
                return datAsigncionLlamadas._Instancia;
            }
        }

        #endregion Singleton

        #region metodos

        public List<entAsigncionLlamadas> ListaClientesAsignadosXUsuario(Int32 UsuarioId)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entAsigncionLlamadas> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListaClientesAsignadosXUsuario", cn);
                cmd.Parameters.AddWithValue("@prmtIntIdUsu", UsuarioId);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entAsigncionLlamadas>();

                while (dr.Read())
                {
                    entSegmento s = new entSegmento();
                    s.Seg_Id = Convert.ToInt32(dr["Cli_Seg_Id"]);

                    entCliente c = new entCliente();
                    c.Cli_Id = Convert.ToInt32(dr["Cli_Id"]);
                    c.Cli_Nombre = dr["Cli_Nombre"].ToString();
                    c.Cli_RazonSocial = dr["Cli_RazonSocial"].ToString();
                    c.Cli_Estado = dr["Cli_Estado"].ToString();
                    c.Cli_FechaRegistro = Convert.ToDateTime(dr["Cli_FechaRegistro"]);
                    c.Segmento = s;

                    entTelefono t = new entTelefono();
                    t.Tel_Id = Convert.ToInt32(dr["Tel_Id"]);
                    t.Tel_Numero = dr["Tel_Numero"].ToString();
                    t.Tel_Producto = dr["Tel_Producto"].ToString();
                    t.Tel_FechaAlta = Convert.ToDateTime(dr["Tel_FechaAlta"]);

                    entCliente_Telefono ct = new entCliente_Telefono();
                    ct.CliTel_Id = Convert.ToInt32(dr["CliTel_Id"]);
                    ct.Cliente = c;
                    ct.Telefono = t;

                    entAsigncionLlamadas al = new entAsigncionLlamadas();
                    al.Asi_Id = Convert.ToInt32(dr["Asi_Id"]);
                    al.ClienteTelefono = ct;

                    Lista.Add(al);

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

        public entAsigncionLlamadas BuscaAsiLla(Int32 UsuarioId,Int32 AsiLlaId) {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            entAsigncionLlamadas al = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spBuscaAsiLla", cn);
                cmd.Parameters.AddWithValue("@prmtIntIdAsiUsu", UsuarioId);
                cmd.Parameters.AddWithValue("@prmtIntIdAsiLla", AsiLlaId);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();

                if (dr.Read()) {
                    entSegmento s = new entSegmento();
                    s.Seg_Nombre = dr["Seg_Nombre"].ToString();

                    entTipDoc td = new entTipDoc();
                    td.td_nombre = dr["td_nombre"].ToString();

                    entCliente c = new entCliente();
                    c.Cli_Id = Convert.ToInt32(dr["Cli_Id"]);
                    c.Cli_Nombre = dr["Cli_Nombre"].ToString();
                    c.Cli_Numero_Documento = dr["Cli_Numero_Documento"].ToString();
                    c.Cli_RazonSocial = dr["Cli_RazonSocial"].ToString();
                    c.TipDoc = td;
                    c.Segmento = s;

                    entTelefono t = new entTelefono();
                    t.Tel_Id = Convert.ToInt32(dr["Tel_Id"]);
                    t.Tel_Numero = dr["Tel_Numero"].ToString();
                    t.Tel_Producto = dr["Tel_Producto"].ToString();
                    t.Tel_Direccion = dr["Tel_Direccion"].ToString();
                    t.Tel_FechaAlta = Convert.ToDateTime(dr["Tel_FechaAlta"]);
                    t.Tel_F1 = Convert.ToDouble(dr["Tel_F1"]);
                    t.Tel_F2 = Convert.ToDouble(dr["Tel_F2"]);
                    t.Tel_F3 = Convert.ToDouble(dr["Tel_F3"]);

                    entUsuario u = new entUsuario();
                    u.Usu_Id = Convert.ToInt32(dr["Asi_Usu_Id"]);

                    entCliente_Telefono ct = new entCliente_Telefono();
                    ct.CliTel_Id = Convert.ToInt32(dr["CliTel_Id"]);
                    ct.Cliente = c;
                    ct.Telefono = t;

                    al = new entAsigncionLlamadas();
                    al.Asi_Id = Convert.ToInt32(dr["Asi_Id"]);
                    al.Usuario = u;
                    al.ClienteTelefono = ct;
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
            return al;
        }

        #endregion metodos
    }
}
