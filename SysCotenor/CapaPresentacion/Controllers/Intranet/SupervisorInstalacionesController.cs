using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidades;
using CapaNegocio;

namespace CapaPresentacion.Controllers.Intranet
{
    public class SupervisorInstalacionesController : Controller
    {
        //
        // GET: /SupervisorInstalaciones/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PrincipalSupervisorInstalaciones()
        {
            return View();
        }

        public ActionResult PedidosAsesoresCampo(Int32? identificador, String mensaje)
        {
            try
            {
                ViewBag.mensaje = mensaje;
                ViewBag.identificador = identificador;
                entUsuario u = (entUsuario)Session["usuario"];
                if (u != null)
                {
                    ViewBag.estados = negPedido.Instancia.ListEstados();
                    ViewBag.usuarios = negUsuario.Instancia.ListaUsuariosAC();
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Inicio");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("ListaPedidosAsesoresCampo", "SupervisorInstalaciones", new { mensaje = e.Message, identificador = 2 });
            }

           
        }
        
        public ActionResult ListaPedidosAsesoresCampo(Int32 IdAC,Int32 IdEs, String fdesde ,String fhasta)
        {
            try
            {                
                entUsuario u = (entUsuario)Session["usuario"];
                if (u != null)
                {
                    int idestado = 0, idasesor = 0;
                    String desde = "", hasta = "";
                    idestado = Convert.ToInt32(IdEs);
                    idasesor = Convert.ToInt32(IdAC);
                    desde = fdesde.ToString();
                    hasta = fhasta.ToString();
                    if (Session["ventasAC"] != null) Session.Remove("ventasAC");
                    Session["ventasAC"] = negPedido.Instancia.ListaComisiones(idasesor, desde, hasta, idestado);

                    return PartialView();
                }
                else
                {
                    return RedirectToAction("Index", "Inicio");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("ListaPedidosAsesoresCampo", "SupervisorInstalaciones", new { mensaje = e.Message, identificador = 2 });
            }


        }
    }
}
