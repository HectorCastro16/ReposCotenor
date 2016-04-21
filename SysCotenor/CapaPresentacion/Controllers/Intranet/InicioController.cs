using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidades;
using CapaNegocio;

namespace CapaPresentacion.Controllers
{
    public class InicioController : Controller
    {
        //
        // GET: /Inicio/

        public ActionResult Index()
        {
            if (Session["usuario"] != null)
            {
                entUsuario u = (entUsuario)Session["usuario"];
                String TipoUsuario = u.TipoUsuario.TipUsu_Nombre.Replace(" ", "").ToString();
                return RedirectToAction("Principal" + TipoUsuario, TipoUsuario);
            }
            else
            {
                return View();
            }
            // hola mundo 
        }

        public ActionResult Login(String mensaje, Int16? identificador)
        {
            int limite = 0;
            ViewBag.mensaje = mensaje;
            ViewBag.identificador = identificador;
            try
            {
                if (identificador == 1){
                        if (Session["intentos"] != null)
                        {
                            limite = (int)Session["intentos"] + 1;
                            Session["intentos"] = limite;
                        }
                        else Session["intentos"] = 1;
                        limite = (int)Session["intentos"];
                        if (limite >= 5)
                        {
                            ViewBag.mensaje = "Se ha bloqueado por cantidad de intentos + o = 3";
                        DateTime actual = DateTime.Now;
                        DateTime finbloqueo = actual.AddMinutes(1);
                        Session["finbloqueo"] = finbloqueo;
                    }
                    
                }
            }
            catch (Exception)
            {
                throw;
            }


            if (Session["usuario"] != null)
            {
                entUsuario u = (entUsuario)Session["usuario"];
                String TipoUsuario = u.TipoUsuario.TipUsu_Nombre.Replace(" ", "").ToString();
                return RedirectToAction("Principal" + TipoUsuario, TipoUsuario);
            }
            else
            {
                return View();
            }

        }


        public ActionResult VerificarAcceso(FormCollection form)
        {
            try
            {
                String Usuario = form["txtUsuario"].Replace(";", "").Replace("'", "").Replace("--", "");
                String Password = form["txtPassword"].Replace(";", "").Replace("'", "").Replace("--", "");
                if (Session["usuario"] != null)
                {
                    entUsuario us = (entUsuario)Session["usuario"];
                    String login = us.Usu_Login;
                    if (Usuario == login)
                    {
                        String TipoUsuario = us.TipoUsuario.TipUsu_Nombre.Replace(" ", "").ToString();
                        return RedirectToAction("Principal" + TipoUsuario, TipoUsuario);
                    }
                    else
                    {
                        return RedirectToAction("ConfirmLogin", "Inicio");
                    }
                }
                else
                {

                    entUsuario u = negUsuario.Instancia.VerificarAccesoIntranet(Usuario, Password);
                    Session["usuario"] = u;

                    if (u.TipoUsuario.TipUsu_Nombre != "" && u.TipoUsuario.TipUsu_Nombre != null)
                    {
                        String TipoUsuario = u.TipoUsuario.TipUsu_Nombre.Replace(" ", "").ToString();
                        if (Session["intentos"] != null){
                            Session.Remove("intentos");
                        }
                        return RedirectToAction("Principal" + TipoUsuario, TipoUsuario);  
                    }
                    else
                    {
                        return RedirectToAction("Login", "Inicio", new { mensaje = "Error = Tipo Usuario" });
                    }

                }

            }
            catch (ApplicationException x)
            {

                ViewBag.mensaje = x.Message;
                return RedirectToAction("Login", "Inicio", new { mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {

                return RedirectToAction("Login", "Inicio", new { mensaje = e.Message, identificador = 2 });
            }
        }

        public ActionResult CerrarSession()
        {

            try
            {
                //Session.Abandon();
                Session["usuario"] = null;
                Session.Remove("usuario");
                if (Session["intentos"] != null){
                    Session.Remove("intentos");
                }
                //Session.RemoveAll();
                return RedirectToAction("Index", "Inicio");
            }
            catch (ApplicationException x)
            {
                ViewBag.mensaje = x.Message;
                return RedirectToAction("Error", "Error");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Error");
            }


        }

        public ActionResult Historia()
        {
            return View();
        }
        public ActionResult Mision()
        {
            return View();
        }
        public ActionResult Vision()
        {
            return View();
        }

        public ActionResult ConfirmLogin()
        {

            return View();
        }
    }
}
