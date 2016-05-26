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

        public ActionResult ActualizarPass(FormCollection valida){
            try
            {
                Int32 iduser = 0;
                String newpass = "", passactual = "";
                if (Session["usuario"] != null)
                {
                    entUsuario u = (entUsuario)Session["usuario"];
                    iduser = u.Usu_Id;
                    passactual = valida["passactual"];
                    newpass = valida["pass2"].ToString();
                }
                int i = negUsuario.Instancia.ActualizaPass(iduser, newpass, passactual);
                return RedirectToAction("Ajustes", new { mensaje = "Su contraseña se actualizao de manera correcta" });

            }
            catch (ArithmeticException ex){
                return RedirectToAction("Ajustes", new { mensaje = ex.Message,tiperror=2});
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Error", new { mensaje = e.Message });
            }
        }

        public ActionResult Ajustes(String mensaje,int? tiperror)
        {
            if (mensaje != null)
            {
                ViewBag.terror = tiperror;
                ViewBag.message = mensaje;
            }
            return View();
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
            catch (Exception e)
            {
                return RedirectToAction("ListaUsuarios", "Comun", new { mensaje = e.Message, identificador = 2 });
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
                entUsuario us = (entUsuario)Session["usuario"];
                if (us != null)
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
                    entUsuario user = (entUsuario)Session["usuario"];
                    String userRegistro = user.Persona.NombreCompleto;

                    u.Usu_UsuarioRegistro = userRegistro;
                    ///////////////////////////////////////////
                    int i = negUsuario.Instancia.InsUpdUsuario(u, 1);
                    if (i > 0)
                    {
                        if (archivo != null && archivo.ContentLength > 0)
                        {
                            var namearchivo = Path.GetFileName(archivo.FileName);
                            var ruta = Path.Combine(Server.MapPath("~/assets/img/Fotos"), namearchivo);
                            archivo.SaveAs(ruta);
                        }
                        return RedirectToAction("ListaUsuarios", new { mensaje = "Se Inserto Satisfactoriamente", identificador = 3 });
                    }
                    else
                    {
                        return RedirectToAction("InsUsuario", "Comun", new { mensaje = "Problemas al Registrar", identificador = 2 });
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Inicio");
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

        public ActionResult UpdUsuario(Int16 UsuarioId, String mensaje, Int16? identificador)
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

        [HttpPost]
        public ActionResult UpdUsuario(entUsuario u, HttpPostedFileBase archivo, FormCollection form)
        {
            try
            {
                entUsuario us = (entUsuario)Session["usuario"];
                if (us != null)
                {
                    String fecNac = form["txtFecNac"];
                    String fecHasta = form["txtFecHasta"];
                    String foto = form["txtFoto"];
                    u.Persona.Per_FechaNacimiento = Convert.ToDateTime(fecNac);
                    u.Usu_FechaHasta = Convert.ToDateTime(fecHasta);

                    if (archivo != null && archivo.ContentLength > 0)
                    {
                        u.Persona.Per_Foto = Path.GetFileName(archivo.FileName);
                    }
                    else
                    {
                        u.Persona.Per_Foto = foto;
                    }
                    //para capturar el usuario en sesion////////////
                    entUsuario user = (entUsuario)Session["usuario"];
                    String userModificacion = user.Persona.NombreCompleto;

                    u.Usu_UsuarioModificacion = userModificacion;
                    ///////////////////////////////////////////
                    int i = negUsuario.Instancia.InsUpdUsuario(u, 2);

                    if (i > 0)
                    {
                        if (archivo != null && archivo.ContentLength > 0)
                        {
                            var namearchivo = Path.GetFileName(archivo.FileName);
                            var ruta = Path.Combine(Server.MapPath("~/assets/img/Fotos"), namearchivo);
                            archivo.SaveAs(ruta);
                        }
                        return RedirectToAction("ListaUsuarios", new { mensaje = "Se Edito Satisfactoriamente", identificador = 3 });
                    }
                    else
                    {
                        return RedirectToAction("UpdUsuario", "Comun", new { UsuarioId = u.Usu_Id, mensaje = "Problemas al Editar", identificador = 2 });
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Inicio");
                }
            }
            catch (ApplicationException x)
            {
                ViewBag.mensaje = x.Message;
                return RedirectToAction("UpdUsuario", "Comun", new { UsuarioId = u.Usu_Id, mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {
                return RedirectToAction("UpdUsuario", "Comun", new { UsuarioId = u.Usu_Id, mensaje = e.Message, identificador = 2 });
            }
        }

        public ActionResult DelUsuario(FormCollection form)
        {
            try
            {
                entUsuario us = (entUsuario)Session["usuario"];
                if (us != null)
                {
                    String idUsuDel = form["txtUserId"];
                    Int16 idUser = Convert.ToInt16(idUsuDel);
                    entUsuario u = new entUsuario();
                    u.Usu_Id = idUser;
                    //para capturar el usuario en sesion////////////
                    entUsuario user = (entUsuario)Session["usuario"];
                    String userModificacion = user.Persona.NombreCompleto;

                    u.Usu_UsuarioModificacion = userModificacion;
                    ///////////////////////////////////////////
                    int i = negUsuario.Instancia.DelBloActUsurio(u, 3);
                    return RedirectToAction("ListaUsuarios", new { mensaje = "Se Elimino Satisfactoriamente", identificador = 3 });
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

        public ActionResult BloUsuario(FormCollection form)
        {
            try
            {
                entUsuario us = (entUsuario)Session["usuario"];
                if (us != null)
                {
                    String idUsuBlo = form["txtUserId"];
                    Int16 idUser = Convert.ToInt16(idUsuBlo);
                    entUsuario u = new entUsuario();
                    u.Usu_Id = idUser;
                    //para capturar el usuario en sesion////////////
                    entUsuario user = (entUsuario)Session["usuario"];
                    String userModificacion = user.Persona.NombreCompleto;

                    u.Usu_UsuarioModificacion = userModificacion;
                    ///////////////////////////////////////////
                    int i = negUsuario.Instancia.DelBloActUsurio(u, 4);
                    return RedirectToAction("ListaUsuarios");
                    //return RedirectToAction("ListaUsuarios", new { mensaje = "Se Elimino Satisfactoriamente", identificador = 3 });
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
        public ActionResult ActUsuario(FormCollection form)
        {
            try
            {
                entUsuario us = (entUsuario)Session["usuario"];
                if (us != null)
                {
                    String idUsuAct = form["txtUserId"];
                    Int16 idUser = Convert.ToInt16(idUsuAct);
                    entUsuario u = new entUsuario();
                    u.Usu_Id = idUser;
                    //para capturar el usuario en sesion////////////
                    entUsuario user = (entUsuario)Session["usuario"];
                    String userModificacion = user.Persona.NombreCompleto;

                    u.Usu_UsuarioModificacion = userModificacion;
                    ///////////////////////////////////////////
                    int i = negUsuario.Instancia.DelBloActUsurio(u, 5);
                    return RedirectToAction("ListaUsuarios");
                    //return RedirectToAction("ListaUsuarios", new { mensaje = "Se Elimino Satisfactoriamente", identificador = 3 });
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
