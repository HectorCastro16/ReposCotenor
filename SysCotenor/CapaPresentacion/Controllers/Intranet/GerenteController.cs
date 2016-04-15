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

        //public ActionResult ListaUsuarios(String mensaje)
        //{

        //    ViewBag.mensaje = mensaje;
        //    List<entUsuario> Lista = negUsuario.Instancia.ListaUsuarios();
        //    return View(Lista);
        //}
    }
}
