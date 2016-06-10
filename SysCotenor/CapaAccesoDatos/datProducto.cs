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
    public class datProducto
    {
        #region Singleton

        private static readonly datProducto _Instancia = new datProducto();
        public static datProducto Instancia
        {
            get
            {
                return datProducto._Instancia;
            }
        }

        #endregion Singleton

        #region metodos

        public List<entProducto> ListaProductoSVA() {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entProducto> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListaProductoSVA", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entProducto>();
                while (dr.Read())
                {
                    entProducto p = new entProducto();
                    p.Pro_ID = Convert.ToInt32(dr["Pro_ID"]);
                    p.Pro_Codigo = dr["Pro_Codigo"].ToString();
                    p.Pro_Nombre = dr["Pro_Nombre"].ToString();
                    Lista.Add(p);
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

        public entProducto BucsaProd(int idprod)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            entProducto p = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spBuscaProducto", cn);
                cmd.Parameters.AddWithValue("@idprod", idprod);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    p = new entProducto();
                    p.Pro_ID = Convert.ToInt32(dr["Pro_ID"]);
                    p.Pro_Nombre = dr["Pro_Nombre"].ToString();
                    p.Pro_Codigo = dr["Pro_Codigo"].ToString();
                    p.Pro_Descripcion = dr["Pro_Descripcion"].ToString();
                    p.Pro_Imagen = dr["Pro_Imagen"].ToString();
                    entCategoria c = new entCategoria();
                    c.Cat_Id = Convert.ToInt32(dr["Cat_Id"]);
                    p.Categoria = c;
                    entPrecio pr = new entPrecio();
                    pr.Pre_ID = Convert.ToInt32(dr["Pre_ID"]);
                    p.Precio = pr;

                }

            }
            catch (Exception)
            {

                throw;
            }
            finally { cmd.Connection.Close(); }
            return p;

        }


        public int InsertUpdateProd(String cadxml)
        {
            SqlCommand cmd = null;
            int result = 0;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarProducto", cn);
                cmd.Parameters.AddWithValue("@cadXML", cadxml);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pr = new SqlParameter("@retorno", DbType.Int32);
                pr.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(pr);
                cn.Open();
                cmd.ExecuteNonQuery();
                return result = Convert.ToInt32(cmd.Parameters["@retorno"].Value);
            }
            catch (Exception)
            {

                throw;
            }
            finally { cmd.Connection.Close(); }

        }

        public List<entPrecio> Listprec()
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entPrecio> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListaPrecio", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entPrecio>();
                while (dr.Read())
                {
                    entPrecio p = new entPrecio();
                    p.Pre_ID = Convert.ToInt32(dr["Pre_ID"]);
                    p.Pre_producto = Convert.ToDouble(dr["Pre_producto"].ToString());
                    Lista.Add(p);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { cmd.Connection.Close(); }
            return Lista;
        }



        public List<entCategoria> Listacat()
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entCategoria> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListaCategoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entCategoria>();
                while (dr.Read())
                {
                    entCategoria c = new entCategoria();
                    c.Cat_Id = Convert.ToInt32(dr["Cat_Id"]);
                    c.Cat_Nombre = dr["Cat_Nombre"].ToString();
                    Lista.Add(c);
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally { cmd.Connection.Close(); }
            return Lista;
        }


        public List<entProducto> ListaProd()
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entProducto> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListaProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entProducto>();
                while (dr.Read())
                {
                    entProducto p = new entProducto();
                    p.Pro_ID = Convert.ToInt32(dr["Pro_ID"]);
                    p.Pro_Codigo = dr["Pro_Codigo"].ToString();
                    p.Pro_Nombre = dr["Pro_Nombre"].ToString();
                    p.Pro_Descripcion = dr["Pro_Descripcion"].ToString();
                    p.Pro_Imagen = dr["Pro_Imagen"].ToString();
                    entCategoria c = new entCategoria();
                    c.Cat_Id = Convert.ToInt32(dr["Cat_Id"]);
                    c.Cat_Nombre = dr["Cat_Nombre"].ToString();
                    p.Categoria = c;
                    entPrecio pr = new entPrecio();
                    pr.Pre_ID = Convert.ToInt32(dr["Pre_ID"]);
                    pr.Pre_producto = Convert.ToDouble(dr["Pre_producto"]);
                    p.Precio = pr;
                    Lista.Add(p);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally { cmd.Connection.Close(); }
            return Lista;
        }
        #endregion metodos
    }
}
