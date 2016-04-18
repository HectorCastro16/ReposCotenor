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

        public ActionResult Login(String mensaje,Int16? identificador)
        {
            int limite = 0;
            ViewBag.mensaje = mensaje;
            ViewBag.identificador = identificador;
            try { 
                  if (identificador == 1){
                    if (Session["intentos"] != null) {
                        limite = (int)Session["intentos"] + 1;
                        Session["intentos"] = limite;
                    }else  Session["intentos"] = 1;
                    limite = (int)Session["intentos"];
                    if (limite == 3) {
                        ViewBag.mensaje = "Se ha bloqueado por cantidad de intentos + o = 3";
                        ViewBag.lim = limite;
                    }
                }
            }
            catch (Exception) {
                throw;
            }
            return View();
        }


        public ActionResult VerificarAcceso(FormCollection form)
        {
            try
            {
                String Usuario = form["txtUsuario"].Replace(";", "").Replace("'", "").Replace("--", "");
                String Password = form["txtPassword"].Replace(";", "").Replace("'", "").Replace("--", "");
                entUsuario u = negUsuario.Instancia.VerificarAccesoIntranet(Usuario, Password);
                Session["usuario"] = u;

                if (u.TipoUsuario.TipUsu_Nombre.Equals("Administrador"))
                {
                    return RedirectToAction("PrincipalAdministrador", "Administrador");
                }
                if (u.TipoUsuario.TipUsu_Nombre.Equals("Gerente"))
                {
                    return RedirectToAction("PrincipalGerente", "Gerente");
                }
                if (u.TipoUsuario.TipUsu_Nombre.Equals("Supervisor Call"))
                {
                    return RedirectToAction("PrincipalSupervisorCall", "SupervisorCall");
                }
                if (u.TipoUsuario.TipUsu_Nombre.Equals("Supervisor Ventas"))
                {
                    return RedirectToAction("PrincipalSupervisorVentas", "SupervisorVentas");
                }
                if (u.TipoUsuario.TipUsu_Nombre.Equals("Supervisor Instalaciones"))
                {
                    return RedirectToAction("PrincipalSupervisorInstalaciones", "SupervisorInstalaciones");
                }
                if (u.TipoUsuario.TipUsu_Nombre.Equals("Supervisor Campo"))
                {
                    return RedirectToAction("PrincipalSupervisorCampo", "SupervisorCampo");
                }
                if (u.TipoUsuario.TipUsu_Nombre.Equals("Supervisor Adsl"))
                {
                    return RedirectToAction("PrincipalSupervisorAdsl", "SupervisorAdsl");
                }
                if (u.TipoUsuario.TipUsu_Nombre.Equals("Asesor Ventas Call"))
                {
                    return RedirectToAction("PrincipalAsesorVentasCall", "AsesorVentasCall");
                }
                if (u.TipoUsuario.TipUsu_Nombre.Equals("Asesor Ventas Campo"))
                {
                    return RedirectToAction("PrincipalAsesorVentasCampo", "AsesorVentasCampo");
                }

                return RedirectToAction("Login", "Inicio", new { mensaje = "Error = Tipo Usuario" });
            }
            catch (ApplicationException x)
            {

                ViewBag.mensaje = x.Message;
                return RedirectToAction("Login", "Inicio", new { mensaje = x.Message, identificador=1 });
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
    }
}
