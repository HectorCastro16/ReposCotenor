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
    public class datCliente_Telefono
    {
        #region Singleton

        private static readonly datCliente_Telefono _Instancia = new datCliente_Telefono();
        public static datCliente_Telefono Instancia
        {
            get
            {
                return datCliente_Telefono._Instancia;
            }
        }

        #endregion Singleton

        #region metodos

        public int InsUpdDelBloActCliente(String cadXML)
        {

            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsUpdDelBloActCliente", cn);
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

        public List<entCliente_Telefono> ListaClientesParaAsignar()
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entCliente_Telefono> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListaClientesParaAsignar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entCliente_Telefono>();

                while (dr.Read())
                {
                    entCliente_Telefono ct = new entCliente_Telefono();
                    ct.CliTel_Id = Convert.ToInt32(dr["CliTel_Id"]);

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

                    ct.Cliente = c;
                    ct.Telefono = t;
                    Lista.Add(ct);

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
