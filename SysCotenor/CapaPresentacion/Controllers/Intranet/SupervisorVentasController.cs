using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidades;
using CapaNegocio;
namespace CapaPresentacion.Controllers.Intranet
{
    public class SupervisorVentasController : Controller
    {
        //
        // GET: /SupervisorVentas/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PrincipalSupervisorVentas()
        {
            return View();
        }

        public ActionResult GuardaActuPedido(FormCollection frm){
            try
            {
                int idpedido = Convert.ToInt32(frm["txtidPedido"]);
                int idestado = Convert.ToInt32(frm["selecestado"]);
                String desc = frm["desc"];
                int result = negPedido.Instancia.ActualizaEstadoPedido(idpedido, idestado, desc);

                return RedirectToAction("ActualizarEstadosVentas", new { mensaje = "Se actualizo corretamente" });
            }
            catch (Exception e)
            {

                return RedirectToAction("ActualizarEstadosVentas", new { mensaje = e.Message });
            }
        }

        public ActionResult ActualizarEstadosVentas(Int32? identificador, String mensaje){
            try
            {
                ViewBag.mensaje = mensaje;
                entUsuario u = new entUsuario();
                if (Session["usuario"] != null){
                    u = (entUsuario)Session["usuario"];
                }               
                ViewBag.estados = negPedido.Instancia.ListEstados();
                ViewBag.usuarios = negUsuario.Instancia.ListaUsuarios(u.Usu_Id, u.Sucursal.Suc_Id);
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
                //ViewBag.mensaje = ex.Message;
                //return View();
            }

        }


        [HttpPost]
        public ActionResult ActualizarEstadosVentas(FormCollection frm) {
            try
            { entUsuario u = new entUsuario();
                if (Session["usuario"] != null) { u = (entUsuario)Session["usuario"]; }
                int idestado = 0,idasesor=0;
                String desde = "", hasta = "";
                idestado = Convert.ToInt32(frm["select1"]);
                idasesor = Convert.ToInt32(frm["select2"]);
                desde = frm["txtFehcaDesde"];
                hasta = frm["txtFecHasta"];
                if (Session["ventas"] != null) Session.Remove("ventas"); 
                Session["ventas"] = negPedido.Instancia.ListaComisiones(idasesor, desde, hasta, idestado);

                return RedirectToAction("ActualizarEstadosVentas");
            }
            catch (Exception e)
            {
                return RedirectToAction("ActualizarEstadosVentas", new { mensaje = e.Message ,identificador=1});
            }
        }


    }
}
