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


        public ActionResult InsEditUsuario(Int32? UsuarioId)
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
                }

                // para sucursal -----------------

                List<entSucursal> Ls = null;
                Ls = negSucursal.Instancia.ListarSucursales(u.Sucursal.Suc_Id);
                var lstSucursal = new SelectList(Ls, "Suc_Id", "Suc_Nombre");
                ViewBag.ListSucursale = lstSucursal;
            if (UsuarioId == null){
                ViewBag.accion = 0;
                return View();
            } else if (UsuarioId != null) {
                Int32 id =Convert.ToInt32(UsuarioId);
                entUsuario us = negUsuario.Instancia.BuscarUsario(id);
                ViewBag.accion = 1;
                return View(us);
               
            }
            else
            {
                return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpPost]
        public ActionResult InsEditUsuario(FormCollection frm) {

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
