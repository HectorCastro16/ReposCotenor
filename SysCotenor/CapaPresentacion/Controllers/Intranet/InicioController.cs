using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.NetworkInformation;
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
           
        }

        public ActionResult Login(String mensaje, Int16? identificador)
        {
           
            ViewBag.mensaje = mensaje;
            ViewBag.identificador = identificador;
            try
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


            }
            catch (Exception)
            {
                throw;
            }
           
        }
       public ActionResult VerificarAcceso(FormCollection form)
        {
            try  {
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
                        /*    Aca va el registro del Usuario en la Tabla UsuarioSecurity*/
                        //Capturo IPV4 de Conexion de Area Local
                        IPHostEntry host;
                        string localIP = "";
                        host = Dns.GetHostEntry(Dns.GetHostName());
                        foreach (IPAddress ip in host.AddressList)
                        {
                            if (ip.AddressFamily.ToString() == "InterNetwork")
                            {
                                localIP = ip.ToString();
                            }
                        }
                        negUsuario.Instancia.RegUsuarioSecurity(u, 1, localIP);

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
                ViewBag.mensaje = e.Message;
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
