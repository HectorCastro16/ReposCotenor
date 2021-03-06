﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaAccesoDatos;

namespace CapaNegocio
{
    public class negUsuario
    {
        #region Singleton
        private static readonly negUsuario _Instancia = new negUsuario();
        public static negUsuario Instancia
        {
            get
            {
                return negUsuario._Instancia;
            }
        }
        #endregion Singleton

        #region metodos

        public int[] CountVentEfectivasXase(int idasesor){
            try
            {
                return datUsuario.Instancia.ConteoEfectivasXAsesor(idasesor);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<entTemporal> Asig_Total_Espera(int idUser){
            try{
            return datUsuario.Instancia.Total_Atend_Espera(idUser);
            }catch (Exception){
                throw;
            }
        }


        public entArticulo BuscaArt(String titulo)
        {
            try
            {
                return datUsuario.Instancia.BuscaArticulo(titulo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int PubArticulo(entArticulo a)
        {
            try
            {
                return datUsuario.Instancia.PublicaArticulo(a);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<entSecurity> ReturUltimoLogeo(int iduser){
            try
            {
                return  datUsuario.Instancia.UltimAcceso(iduser);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<entArticulo> ListaArt(){
            try
            {
                return datUsuario.Instancia.ListaArticluos();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int ActualizaPass(Int32 iduser,String newpass,String pasactual){
            try{
                Int32 i = datUsuario.Instancia.ActualizaPass(iduser,newpass,pasactual);
                if (i == 2){
                    throw new ArithmeticException("La contraseña ingresada no existe");
                }

                return i;
            }catch (Exception){
                throw;
            }
          }

        public entUsuario VerificarAccesoIntranet(String prmstrLogin, String prmstrPassw)
        {
            try
            {
                entUsuario u = null;
                u = datUsuario.Instancia.VerificarAccesoIntranet(prmstrLogin, prmstrPassw);

                if (u == null)
                {
                    throw new ApplicationException("Usuario y/o Contaseña No Validos");
                }
                if (u.Usu_Estado == "Bloqueado")
                {
                    throw new ApplicationException("Usuario Bloqueado - Comunicarse con el Departamento de Sistemas");
                }
                if (u.Usu_Estado == "Eliminado")
                {
                    throw new ApplicationException("Usuario Eliminado del Sistema");
                }
                DateTime fechaHoy = DateTime.Now;
                if (DateTime.Compare(fechaHoy, u.Usu_FechaHasta) > 0)
                {
                    throw new ApplicationException("Ha Expirado su Tiempo de Actividad en el Sistema");
                }

                return u;
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
        public List<entUsuario> ListaUsuarios(Int32 UsuarioId, Int32 SucursalId)
        {
            try
            {
                return datUsuario.Instancia.ListaUsuarios(UsuarioId, SucursalId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<entUsuario> ListaUsuariosAC()
        {
            try
            {
                return datUsuario.Instancia.ListaUsuariosAC();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<entUsuario> ListaUsuariosCall(Int32 UsuarioId, Int32 SucursalId)
        {
            try
            {
                List<entUsuario> lstTotal = datUsuario.Instancia.ListaUsuarios(UsuarioId, SucursalId);
               List<entUsuario> ListaPrevia=  datUsuario.Instancia.ListarUusariosConAsignacionCalls(UsuarioId);
                List<entUsuario> Returlst = new List<entUsuario>();
                for (int i = 0; i < lstTotal.Count; i++){
                    Returlst.Add(lstTotal[i]);
                    for (int j = 0; j < ListaPrevia.Count; j++){
                        if (lstTotal[i].Usu_Id == ListaPrevia[j].Usu_Id){
                            Returlst.Remove(lstTotal[i]);
                            Returlst.Add(ListaPrevia[j]);
                        }
                     }
                }
                return Returlst;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public entUsuario DetalleUsuario(Int32 UsuarioId, Int32 UsuarioIdSuper)
        {

            try
            {
                entUsuario u = datUsuario.Instancia.DetalleUsuario(UsuarioId, UsuarioIdSuper);
                if (u == null)
                {
                    throw new ApplicationException("Usuario no Encontrado");
                }
                return u;
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

        public int RegUsuarioSecurity(entUsuario u, Int16 TipoEdicion, String IpAcceso)
        {
            try
            {
                String cadXml = "";
                cadXml += "<UsuarioSecurity ";
                cadXml += "UsuarioID='" + u.Usu_Id.ToString() + "' ";
                cadXml += "Username='" + u.Usu_Login + "' ";
                cadXml += "IPAcceso='" + IpAcceso + "' ";
                cadXml += "TipoEdicion='" + TipoEdicion + "'/>";

                cadXml = "<root>" + cadXml + "</root>";

                int i = datUsuario.Instancia.RegUsuarioSecurity(cadXml);
                if (i <= 0) {
                    throw new ApplicationException("Problema SQL Server !"); }
                 
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
        
        public int InsUpdUsuario(entUsuario u, Int16 TipoEdicion)
        {

            try
            {
                String cadXml = "";
                cadXml += "<Persona ";
                cadXml += "Per_Id='" + u.Persona.Per_Id + "' ";
                cadXml += "Per_Nombres='" + u.Persona.Per_Nombres + "' ";
                cadXml += "Per_Apellidos='" + u.Persona.Per_Apellidos + "' ";
                cadXml += "Per_DNI='" + u.Persona.Per_DNI + "' ";
                cadXml += "Per_Celular='" + u.Persona.Per_Celular + "' ";
                cadXml += "Per_Correo='" + u.Persona.Per_Correo + "' ";
                cadXml += "Per_Direccion='" + u.Persona.Per_Direccion + "' ";
                cadXml += "Per_Foto='" + u.Persona.Per_Foto + "' ";
                cadXml += "Per_FechaNacimiento='" + u.Persona.Per_FechaNacimiento.ToString("yyyy/MM/dd") + "' ";
                cadXml += "Per_LugarNacimiento='" + u.Persona.Per_LugarNacimiento + "' ";
                cadXml += "TipoEdicion='" + TipoEdicion + "'>";

                        cadXml += "<Usuario ";
                        cadXml += "Usu_Id='" + u.Usu_Id + "' ";
                        cadXml += "Usu_TipUsu_Id='" + u.TipoUsuario.TipUsu_Id + "' ";
                        cadXml += "Usu_Suc_Id='" + u.Sucursal.Suc_Id + "' ";
                        cadXml += "Usu_FechaHasta='" + u.Usu_FechaHasta.ToString("yyyy/MM/dd") + "' ";
                        cadXml += "Usu_UsuarioRegistro='" + u.Usu_UsuarioRegistro + "' ";
                        cadXml += "Usu_UsuarioModificacion='" + u.Usu_UsuarioModificacion + "' ";
                        cadXml += "Usu_Telefono='" + u.Usu_Telefono + "' ";
                        cadXml += "TipoEdicion='" + TipoEdicion + "'/>";

                cadXml += "</Persona>";
                cadXml = "<root>" + cadXml + "</root>";

                if (TipoEdicion == 1)
                {
                    Int32 Dni_Ingreso = Convert.ToInt32(u.Persona.Per_DNI);
                    int val = datUsuario.Instancia.ValidaDni(Dni_Ingreso);
                    if (val > 0)
                    {
                        throw new ApplicationException("DNI Ya Registrado");
                    }
                }

                //variable i llega el resultado
                int i = datUsuario.Instancia.InsUpdDelBloAct(cadXml);

                if (TipoEdicion == 1)
                {
                    if (i <= 0)
                    {
                        throw new ApplicationException("No se Pudo Insertar a el trabajador");
                    }
                }
                if (TipoEdicion == 2)
                {
                    if (i <= 0)
                    {
                        throw new ApplicationException("No se Pudo Editar a el trabajador");
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

        public int InsUpdSupervisor(entUsuario u, Int16 TipoEdicion)
        {
            try
            {
                String cadXml = "";
                cadXml += "<Persona ";
                cadXml += "Per_Id='" + u.Persona.Per_Id + "' ";
                cadXml += "Per_Nombres='" + u.Persona.Per_Nombres + "' ";
                cadXml += "Per_Apellidos='" + u.Persona.Per_Apellidos + "' ";
                cadXml += "Per_DNI='" + u.Persona.Per_DNI + "' ";
                cadXml += "Per_Celular='" + u.Persona.Per_Celular + "' ";
                cadXml += "Per_Correo='" + u.Persona.Per_Correo + "' ";
                cadXml += "Per_Direccion='" + u.Persona.Per_Direccion + "' ";
                cadXml += "Per_Foto='" + u.Persona.Per_Foto + "' ";
                cadXml += "Per_FechaNacimiento='" + u.Persona.Per_FechaNacimiento.ToString("yyyy/MM/dd") + "' ";
                cadXml += "Per_LugarNacimiento='" + u.Persona.Per_LugarNacimiento + "' ";
                cadXml += "TipoEdicion='" + TipoEdicion + "'>";

                    cadXml += "<Usuario ";
                    cadXml += "Usu_Id='" + u.Usu_Id + "' ";
                    cadXml += "Usu_TipUsu_Id='" + u.TipoUsuario.TipUsu_Id + "' ";
                    cadXml += "Usu_Suc_Id='" + u.Sucursal.Suc_Id + "' ";
                    cadXml += "Usu_FechaHasta='" + u.Usu_FechaHasta.ToString("yyyy/MM/dd") + "' ";
                    cadXml += "Usu_UsuarioRegistro='" + u.Usu_UsuarioRegistro + "' ";
                    cadXml += "Usu_UsuarioModificacion='" + u.Usu_UsuarioModificacion + "' ";
                    cadXml += "Usu_Telefono='" + u.Usu_Telefono + "' ";
                    cadXml += "TipoEdicion='" + TipoEdicion + "'/>";

                cadXml += "</Persona>";
                cadXml = "<root>" + cadXml + "</root>";

                if (TipoEdicion == 1)
                {
                    Int32 Dni_Ingreso = Convert.ToInt32(u.Persona.Per_DNI);
                    int val = datUsuario.Instancia.ValidaDni(Dni_Ingreso);
                    if (val > 0)
                    {
                        throw new ApplicationException("DNI Ya Registrado");
                    }
                }

                //variable i llega el resultado
                int i = datUsuario.Instancia.InsUpdDelBloActSuper(cadXml);

                if (TipoEdicion == 1)
                {
                    if (i <= 0)
                    {
                        throw new ApplicationException("No se Pudo Insertar a el trabajador");
                    }
                }
                if (TipoEdicion == 2)
                {
                    if (i <= 0)
                    {
                        throw new ApplicationException("No se Pudo Editar a el trabajador");
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
        public int DelBloActUsurio(entUsuario u, Int16 TipoEdicion) {
            try
            {
                String cadXml = "";
                cadXml += "<Usuario ";
                cadXml += "Usu_Id='" + u.Usu_Id + "' ";
                cadXml += "Usu_UsuarioModificacion='" + u.Usu_UsuarioModificacion + "' ";
                cadXml += "TipoEdicion='" + TipoEdicion + "'/>";
                cadXml = "<root>" + cadXml + "</root>";
                
                if(TipoEdicion ==3){
                
                
                }
                //variable i llega el resultado
                int i = datUsuario.Instancia.InsUpdDelBloAct(cadXml);
                if (TipoEdicion == 3)
                {
                    if (i <= 0)
                    {
                        throw new ApplicationException("No se Pudo Eliminar a el trabajador");
                    }
                }
                if (TipoEdicion == 4)
                {
                    if (i <= 0)
                    {
                        throw new ApplicationException("No se Pudo Bloquear a el trabajador");
                    }
                }
                if (TipoEdicion == 4)
                {
                    if (i <= 0)
                    {
                        throw new ApplicationException("No se Pudo Activar a el trabajador");
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

        public int DelBloActSuper(entUsuario u, Int16 TipoEdicion)
        {
            try
            {
                String cadXml = "";
                cadXml += "<Usuario ";
                cadXml += "Usu_Id='" + u.Usu_Id + "' ";
                cadXml += "Usu_UsuarioModificacion='" + u.Usu_UsuarioModificacion + "' ";
                cadXml += "TipoEdicion='" + TipoEdicion + "'/>";
                cadXml = "<root>" + cadXml + "</root>";
                //variable i llega el resultado
                int i = datUsuario.Instancia.InsUpdDelBloActSuper(cadXml);
                if (TipoEdicion == 3)
                {
                    if (i <= 0)
                    {
                        throw new ApplicationException("No se Pudo Eliminar a el trabajador");
                    }
                }
                if (TipoEdicion == 4)
                {
                    if (i <= 0)
                    {
                        throw new ApplicationException("No se Pudo Bloquear a el trabajador");
                    }
                }
                if (TipoEdicion == 4)
                {
                    if (i <= 0)
                    {
                        throw new ApplicationException("No se Pudo Activar a el trabajador");
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

        public List<entUsuario> ListaSupersCall(Int32 SucursalId) {
            try
            {
                return datUsuario.Instancia.ListaSupersCall(SucursalId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int InsUpdDelBloActAsignacionUsuario(entUsuario usu, Int16 TipoEdicion) {

            try
            {
                List<entAsigncionLlamadas> ListAsiLla = new List<entAsigncionLlamadas>();
                ListAsiLla = usu.lsAsiLla;

                String cadXml = "";
                cadXml += "<AsignaLlamada ";
                cadXml += "TipoEdicion='" + TipoEdicion + "'>";
                foreach (entAsigncionLlamadas al in ListAsiLla) {
                    cadXml += "<AsiLla ";
                    cadXml += "Asi_Usu_Id='" + al.Usuario.Usu_Id + "' ";
                    cadXml += "Asi_CliTel_Id='" + al.ClienteTelefono.CliTel_Id + "' ";
                    cadXml += "Asi_UsuarioRegistro='" + al.Asi_UsuarioRegistro + "' ";
                    cadXml += "TipoEdicion='" + TipoEdicion + "'/>";
                }
                cadXml += "</AsignaLlamada>";
                cadXml = "<root>" + cadXml + "</root>";

                int i = datUsuario.Instancia.InsUpdDelBloActAsignacionUsuario(cadXml);
                if (TipoEdicion == 1)
                {
                    if (i <= 0)
                    {
                        throw new ApplicationException("No se Pudo Asignar Data de Cliente");
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

        #endregion metodos
    }
}
