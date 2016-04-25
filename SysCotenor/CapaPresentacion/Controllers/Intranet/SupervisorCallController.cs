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

        //public ActionResult ListaUsuarioEstados()
        //{
        //    try
        //    {
        //        String codsupervisor = "";
        //        List<entUsuario> lista = null;
        //        if (Session["usuario"] != null)
        //        {
        //            entUsuario u = (entUsuario)Session["usuario"];
        //            codsupervisor = u.Usu_Id;
        //        }
        //        lista = negUsuario.Instancia.ListUsuariosEstado(codsupervisor);
        //        return View(lista);
        //    }
        //    catch (ApplicationException ae) {
        //        ViewBag.mensaje = ae.Message;
        //        List<entUsuario> lista = new List<entUsuario>();
        //        return View(lista);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        public ActionResult ListaUsuarioAsesorCall()
        {
            entUsuario u = (entUsuario)Session["usuario"];
            if (u != null)
            {
                Int32 sucursalId = u.Sucursal.Suc_Id;
                Int32 UsuarioId = u.Usu_Id;
                List<entUsuario> lista = negUsuario.Instancia.ListaUsuarios(UsuarioId, 8, sucursalId);
                return View(lista);
            }
            else
            {
                return RedirectToAction("Index", "Inicio");
            }
        }

        public ActionResult DetalleUsuario(Int32 UsuarioId)
        {
            Int32 UsuaID = UsuarioId;
            entUsuario u = negUsuario.Instancia.DetalleUsuario(UsuaID, 8);
            return View(u);
        }
    }
}
