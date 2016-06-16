using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaAccesoDatos;

namespace CapaNegocio
{
    public class negCliente_Telefono
    {
        #region Singleton
        private static readonly negCliente_Telefono _Instancia = new negCliente_Telefono();
        public static negCliente_Telefono Instancia
        {
            get
            {
                return negCliente_Telefono._Instancia;
            }
        }
        #endregion Singleton

        #region metodos

        public int InsUpdCliente(entCliente_Telefono ct, Int16 TipoEdicion){
            try
            {

                List<entProducto> Svas = new List<entProducto>();
                Svas = ct.Cliente.SVAS;
                Int32 TieneSVA = 0;
                TieneSVA = (Svas != null) ? 1 : 0;

                String cadXml = "";
                cadXml += "<CliTel ";
                cadXml += "CliTel_UsuarioRegistro='" + ct.CliTel_UsuarioRegistro + "' ";               
                cadXml += "TieneSVA='" + TieneSVA + "' ";
                cadXml += "TipoEdicion='" + TipoEdicion + "'>";

                    cadXml += "<Cli ";
                    cadXml += "Cli_TipDoc_Id='" + ct.Cliente.TipDoc.td_id+ "' ";
                    cadXml += "Cli_Nombre='" + ct.Cliente.Cli_Nombre + "' ";
                    cadXml += "Cli_RazonSocial='" + ct.Cliente.Cli_RazonSocial + "' ";
                    cadXml += "Cli_Seg_Id='" + ct.Cliente.Segmento.Seg_Id+ "' ";
                    cadXml += "Cli_Numero_Documento='" + ct.Cliente.Cli_Numero_Documento+ "' ";
                    cadXml += "Cli_UsuarioRegistro='" + ct.Cliente.Cli_UsuarioRegistro + "' ";
                    cadXml += "TipoEdicion='" + TipoEdicion + "'>";

                    if (TieneSVA != 0) {
                        foreach (entProducto p in ct.Cliente.SVAS) {
                            cadXml += "<ProCon ";
                        cadXml += "ProCon_Pro_Id='" + p.Pro_ID + "' ";
                        cadXml += "ProCon_UsuarioRegistro='" + ct.CliTel_UsuarioRegistro + "'/>"; 
                        }                    
                    }                      

                    cadXml += "</Cli>";     

                    cadXml += "<Tel ";
                    cadXml += "Tel_Numero='" + ct.Telefono.Tel_Numero + "' ";
                    cadXml += "Tel_Producto='" + ct.Telefono.Tel_Producto + "' ";
                    cadXml += "Tel_Direccion='" + ct.Telefono.Tel_Direccion + "' ";
                    cadXml += "Tel_FechaAlta='" + ct.Telefono.Tel_FechaAlta.ToString("yyyy/MM/dd") + "' ";
                    cadXml += "Tel_F1='" + ct.Telefono.Tel_F1 + "' ";
                    cadXml += "Tel_F2='" + ct.Telefono.Tel_F2 + "' "; 
                    cadXml += "Tel_F3='" + ct.Telefono.Tel_F3 + "' ";
                    cadXml += "TipoEdicion='" + TipoEdicion + "'/>";

                cadXml += "</CliTel>";
                cadXml = "<root>" + cadXml + "</root>";

                if (TipoEdicion == 1)
                {
                    String Telefono_Ingreso = ct.Telefono.Tel_Numero.ToString();
                    int val = datTelefono.Instancia.ValidaTelefono(Telefono_Ingreso);
                    if (val > 0)
                    {
                        throw new ApplicationException("Telefono Ya Registrado");
                    }

                }
                int i = datCliente_Telefono.Instancia.InsUpdDelBloActCliente(cadXml);
                if (TipoEdicion == 1)
                {
                    if (i <= 0)
                    {
                        throw new ApplicationException("No se Pudo Insertar a el Cliente");
                    }
                }
                if (TipoEdicion == 2)
                {
                    if (i <= 0)
                    {
                        throw new ApplicationException("No se Pudo Editar a el Cliente");
                    }
                }
                return i;
            }
            catch (ApplicationException ae)
            {
                throw ae;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public List<entCliente_Telefono> ListaClientesParaAsignar() {

            try
            {
                return datCliente_Telefono.Instancia.ListaClientesParaAsignar();
            }
            catch (Exception)
            {

                throw;
            }
        }


            #endregion metodos
    }
}
