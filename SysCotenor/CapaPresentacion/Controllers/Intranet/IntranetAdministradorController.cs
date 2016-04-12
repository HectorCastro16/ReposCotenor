using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidades;
using CapaNegocio;

namespace CapaPresentacion.Controllers.Intranet
{
    public class IntranetAdministradorController : Controller
    {
        //
        // GET: /IntranetAdministrador/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PrincipalAdministrador(entUsuario u)
        {
            return View(u);
        }
    }
}
