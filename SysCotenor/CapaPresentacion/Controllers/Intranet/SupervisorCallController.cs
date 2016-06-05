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

        public ActionResult lstUsuariosEstadoAsignacionLlamadas()
        {
            entUsuario u = (entUsuario)Session["usuario"];
            if (u != null)
            {
                Int32 sucursalId = u.Sucursal.Suc_Id;
                Int32 UsuarioId = u.Usu_Id;
                List<entUsuario> lista = negUsuario.Instancia.ListaUsuariosCall(UsuarioId, sucursalId);
                RemoverSessiones();
                return View(lista);
            } else
            {
                return RedirectToAction("Index", "Inicio");
            }
        }

        public ActionResult ExtraerDatosAsignar(String mensaje, Int32? iduser, String usuario) {

            try
            {
                ViewBag.mensaje = mensaje;
                if (iduser != null && Session["asignacion"] == null)
                {
                    CrearTablaSessionAsig();
                    DataTable dt = new DataTable();
                    dt = (DataTable)Session["asignacion"];
                    List<entAsigncionLlamadas> Lista = negAsLlamadas.Instancia.ListaLamadasAsig(Convert.ToInt32(iduser));
                    if (Lista.Count > 0)
                    {
                        for (int i = 0; i < Lista.Count; i++)
                        {
                            DataRow r = dt.NewRow();
                            // tipoedicion 0 = viene de la BD , 1 agregado reciente mente, 2 registro de la BD editado.
                            r["Asi_id"] = Lista[i].Asi_Id;
                            r["Asi_estado"] = Lista[i].Asi_Estado;
                            r["telefono"] = Lista[i].Asi_NumTelf;
                            r["cliente"] = Lista[i].Cliente;
                            r["f1"] = Lista[i].Asi_F1;
                            r["f2"] = Lista[i].Asi_F2;
                            r["f3"] = Lista[i].Asi_F3;
                            r["sva"] = Lista[i].Asi_SVA;
                            r["iniciovigencia"] = Convert.ToDateTime(Lista[i].Asi_FechInicioCliente).ToShortDateString();
                            r["Estadooedicion"] = 0;
                            r["estadoAtencion"] = Lista[i].Asi_Estado;
                            dt.Rows.Add(r);
                        }
                    }
                    Session["id"] = iduser;
                    Session["user"] = usuario;
                }
            }
            catch (Exception e){
                return RedirectToAction("Error", "Error", new { mensaje = e.Message });
            }
                return View();
            }
        
        public ActionResult BuscaFilaSession(String telef){
            if (Session["asignacion"] != null){
                DataTable dt = (DataTable)Session["asignacion"];
                foreach (DataRow dr in dt.Rows){
                    if (dr["telefono"].ToString() == telef){
                        entAsigncionLlamadas ac = new entAsigncionLlamadas();
                        ac.Asi_NumTelf= dr["telefono"].ToString();
                        ac.Cliente = dr["cliente"].ToString();
                        ac.Asi_F1 = Convert.ToDouble(dr["f1"]);
                        ac.Asi_F2 = Convert.ToDouble(dr["f2"]);
                        ac.Asi_F3 = Convert.ToDouble(dr["f3"]);
                        ac.Asi_SVA = dr["sva"].ToString();
                        ac.Asi_FechInicioCliente = dr["iniciovigencia"].ToString();
                        Session["asllamadas"] = ac;
                        return RedirectToAction("ExtraerDatosAsignar");
                    }
                }
            }
            return RedirectToAction("Index", "Inicio");

        }

        public ActionResult EliminarFila(String telef) {
            if(Session["asignacion"] ==null) CrearTablaSessionAsig();
              DataTable dt = (DataTable)Session["asignacion"];
            if (dt.Rows.Count > 0){
                foreach (DataRow ror in dt.Rows){
                    if (ror["telefono"].ToString() == telef || ror["telefono"].ToString()==""){
                        ror.Delete();
                        break;
                    }
                }
            }
            return RedirectToAction("ExtraerDatosAsignar");
        }
 
        public ActionResult ExtraerDatosAsignarSession(FormCollection form,String agregar,String GuadarTodo,String Cancelar)
        {
            try
            {
                if (Cancelar != null)
                {
                    RemoverSessiones();
                    String msje = "Se ha cancelado todo el proceso";
                    return RedirectToAction("ExtraerDatosAsignar", new { mensaje = msje });
                }

                if (agregar != null)
                {

                    if (Session["asignacion"] == null) CrearTablaSessionAsig();
                    DataTable dt = (DataTable)Session["asignacion"];
                    if (form["telefono"] == "")
                    {
                        String msje = "No se pudo agregar, ingrese un numero valido,ó /cel/dni";
                        return RedirectToAction("ExtraerDatosAsignar", new { mensaje = msje });
                    }
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (form["telefono"] == dr["telefono"].ToString())
                                {
                                    dr["cliente"] = form["cliente"];
                                    dr["f1"] = form["f1"].Replace(".", ",");
                                    dr["f2"] = form["f2"].Replace(".", ",");
                                    dr["f3"] = form["f3"].Replace(".", ",");
                                    dr["sva"] = form["sva"];
                                    dr["iniciovigencia"] = form["iniciovigencia"];
                                    if (dr["Estadooedicion"].ToString() == "0")
                                    {
                                        dr["Estadooedicion"] = 2;
                                    }
                                    return RedirectToAction("ExtraerDatosAsignar");
                                }

                            }
                        }
                        DataRow r = dt.NewRow();
                        r["telefono"] = form["telefono"];
                        r["cliente"] = form["cliente"];
                        r["f1"] = form["f1"].Replace(".", ",");
                        r["f2"] = form["f2"].Replace(".", ",");
                        r["f3"] = form["f3"].Replace(".", ",");
                        r["sva"] = form["sva"];
                        r["iniciovigencia"] = form["iniciovigencia"];
                        r["Estadooedicion"] = 1;
                        dt.Rows.Add(r);
                    }
                }
                // guarda todos los registros de la session
                if (GuadarTodo != null)
                {
                    Int32 idasesor = (Int32)Session["id"];
                    entUsuario u = null;
                    u = (entUsuario)Session["usuario"];
                    Int32 iduser = u.Usu_Id;
                    DataTable dt = (DataTable)Session["asignacion"];
                    int i = negAsLlamadas.Instancia.GuardarAsLlamadas(dt, idasesor, iduser);
                    RemoverSessiones();
                    return RedirectToAction("lstUsuariosEstadoAsignacionLlamadas");
                }
                return RedirectToAction("ExtraerDatosAsignar");
            }catch(Exception ex) {
                return RedirectToAction("Error", "Error",new {mensaje=ex.Message});
            }
        }

        private void RemoverSessiones(){
            Session.Remove("id");
            Session.Remove("asignacion");
            Session.Remove("user");
        }


        private void CrearTablaSessionAsig(){
            try{
                DataTable dt = new DataTable();
                dt.Columns.Add("Asi_id", Type.GetType("System.Int32"));
                dt.Columns.Add("Asi_estado", Type.GetType("System.Int32"));
                dt.Columns.Add("telefono", Type.GetType("System.String"));
                dt.Columns.Add("cliente", Type.GetType("System.String"));
                dt.Columns.Add("f1", Type.GetType("System.Double"));
                dt.Columns.Add("f2", Type.GetType("System.Double"));
                dt.Columns.Add("f3", Type.GetType("System.Double"));
                dt.Columns.Add("sva", Type.GetType("System.String"));
                dt.Columns.Add("iniciovigencia", Type.GetType("System.String"));
                dt.Columns.Add("Estadooedicion", Type.GetType("System.Int32"));
                dt.Columns.Add("estadoAtencion", Type.GetType("System.Boolean"));
                Session["asignacion"] = dt;
            }
            catch (Exception){
                throw;
            }
        }

    }
}
