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
   public class datArbolVenta{
        #region singleton

        private static readonly datArbolVenta _instancia = new datArbolVenta();
        public static datArbolVenta Instancia{
            get { return datArbolVenta._instancia; }
        }
        #endregion singleton


        #region metodos 


        public List<entProducto> ListarProdcts(Int32 id_Cat)
        {
        SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entProducto> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spProducto", cn);
                cmd.Parameters.AddWithValue("@id_Categoria", id_Cat);
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

        
        public List<entCategoria> ListarCatGorias(Int32 id_GrupCom)
        {

            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entCategoria> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spCategoria", cn);
                cmd.Parameters.AddWithValue("@id_grup_com", id_GrupCom);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entCategoria>();
                while (dr.Read())
                {
                    entCategoria c = new entCategoria();
                    c.Cat_Id = Convert.ToInt32(dr["Cat_Id"]);
                    c.Cat_Codigo = dr["Cat_Codigo"].ToString();
                    c.Cat_Nombre = dr["Cat_Nombre"].ToString();
                    c.Cat_Descripcion = dr["Cat_Descripcion"].ToString(); 
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

        public List<entGrupoComercial> ListarGrup_Com(Int32 id_DetAcc){

            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entGrupoComercial> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spGrupoComercial", cn);
                cmd.Parameters.AddWithValue("@DetAcc_ID", id_DetAcc);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entGrupoComercial>();
                while (dr.Read())
                {
                    entGrupoComercial gc = new entGrupoComercial();
                    gc.Gru_ID = Convert.ToInt32(dr["Gru_ID"]);
                    gc.Gru_Codigo = dr["Gru_Codigo"].ToString();
                    gc.Gru_Nombre = dr["Gru_Nombre"].ToString();
                    gc.Gru_Descripcion = dr["Gru_Descripcion"].ToString();
                    Lista.Add(gc);
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally { cmd.Connection.Close(); }

            return Lista;
        }



        public List<entDetalleAccionComercial> ListarDetAcc_Com(Int32 idAcc_Com){
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entDetalleAccionComercial> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spDet_AccComercial", cn);
                cmd.Parameters.AddWithValue("@Acc_Id", idAcc_Com);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entDetalleAccionComercial>();
                while (dr.Read())
                {
                    entDetalleAccionComercial det = new entDetalleAccionComercial();
                    det.DetAcc_ID = Convert.ToInt32(dr["DetAcc_ID"]);
                    det.DetAcc_Codigo = dr["DetAcc_Codigo"].ToString();
                    det.DetAcc_Nombre = dr["DetAcc_Nombre"].ToString();
                    det.DetAcc_Descripcion = dr["DetAcc_Descripcion"].ToString();
                    Lista.Add(det);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally { cmd.Connection.Close(); }
            return Lista;
        }



        public List<entAccionComercial> ListarCboAcc_Com(){
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entAccionComercial> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListaACcbo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entAccionComercial>();
                while (dr.Read())
                {
                    entAccionComercial ac = new entAccionComercial();
                    ac.Acc_Id = Convert.ToInt32(dr["Acc_Id"]);
                    ac.Acc_Codigo = dr["Acc_Codigo"].ToString();
                    ac.Acc_Nombre = dr["Acc_Nombre"].ToString();
                    ac.Acc_Descripcion = dr["Acc_Descripcion"].ToString();
                    Lista.Add(ac);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally { cmd.Connection.Close();}
            return Lista;
        }




        #endregion metodos



    }
}
