using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidades;
using CapaNegocio;

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

    }

}
