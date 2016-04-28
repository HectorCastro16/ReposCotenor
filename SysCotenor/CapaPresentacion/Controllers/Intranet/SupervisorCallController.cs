using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidades;
using CapaNegocio;

namespace CapaPresentacion.Controllers.Intranet
{
    public class SupervisorCallController : Controller
    {
        //
        // GET: /SupervisorCall/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PrincipalSupervisorCall(entUsuario u)
        {
            return View(u);
        }

        
        public ActionResult InsUsuario()
        {

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
                
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Inicio");
            }
        }

    }
}
