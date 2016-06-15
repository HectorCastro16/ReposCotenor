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
    public class datCliente
    {
        #region singleton
        private static readonly datCliente _instancia = new datCliente();
        public static datCliente Instancia
        {
            get { return datCliente._instancia; }
        }
        #endregion singleton

        #region metodos

        public List<entTipDoc> ListaTipDoc()
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entTipDoc> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListaDocumentos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entTipDoc>();
                while (dr.Read())
                {
                    entTipDoc d = new entTipDoc();
                    d.td_id = Convert.ToInt32(dr["td_id"]);
                    d.td_nombre = dr["td_nombre"].ToString();
                    d.td_Descripcion = dr["td_Descripcion"].ToString();
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


        public List<entCliente_Telefono> BuscaCliente(String telefono, String NumDoc)
        {
            SqlCommand cmd = null;
            List<entCliente_Telefono> Lista = null;
            SqlDataReader dr = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spBuscaCliente", cn);
                cmd.Parameters.AddWithValue("@TELEFONO", telefono);
                cmd.Parameters.AddWithValue("@NRO_DOCUMENTO", NumDoc);

                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entCliente_Telefono>();
                while (dr.Read())
                {
                   entCliente c = new entCliente();
                    c.Cli_Id = Convert.ToInt32(dr["Cli_Id"]);
                    c.Cli_Codigo = dr["Cli_Codigo"].ToString();
                    c.Cli_Nombre = dr["Cli_Nombre"].ToString();
                    c.Cli_RazonSocial = dr["Cli_RazonSocial"].ToString();
                    c.Cli_Numero_Documento = dr["Cli_Numero_Documento"].ToString();
                    c.Cli_FechaNacimiento = Convert.ToDateTime(dr["Cli_FechaNacimiento"]);
                    c.Cli_LugarNacimiento = dr["Cli_LugarNacimiento"].ToString();
                    c.Cli_Correo = dr["Cli_Correo"].ToString();
                    c.Cli_Telefono_Referencia = dr["Cli_Telefono_Referencia"].ToString();
                    c.Cli_Estado = dr["Cli_Estado"].ToString();
                    c.Cli_FechaRegistro = Convert.ToDateTime(dr["Cli_FechaRegistro"]);
                    c.Cli_FechaModificacion = Convert.ToDateTime(dr["Cli_FechaModificacion"]);
                    entTipDoc d = new entTipDoc();
                    d.td_Descripcion = dr["td_Descripcion"].ToString();
                    c.TipDoc = d;
                    entSegmento s = new entSegmento();
                    s.Seg_Nombre = dr["Seg_Nombre"].ToString();
                    c.Segmento = s;
                    entTelefono t = new entTelefono();
                    t.Tel_Id = Convert.ToInt32(dr["Tel_Id"]);
                    t.Tel_Numero = dr["Tel_Numero"].ToString();
                    t.Tel_Producto = dr["Tel_Producto"].ToString();
                    t.Tel_Direccion = dr["Tel_Direccion"].ToString();
                    t.Tel_FechaAlta = Convert.ToDateTime(dr["Tel_FechaAlta"]);
                    t.Tel_F1 =Convert.ToDouble(dr["Tel_F1"]);
                    t.Tel_F2 = Convert.ToDouble(dr["Tel_F2"]);
                    t.Tel_F3 = Convert.ToDouble(dr["Tel_F3"]);
                    entCliente_Telefono cl = new entCliente_Telefono();
                    cl.Cliente = c;
                    cl.Telefono = t;
                    Lista.Add(cl);
                    
                }
            }
            catch (Exception)
            {
            }
            finally { cmd.Connection.Close(); }
            return Lista;
        }

        #endregion metodos


    }
}
