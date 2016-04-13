using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidades;
using CapaNegocio;

namespace CapaPresentacion.Controllers.Intranet
{
    public class AsesorVentasController : Controller
    {
        //
        // GET: /AsesorVentas/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PrincipalAsesorVentas(entUsuario u)
        {
            return View(u);
        }
    }
}
