using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidades;
using CapaNegocio;

namespace CapaPresentacion.Controllers.Intranet
{
    public class SupervisorController : Controller
    {
        //
        // GET: /Supervisor/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PrincipalSupervisor(entUsuario u)
        {
            return View(u);
        }

    }
}
