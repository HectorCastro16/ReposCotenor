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

        public ActionResult VerificarAcceso(FormCollection form)
        {
            try
            {
                String Usuario = form["txtUsuario"];
                String Password = form["txtPassword"];
                entUsuario u = negUsuario.Instancia.VerificarAccesoIntranet(Usuario, Password);
                Session["usuario"] = u; //agregando el objeto c en el atributo cliente de la sesion

                if (u.TipoUsuario.NombreTipo.Equals("JefeLogistica"))
                {
                    return RedirectToAction("MenuPrincipalLogistica", "IntranetJefeLogistica", u);
                }
                else if (u.TipoUsuario.NombreTipo.Equals("JefeAlmacen"))
                {
                    return RedirectToAction("MenuPrincipalAlmacen", "IntranetJefeAlmacen", u);
                }
                else
                {
                    return RedirectToAction("MenuPrincipalDespachador", "IntranetDespachador", u);
                }


            }
            catch (ApplicationException x)
            {
                ViewBag.mensaje = x.Message;
                return RedirectToAction("Index", "Inicio", new { mensaje = x.Message });
            }
            catch (Exception e)
            {

                return RedirectToAction("Index", "Inicio", new { mensaje = e.Message });
            }
        }

    }
}
