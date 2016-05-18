using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidades;
using CapaNegocio;
namespace CapaPresentacion.Controllers.Intranet
{
    public class AsesorVentasCallController : Controller
    {
        //
        // GET: /AsesorVentasCall/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PrincipalAsesorVentasCall()
        {
            return View();
        }


        public ActionResult ListarMisLlamadas(){
            try
            {
                entUsuario u = new entUsuario();
                if (Session["usuario"] != null) u = (entUsuario)Session["usuario"];
                List<entAsigncionLlamadas> Lista = negAsLlamadas.Instancia.ListaLamadasAsig(u.Usu_Id);
                return View(Lista);
            }catch (Exception e){
                return RedirectToAction("Error", "Error", new { mensaje = e.Message });
            }
        }

        public ActionResult Venta(){
            try{
                return View();
            }
            catch (Exception e){
                return RedirectToAction("Error", "Error", new { mensaje = e.Message });
            }
        }
        [HttpPost]
        public ActionResult Venta(FormCollection frm){
            try{

                //String id = frm["Cbo_ac"].ToString();
                //Int32 i = Convert.ToInt32(frm["Cbo_ac"]);
                
                return RedirectToAction("Venta");
            }
            catch (Exception e){
                return RedirectToAction("Error","Error",new { mensaje=e.Message});
                
            }
          }

        #region --------------------------ArbolJSON----------------------------------

        public ActionResult LlenarDetAccComJSON(int idAccCom)
        {
            var Lista = negArbolVenta.Instancia.ListaDetAccCbo(Convert.ToInt32(idAccCom));
            var JsonLista = Json(Lista.ToList(), JsonRequestBehavior.AllowGet);
            return JsonLista;
        }

        public ActionResult LlenarGrup_ComJSON(int id_detaCC_Com){
            var lista = negArbolVenta.Instancia.ListaGrupComCbo(Convert.ToInt32(id_detaCC_Com));
            var JsonLista = Json(lista.ToList(), JsonRequestBehavior.AllowGet);
            return JsonLista;
        }
        public ActionResult LlenarCatJSON(int id_Grup)
        {
            var lista = negArbolVenta.Instancia.ListaCatComCbo(Convert.ToInt32(id_Grup));
            var JsonLista = Json(lista.ToList(), JsonRequestBehavior.AllowGet);
            return JsonLista;
        }

        public ActionResult LlenarProdJSON(int id_Cat)
        {
            var lista = negArbolVenta.Instancia.ListaprodCombo(Convert.ToInt32(id_Cat));
            var JsonLista = Json(lista.ToList(), JsonRequestBehavior.AllowGet);
            return JsonLista;
        }
        #endregion ArbolJSON

    }
}
