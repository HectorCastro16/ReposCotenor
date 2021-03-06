﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaAccesoDatos;

namespace CapaNegocio
{
    public class negProducto
    {
        #region Singleton
        private static readonly negProducto _Instancia = new negProducto();
        public static negProducto Instancia
        {
            get
            {
                return negProducto._Instancia;
            }
        }
        #endregion Singleton

        #region metodos


       

        public entProducto BuscaProd(int idpro)
        {
            try
            {
                return datProducto.Instancia.BucsaProd(idpro);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int InsUpdProducto(entProducto p, int tipedic, String userRM,int tepprecio )
        {
            try
            {
                String cadxml = "";
                cadxml += "<producto ";
                cadxml += "id='" + p.Pro_ID + "' ";
                cadxml += "categoria='" + p.Categoria.Cat_Id + "' ";
                cadxml += "valprecio='" + p.Precio.Pre_producto.ToString().Replace(",",".") + "' ";
                cadxml += "precio='" + p.Precio.Pre_ID + "' ";
                cadxml += "nombre='" + p.Pro_Nombre + "' ";
                cadxml += "descripcion='" + p.Pro_Descripcion + "' ";
                cadxml += "imagen='" + p.Pro_Imagen + "' ";
                cadxml += "tipoedicion='" + tipedic + "' ";
                cadxml += "teprecio='" + tepprecio + "' ";
                cadxml += "userRM='" + userRM + "'/>";

                cadxml = "<root>" + cadxml + "</root>";
                int i = datProducto.Instancia.InsertUpdateProd(cadxml);
                return i;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<entPrecio> ListPrec()
        {
            try
            {
                return datProducto.Instancia.Listprec();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<entCategoria> ListCatego()
        {
            try
            {
                return datProducto.Instancia.Listacat();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<entProducto> ListProd()
        {
            try
            {
                return datProducto.Instancia.ListaProd();
            }
            catch (Exception)
            {
                throw;
            }
        }




        public List<entProducto> ListaProductoSVA() {
            try
            {
                return datProducto.Instancia.ListaProductoSVA();
            }
            catch (Exception e)
            {
                
                throw e;
            }
        
        }

        #endregion metodos
    }
}
