using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidades;
using CapaNegocio;

namespace CapaPresentacion.Controllers.Intranet
{
    public class SupervisorCampoController : Controller
    {
        //
        // GET: /SupervisorCampo/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PrincipalSupervisorCampo()
        {
            return View();
        }
        
    }
}
