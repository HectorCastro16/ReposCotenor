using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers.Intranet
{
    public class ComunController : Controller
    {
        //
        // GET: /Comun/

        public ActionResult Index()
        {
            return View();
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

                List<entSucursal> s = negSucursal.Instancia.ListaSucursalxId(u.Sucursal.Suc_Id);
                var lsSucursal = new SelectList(s, "Suc_Id", "Suc_Nombre");
                ViewBag.ListaSucursal = lsSucursal;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpPost]
        public ActionResult InsUsuario(entUsuario u, HttpPostedFileBase archivo)
        {

            return View();

        }





        public ActionResult ListaUsuarios()
        {
            entUsuario u = (entUsuario)Session["usuario"];
            if (u != null)
            {
                Int32 sucursalId = u.Sucursal.Suc_Id;
                Int32 UsuarioId = u.Usu_Id;
                List<entUsuario> lista = negUsuario.Instancia.ListaUsuarios(UsuarioId, sucursalId);
                return View(lista);
            }
            else
            {
                return RedirectToAction("Index", "Inicio");
            }
        }


        public ActionResult DetalleUsuario(Int32 UsuarioId)
        {
            entUsuario u = (entUsuario)Session["usuario"];
            if (u != null)
            {
                Int32 UsuaIDSuper = u.Usu_Id;
                entUsuario us = negUsuario.Instancia.DetalleUsuario(UsuarioId, UsuaIDSuper);
                return View(us);
            }
            else
            {
                return RedirectToAction("Index", "Inicio");
            }
        }


    }
}
