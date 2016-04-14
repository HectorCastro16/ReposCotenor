﻿using System;
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
            return View();
        }

        public ActionResult Login(String mensaje)
        {
            ViewBag.mensaje = mensaje;
            return View();
        }


        public ActionResult VerificarAcceso(FormCollection form)
        {
            
           
            try
            {
                String Usuario = form["txtUsuario"].Replace(";", "").Replace("'", "").Replace("--","");
                String Password = form["txtPassword"].Replace(";", "").Replace("'", "").Replace("--", "");
                entUsuario u = negUsuario.Instancia.VerificarAccesoIntranet(Usuario, Password);
                Session["usuario"] = u;

                if (u.TipoUsuario.TipUsu_Nombre.Equals("Administrador"))
                {
                    return RedirectToAction("PrincipalAdministrador", "Administrador", u);
                }
                else if (u.TipoUsuario.TipUsu_Nombre.Equals("Gerente"))
                {
                    return RedirectToAction("PrincipalGerente", "Gerente", u);
                }
                else if (u.TipoUsuario.TipUsu_Nombre.Equals("Supervisor Call"))
                {
                    return RedirectToAction("PrincipalSupervisorCall", "SupervisorCall", u);
                }
                else if (u.TipoUsuario.TipUsu_Nombre.Equals("Supervisor Instalaciones"))
                {
                    return RedirectToAction("PrincipalSupervisorInstalaciones", "SupervisorInstalaciones", u);
                }
                else if (u.TipoUsuario.TipUsu_Nombre.Equals("Supervisor Ventas"))
                {
                    return RedirectToAction("PrincipalSupervisorVentas", "SupervisorVentas", u);
                }
                else if (u.TipoUsuario.TipUsu_Nombre.Equals("Supervisor Cable"))
                {
                    return RedirectToAction("PrincipalSupervisorCable", "SupervisorCable", u);
                }
                else
                {
                    return RedirectToAction("PrincipalAsesorVentas", "AsesorVentas", u);
                }
            }
            catch (ApplicationException x)
            {
                
                ViewBag.mensaje = x.Message;
                return RedirectToAction("Login", "Inicio", new { mensaje = x.Message });
            }
            catch (Exception e)
            {

                return RedirectToAction("Login", "Inicio", new { mensaje = e.Message });
            }
        }

        public ActionResult CerrarSession()
        {

            try
            {
                //Session.Abandon();
                Session["usuario"] = null;
                Session.Remove("usuario");
                //Session.RemoveAll();
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

            return RedirectToAction("Index", "Inicio");
        }

        public ActionResult Historia() {
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
    }
}
