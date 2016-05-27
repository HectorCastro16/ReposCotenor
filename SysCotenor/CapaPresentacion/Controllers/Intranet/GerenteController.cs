using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidades;
using CapaNegocio;
using System.IO;

namespace CapaPresentacion.Controllers.Intranet
{
    public class GerenteController : Controller
    {
        //
        // GET: /Gerente/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PrincipalGerente()
        {
            return View();
        }

        public ActionResult ListaSupervisores(String mensaje, Int16? identificador)
        {
            try
            {
                ViewBag.mensaje = mensaje;
                ViewBag.identificador = identificador;
                entUsuario u = (entUsuario)Session["usuario"];
                if (u != null)
                {
                    List<entUsuario> Lista = negGerente.Instancia.ListaSupervisores();
                    return View(Lista);
                }
                else
                {
                    return RedirectToAction("Index", "Inicio");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("ListaSupervisores", "Gerente", new { mensaje = e.Message, identificador = 2 });
            }           
        }

        public ActionResult DetalleSupervisor(Int32 SuperId) {

            try
            {
                entUsuario u = (entUsuario)Session["usuario"];
                if (u != null)
                {
                    entUsuario us = negGerente.Instancia.DetalleSupervisor(SuperId);
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
                return RedirectToAction("ListaSupervisores", "Gerente", new { mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {
                return RedirectToAction("ListaSupervisores", "Gerente", new { mensaje = e.Message, identificador = 2 });
            }        
        }

        public ActionResult InsSupervisor(String mensaje, Int16? identificador)
        {
            try
            {
                ViewBag.mensaje = mensaje;
                ViewBag.identificador = identificador;
                entUsuario u = (entUsuario)Session["usuario"];
                if (u != null)
                {
                    List<entTipoUsuario> t =  t = negTipoUsuario.Instancia.ListaTipoUsuarioSupervisores();
                    var lsTipoUsuario = new SelectList(t, "TipUsu_Id", "TipUsu_Nombre");
                    ViewBag.ListaTipoUsuario = lsTipoUsuario;

                    List<entSucursal> s = negSucursal.Instancia.ListaSucursal();
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
                return RedirectToAction("ListaSupervisores", "Gerente", new { mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {

                return RedirectToAction("ListaSupervisores", "Gerente", new { mensaje = e.Message, identificador = 2 });
            }
        }

        [HttpPost]
        public ActionResult InsSupervisor(entUsuario u, HttpPostedFileBase archivo, FormCollection form)
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
                    int i = negUsuario.Instancia.InsUpdSupervisor(u, 1);
                    if (i > 0)
                    {
                        if (archivo != null && archivo.ContentLength > 0)
                        {
                            var namearchivo = Path.GetFileName(archivo.FileName);
                            var ruta = Path.Combine(Server.MapPath("~/assets/img/Fotos"), namearchivo);
                            archivo.SaveAs(ruta);
                        }
                        return RedirectToAction("ListaSupervisores", new { mensaje = "Se Inserto Satisfactoriamente", identificador = 3 });
                    }
                    else
                    {
                        return RedirectToAction("InsSupervisor", "Gerente", new { mensaje = "Problemas al Registrar", identificador = 2 });
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
                return RedirectToAction("InsSupervisor", "Gerente", new { mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {
                return RedirectToAction("InsSupervisor", "Gerente", new { mensaje = e.Message, identificador = 2 });
            }
        }
        public ActionResult UpdSupervisor(Int16 SuperId, String mensaje, Int16? identificador)
        {
            try
            {
                ViewBag.mensaje = mensaje;
                ViewBag.identificador = identificador;
                entUsuario u = (entUsuario)Session["usuario"];
                if (u != null)
                {
                    List<entTipoUsuario> t  = negTipoUsuario.Instancia.ListaTipoUsuarioSupervisores();
                    var lsTipoUsuario = new SelectList(t, "TipUsu_Id", "TipUsu_Nombre");
                    ViewBag.ListaTipoUsuario = lsTipoUsuario;

                    List<entSucursal> s = negSucursal.Instancia.ListaSucursal();
                    var lsSucursal = new SelectList(s, "Suc_Id", "Suc_Nombre");
                    ViewBag.ListaSucursal = lsSucursal;
                    entUsuario us = negGerente.Instancia.DetalleSupervisor(SuperId);
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
                return RedirectToAction("ListaSupervisores", "Gerente", new { mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {
                return RedirectToAction("ListaSupervisores", "Gerente", new { mensaje = e.Message, identificador = 2 });
            }
        }


        [HttpPost]
        public ActionResult UpdSupervisor(entUsuario u, HttpPostedFileBase archivo, FormCollection form)
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
                    int i = negUsuario.Instancia.InsUpdSupervisor(u, 2);

                    if (i > 0)
                    {
                        if (archivo != null && archivo.ContentLength > 0)
                        {
                            var namearchivo = Path.GetFileName(archivo.FileName);
                            var ruta = Path.Combine(Server.MapPath("~/assets/img/Fotos"), namearchivo);
                            archivo.SaveAs(ruta);
                        }
                        return RedirectToAction("ListaSupervisores", new { mensaje = "Se Edito Satisfactoriamente", identificador = 3 });
                    }
                    else
                    {
                        return RedirectToAction("UpdSupervisor", "Gerente", new { UsuarioId = u.Usu_Id, mensaje = "Problemas al Editar", identificador = 2 });
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
                return RedirectToAction("UpdSupervisor", "Gerente", new { UsuarioId = u.Usu_Id, mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {
                return RedirectToAction("UpdSupervisor", "Gerente", new { UsuarioId = u.Usu_Id, mensaje = e.Message, identificador = 2 });
            }
        }
        public ActionResult DelSupervisor(FormCollection form)
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
                    int i = negUsuario.Instancia.DelBloActSuper(u, 3);
                    return RedirectToAction("ListaSupervisores", new { mensaje = "Se Elimino Satisfactoriamente", identificador = 3 });
                }
                else
                {
                    return RedirectToAction("Index", "Inicio");
                }
            }
            catch (ApplicationException x)
            {
                ViewBag.mensaje = x.Message;
                return RedirectToAction("ListaSupervisores", "Gerente", new { mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {
                return RedirectToAction("ListaSupervisores", "Gerente", new { mensaje = e.Message, identificador = 2 });
            }
        }
        public ActionResult BloSupervisor(FormCollection form)
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
                    int i = negUsuario.Instancia.DelBloActSuper(u, 4);
                    return RedirectToAction("ListaSupervisores");
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
                return RedirectToAction("ListaSupervisores", "Gerente", new { mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {
                return RedirectToAction("ListaSupervisores", "Gerente", new { mensaje = e.Message, identificador = 2 });
            }
        }
        public ActionResult ActSupervisor(FormCollection form)
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
                    int i = negUsuario.Instancia.DelBloActSuper(u, 5);
                    return RedirectToAction("ListaSupervisores");
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
                return RedirectToAction("ListaSupervisores", "Gerente", new { mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {
                return RedirectToAction("ListaSupervisores", "Gerente", new { mensaje = e.Message, identificador = 2 });
            }
        }



    }

}
