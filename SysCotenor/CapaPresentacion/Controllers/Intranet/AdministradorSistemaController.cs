using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers.Intranet
{
    public class AdministradorSistemaController : Controller
    {
        //
        // GET: /AdministradorSistema/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult PrincipalAdministradorSistema()
        {
            return View();
        }
    }
}
