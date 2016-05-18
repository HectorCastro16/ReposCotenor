using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace CapaPresentacion.Controllers.Intranet
{
    public class ComunController : Controller
    {
        //
        // GET: /Comun/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult InsUsuario(String mensaje, Int16? identificador)
        {
            try
            {
                ViewBag.mensaje = mensaje;
                ViewBag.identificador = identificador;
                entUsuario u = (entUsuario)Session["usuario"];
                if (u != null)
                {
                    List<entTipoUsuario> t = null;

                    if (u.TipoUsuario.TipUsu_Id == 3)
                    {
                        t = negTipoUsuario.Instancia.ListaTipoUsuarioxId(8);
                    }
                    if (u.TipoUsuario.TipUsu_Id == 6)
                    {
                        t = negTipoUsuario.Instancia.ListaTipoUsuarioxId(9);
                    }
                    var lsTipoUsuario = new SelectList(t, "TipUsu_Id", "TipUsu_Nombre");
                    ViewBag.ListaTipoUsuario = lsTipoUsuario;

                    List<entSucursal> s = negSucursal.Instancia.ListaSucursalxId(u.Sucursal.Suc_Id);
                    var lsSucursal = new SelectList(s, "Suc_Id", "Suc_Nombre");
                    ViewBag.ListaSucursal = lsSucursal;

                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Inicio");
                }

            }
            catch (ApplicationException x)
            {
                ViewBag.mensaje = x.Message;
                return RedirectToAction("ListaUsuarios", "Comun", new { mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {

                return RedirectToAction("ListaUsuarios", "Comun", new { mensaje = e.Message, identificador = 2 });
            }
           
        }

        [HttpPost]
        public ActionResult InsUsuario(entUsuario u, HttpPostedFileBase archivo, FormCollection form)
        {
            
            
            try
            {
                String dominio = form["dominio"];
                String fecNac = form["txtFecNac"];
                String fecHasta = form["txtFecHasta"];
                if (dominio != "Otros" && dominio != "Seleccionar")
                    u.Persona.Per_Correo += "@" + dominio;

                u.Persona.Per_FechaNacimiento = Convert.ToDateTime(fecNac);
                u.Usu_FechaHasta = Convert.ToDateTime(fecHasta);

                if (archivo != null && archivo.ContentLength > 0)
                {
                    u.Persona.Per_Foto = Path.GetFileName(archivo.FileName);
                }
                else
                {
                    u.Persona.Per_Foto = "foto.png";
                }

                //para capturar el usuario en sesion////////////
                entUsuario us = (entUsuario)Session["usuario"];
                String userRegistro = us.Persona.NombreCompleto;

                u.Usu_UsuarioRegistro = userRegistro;
                ///////////////////////////////////////////

                int i = negUsuario.Instancia.InsUpdUsuario(u, 1);
                //int i = 0;

                if (i > 0)
                {
                    if (archivo != null && archivo.ContentLength > 0)
                    {

                        var namearchivo = Path.GetFileName(archivo.FileName);
                        var ruta = Path.Combine(Server.MapPath("~/assets/Imagenes/Fotos"), namearchivo);
                        archivo.SaveAs(ruta);
                    }
                    return RedirectToAction("ListaUsuarios", new { mensaje = "Se Inserto Satisfactoriamente", identificador = 3 });
                }
                else
                {
                    return RedirectToAction("ListaUsuarios", "Comun", new { mensaje = "No se pudo registrar", identificador = 2 });
                }
            }
            catch (ApplicationException x)
            {
                ViewBag.mensaje = x.Message;
                return RedirectToAction("InsUsuario", "Comun", new { mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {

                return RedirectToAction("InsUsuario", "Comun", new { mensaje = e.Message, identificador = 2 });
            }

        }

        public ActionResult ListaUsuarios(String mensaje, Int16? identificador)
        {

           

            try
            {
                ViewBag.mensaje = mensaje;
                ViewBag.identificador = identificador;

                entUsuario u = (entUsuario)Session["usuario"];
                if (u != null)
                {
                    Int32 sucursalId = u.Sucursal.Suc_Id;
                    Int32 UsuarioId = u.Usu_Id;
                    List<entUsuario> lista = negUsuario.Instancia.ListaUsuarios(UsuarioId, sucursalId);
                    return View(lista);
                }
                else
                {
                    return RedirectToAction("Index", "Inicio");
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }


        public ActionResult DetalleUsuario(Int32 UsuarioId)
        {
            try
            {
                entUsuario u = (entUsuario)Session["usuario"];
                if (u != null)
                {
                    Int32 UsuaIDSuper = u.Usu_Id;
                    entUsuario us = negUsuario.Instancia.DetalleUsuario(UsuarioId, UsuaIDSuper);
                    return View(us);
                }
                else
                {
                    return RedirectToAction("Index", "Inicio");
                }
            }
            catch (ApplicationException x)
            {
                ViewBag.mensaje = x.Message;
                return RedirectToAction("ListaUsuarios", "Comun", new { mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {

                return RedirectToAction("ListaUsuarios", "Comun", new { mensaje = e.Message, identificador = 2 });
            }
           
        }


        public ActionResult UpdUsuario(Int16 UsuarioId, String mensaje, Int16? identificador) {

            try
            {
                ViewBag.mensaje = mensaje;
                ViewBag.identificador = identificador;
                entUsuario u = (entUsuario)Session["usuario"];
                if (u != null)
                {
                    List<entTipoUsuario> t = null;

                    if (u.TipoUsuario.TipUsu_Id == 3)
                    {
                        t = negTipoUsuario.Instancia.ListaTipoUsuarioxId(8);
                    }
                    if (u.TipoUsuario.TipUsu_Id == 6)
                    {
                        t = negTipoUsuario.Instancia.ListaTipoUsuarioxId(9);
                    }
                    var lsTipoUsuario = new SelectList(t, "TipUsu_Id", "TipUsu_Nombre");
                    ViewBag.ListaTipoUsuario = lsTipoUsuario;

                    List<entSucursal> s = negSucursal.Instancia.ListaSucursalxId(u.Sucursal.Suc_Id);
                    var lsSucursal = new SelectList(s, "Suc_Id", "Suc_Nombre");
                    ViewBag.ListaSucursal = lsSucursal;

                    entUsuario us = negUsuario.Instancia.DetalleUsuario(UsuarioId, u.Usu_Id);

                    return View(us);
                }
                else
                {
                    return RedirectToAction("Index", "Inicio");
                }
            }
            catch (ApplicationException x)
            {
                ViewBag.mensaje = x.Message;
                return RedirectToAction("ListaUsuarios", "Comun", new { mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {

                return RedirectToAction("ListaUsuarios", "Comun", new { mensaje = e.Message, identificador = 2 });
            }
        }


    }
}
