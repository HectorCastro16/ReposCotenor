using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidades;
using CapaNegocio;
using System.Data;

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

        public ActionResult ExtraerDatosAsignar(String mensaje) {
            ViewBag.mensaje = mensaje;
            return View();
        }

        public ActionResult AsignarCliente() {
            return PartialView();
        }

        public ActionResult EliminarFila(String telef)
        {

            if(Session["asignacion"] ==null) CrearTablaSessionAsig();
              DataTable dt = (DataTable)Session["asignacion"];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow ror in dt.Rows)
                {
                    if (ror["telefono"].ToString() == telef || ror["telefono"].ToString()=="")
                    {
                        ror.Delete();
                        break;
                    }

                }
            }

            return RedirectToAction("ExtraerDatosAsignar");
        }
 
        public ActionResult ExtraerDatosAsignarSession(FormCollection form,String agregar,String guardar){
            if (agregar != null)
            {
                if (Session["asignacion"] == null) CrearTablaSessionAsig();
                DataTable dt = (DataTable)Session["asignacion"];
                if (form["telefono"] == "")
                {
                    String msje = "No se pudo agregar, ingrese un numero valido,ó /cel/dni";
                    return RedirectToAction("ExtraerDatosAsignar",new {mensaje=msje });
                    
                }
                else
                {

                    DataRow r = dt.NewRow();
                    r["telefono"] = form["telefono"];
                    r["cliente"] = form["cliente"];
                    r["f1"] = form["f1"];
                    r["f2"] = form["f2"];
                    r["f3"] = form["f3"];
                    r["sva"] = form["sva"];
                    r["iniciovigencia"] = form["iniciovigencia"];
                    dt.Rows.Add(r);
                }
            }
            return RedirectToAction("ExtraerDatosAsignar");
        }

        private void CrearTablaSessionAsig(){
            try{
                DataTable dt = new DataTable();
                dt.Columns.Add("telefono", Type.GetType("System.String"));
                dt.Columns.Add("cliente", Type.GetType("System.String"));
                dt.Columns.Add("f1", Type.GetType("System.String"));
                dt.Columns.Add("f2", Type.GetType("System.String"));
                dt.Columns.Add("f3", Type.GetType("System.String"));
                dt.Columns.Add("sva", Type.GetType("System.String"));
                dt.Columns.Add("iniciovigencia", Type.GetType("System.DateTime"));

                Session["asignacion"] = dt;

            }
            catch (Exception){
                throw;
            }

        }



        //public ActionResult InsUsuario()
        //{

        //    entUsuario u = (entUsuario)Session["usuario"];
        //    if (u != null)
        //    {
        //        List<entTipoUsuario> t = null;
        //        if (u.TipoUsuario.TipUsu_Id == 3)
        //        {
        //            t = negTipoUsuario.Instancia.ListaTipoUsuarioxId(8);
        //        }
        //        if (u.TipoUsuario.TipUsu_Id == 6)
        //        {
        //            t = negTipoUsuario.Instancia.ListaTipoUsuarioxId(9);
        //        }
        //        var lsTipoUsuario = new SelectList(t, "TipUsu_Id", "TipUsu_Nombre");
        //        ViewBag.ListaTipoUsuario = lsTipoUsuario;

        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Inicio");
        //    }
        //}

    }
}
