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
                String Usuario = form["txtUsuario"];
                String Password = form["txtPassword"];
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
                else if (u.TipoUsuario.TipUsu_Nombre.Equals("Supervisor"))
                {
                    return RedirectToAction("PrincipalSupervisor", "Supervisor", u);
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

    }
}
