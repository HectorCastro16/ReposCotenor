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

        public ActionResult Venta(String telef){
            try{
                ViewBag.tel = telef;
                String dni = "55555555";
                entCliente c = negCliente.Instancia.BuscaCliente(telef,dni);
                var cliente = c;
                ViewBag.cliente = cliente;
                return View();
            }
            catch (Exception e){
                return RedirectToAction("Error", "Error", new { mensaje = e.Message });
            }
        }
        [HttpPost]
        public ActionResult Venta(FormCollection frm){
            int tipedicion = 0;
            int tipedicpersona = 0;
            int idprod = 0;
            try {
                entUsuario u = null;
                if (Session["usuario"] != null){
                     u = (entUsuario)Session["usuario"];
                }
                //pedido------------------------------------------
                entPedido p = new entPedido();

               // usuario ----------------------------------------
                p.Usuario = u;
                //accion comercial ------------------------------
                entAccionComercial ac = new entAccionComercial();
                ac.Acc_Id = Convert.ToInt32(frm["idAccCom"]);
                p.AccionComercial = ac;

                p.Ped_Dir_Inst = frm["direccion"];
                p.Ped_Total = Convert.ToDouble(frm["apagar"]);
                idprod = Convert.ToInt32(frm["Prod"]);
                //cliente --------------------------------------
                entCliente c = new entCliente();
                c.Cli_Id = Convert.ToInt32(frm["idcliente"]);
                entSegmento sg = new entSegmento();
                sg.Seg_Id = 1;//Convert.ToInt32(frm[""]);
                c.Segmento = sg;
                c.Cli_Ruc = frm["ruc"];
                entDistrito d = new entDistrito();
                d.idDist = Convert.ToInt32(frm["distrit"]);
                c.Cli_Distrito = d;
                entProvincia pr = new entProvincia();
                pr.idProv = Convert.ToInt32(frm["provin"]);
                c.Cli_Provincia = pr;
                entDepartamento dpt = new entDepartamento();
                dpt.idDepa = Convert.ToInt32(frm["depto"]);
                c.Cli_Depardamento = dpt;
                c.Cli_Empresa = frm["empresa"];

                p.Cliente = c;
                // persona -----------------------------------
                entPersona per = new entPersona();
                per.Per_Id = Convert.ToInt32(frm["idpersona"]);
                per.Per_Nombres = frm["cliente"];
                per.Per_Apellidos = frm[""];
                per.Per_DNI = frm["dni"];
                per.Per_Celular = frm["telefref"];
                per.Per_Correo = frm["correo"];
                per.Per_Direccion = frm["direccion"];
                per.Per_FechaNacimiento =Convert.ToDateTime(frm["fnacim"]);
                per.Per_LugarNacimiento = frm["lugnacimi"];

                // persona_telefono --------------------------------
                entPersona_Telefono pt = new entPersona_Telefono();
                pt.persona = per;


                p.Cliente.Persona_telef = pt;
                // telefono ------------------------------------
                entTelefono t = new entTelefono();
                t.Tel_Numero = frm["telefono"];
                pt.telefono = t;
                p.Cliente.Persona_telef = pt;
                if (Convert.ToInt32(frm["idcliente"]) == 0)
                {
                    tipedicion = 1;
                }
                else { tipedicion = 2; }
                if (Convert.ToInt32(frm["idpersona"]) == 0)
                {
                    tipedicpersona = 1;
                }
                else {
                    tipedicpersona = 2;
                }

                int i = negPedido.Instancia.RegistroPedido(p, idprod, tipedicion,tipedicpersona);

                return RedirectToAction("Venta");
            }
            catch (Exception e){
                return RedirectToAction("Error","Error",new { mensaje=e.Message});
                
            }
          }

        #region ArbolUbigeoJSON------------------------------------------------------------


        public ActionResult LlenarDistJSON(int idprov)
        {
            var Lista = negArbolUbigeo.Instancia.ListarDist(Convert.ToInt32(idprov));
            var JsonLista = Json(Lista.ToList(), JsonRequestBehavior.AllowGet);
            return JsonLista;
        }

        public ActionResult LlenarProvJSON(int iddepat)
        {
            var Lista = negArbolUbigeo.Instancia.ListarProv(Convert.ToInt32(iddepat));
            var JsonLista = Json(Lista.ToList(), JsonRequestBehavior.AllowGet);
            return JsonLista;
        }

        //public ActionResult LlenarDeptJSON()
        //{
        //    var Lista = negArbolUbigeo.Instancia.ListarDept();
        //    var JsonLista = Json(Lista.ToList(), JsonRequestBehavior.AllowGet);
        //    return JsonLista;
        //}

        #endregion ArbolUbigeoJSON



        #region --------------------------ArbolVentaJSON----------------------------------

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
