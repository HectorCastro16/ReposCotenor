using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaAccesoDatos;

namespace CapaNegocio
{
    public class negCliente
    {
        #region singleton

        private static readonly negCliente _intancia = new negCliente();
        public static negCliente Instancia
        {
            get { return negCliente._intancia; }
        }
        #endregion singleton

        #region metodos 

        public List<entPedido> BuscaRegLlamadas(String telefono) {
            try
            {
                List<entPedido> Lista = new List<entPedido>();
                Lista = datCliente.Instancia.ListaHistllamadas(telefono);
                if (Lista.Count <= 0){
                    throw new ApplicationException("No se encontraron registros");
                }
                return Lista;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<entTipDoc> ListaDoc()
        {
            try
            {
                return datCliente.Instancia.ListaTipDoc();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<entCliente_Telefono> BuscaCliente(String telf, int tipdoc, String numDoc)
        {
            try
            {
               List<entCliente_Telefono> Lista = null;
                if (telf != null)
                {
                    {
                        if (telf.Trim().Length != 8)
                        {
                            throw new ApplicationException("El Teléfono no tiene formato válido");
                        }
                        Lista = datCliente.Instancia.BuscaCliente(telf, numDoc="0");
                    }
                }
                #region valida_num_doc-----------------------------------------------
                else
                {
                    if (tipdoc != 0)
                    {
                        #region valida_DNI-----------------------------------------------
                        if (tipdoc == 1)
                        {
                            if (numDoc.Trim().Length != 8)
                            {
                                throw new ApplicationException("DNI no tiene formato válido");
                            }
                        }
                        #endregion

                        #region valida_RUC-----------------------------------------------
                        else if (tipdoc == 2)
                        {
                            if (numDoc.Trim().Length != 11)
                            {
                                throw new ApplicationException("RUC no tiene formato válido");
                            }
                        }
                        #endregion

                        #region valida_CEX-----------------------------------------------
                        else if (tipdoc == 3)
                        {
                            if (numDoc.Trim().Length != 12)
                            {
                                throw new ApplicationException("Carnet de Extranjeria no tiene formato válido");
                            }
                        }
                        #endregion

                        #region valida_PASAPORTE-----------------------------------------
                        else if (tipdoc == 4)
                        {
                            if (numDoc.Trim().Length != 12)
                            {
                                throw new ApplicationException("PASAPORTE no tiene formato válido");
                            }
                        }
                        #endregion

                        #region valida_PART NACIMIENTO-----------------------------------
                        else if (tipdoc == 5)
                        {
                            if (numDoc.Trim().Length != 15)
                            {
                                throw new ApplicationException("PART. DE NACIMIENTO-IDENTIDAD no tiene formato válido");
                            }
                        }
                        #endregion

                        #region valida_OTRO----------------------------------------------
                        else if (tipdoc == 6)
                        {
                            if (numDoc.Trim().Length != 15)
                            {
                                throw new ApplicationException("El documento no tiene formato válido");
                            }
                        }
                        #endregion

                        Lista = datCliente.Instancia.BuscaCliente(telf="0", numDoc);
                    }
                    else {
                        throw new ApplicationException("No se especificó el tipo de documento");
                    }
                }
                #endregion

                if (Lista.Count<=0)
                {
                    throw new ApplicationException("No se encontraron registros");
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion metodos

    }
}
