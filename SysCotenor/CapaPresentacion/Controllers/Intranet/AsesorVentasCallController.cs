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

        public ActionResult ListaClientesAsignados(String mensaje, Int16? identificador)
        {
            try
            {
                ViewBag.mensaje = mensaje;
                ViewBag.identificador = identificador;
                entUsuario u = (entUsuario)Session["usuario"];
                if (u != null)
                {
                    Int32 UsuarioId = u.Usu_Id;
                    List<entAsigncionLlamadas> lista = negAsigncionLlamadas.Instancia.ListaClientesAsignadosXUsuario(UsuarioId);
                    return View(lista);
                }
                else
                {
                    return RedirectToAction("Index", "Inicio");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("ListaClientesAsignados", "AsesorVentasCall", new { mensaje = e.Message, identificador = 2 });
            }

        }

        [ValidateInput(false)]
        public ActionResult RegistroLlamada(Int32 AsiLlaId, String mensaje, Int16? identificador)
        {
            try
            {
                ViewBag.mensaje = mensaje;
                ViewBag.identificador = identificador;
                entUsuario u = (entUsuario)Session["usuario"];
                if (u != null)
                {
                    //para capturar el usuario en sesion////////////
                    entUsuario user = (entUsuario)Session["usuario"];
                    Int32 idUser = user.Usu_Id;
                    ////////////////////////////////////////////////////////
                    entAsigncionLlamadas asilla = negAsigncionLlamadas.Instancia.BuscaAsiLla(idUser, AsiLlaId);
                    ViewBag.AsiLla = asilla;

                    List<entTipDoc> lsTd = negTipDoc.Instancia.ListaTipDoc();
                    //var lsTipDoc = new SelectList(lsTd, "td_id", "td_nombre");
                    ViewBag.ListaTipDoc = lsTd;

                    List<entSegmento> lsSeg = negSegmento.Instancia.ListaSegmento();
                    //var lsTipDoc = new SelectList(lsTd, "td_id", "td_nombre");
                    ViewBag.ListaSegmento = lsSeg;

                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Inicio");
                }
            }
            catch (ApplicationException x)
            {
                ViewBag.mensaje = x.Message;
                return RedirectToAction("ListaClientesAsignados", "AsesorVentasCall", new { mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {

                return RedirectToAction("ListaClientesAsignados", "AsesorVentasCall", new { mensaje = e.Message, identificador = 2 });
            }

        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult RegistroLlamada(FormCollection form)
        {
            Int32 AsiUsu = Convert.ToInt32(form["txtAsiU"]);
            try
            {
                entUsuario us = (entUsuario)Session["usuario"];
                if (us != null)
                {

                    entSegmento s = new entSegmento();
                    s.Seg_Id = Convert.ToInt32(form["txt_Seg"]);

                    entTipDoc td = new entTipDoc();
                    td.td_id = Convert.ToInt32(form["txt_TipDoc"]);

                    //para capturar el usuario en sesion////////////
                    entUsuario user = (entUsuario)Session["usuario"];
                    String userRegistro = user.Persona.NombreCompleto;
                    ///////////////////////////////////////////                   

                    entCliente c = new entCliente();
                    c.Cli_Id = Convert.ToInt32(form["txtIdC"]);
                    c.Segmento = s;
                    c.TipDoc = td;
                    if (s.Seg_Id == 1) {
                        c.Cli_Nombre = form["txt_NomCli"].ToString();
                        c.Cli_RazonSocial = "";
                    }
                    else {
                        c.Cli_Nombre = "";
                        c.Cli_RazonSocial = form["txt_NomCli"].ToString();
                    }
                    c.Cli_FechaNacimiento = Convert.ToDateTime(form["txtFecNac"]);
                    c.Cli_LugarNacimiento = form["txt_LugNac"].ToString();
                    c.Cli_Numero_Documento = form["txt_NumDocumento"].ToString();
                    c.Cli_Telefono_Referencia = form["txt_TelRef"].ToString();
                    c.Cli_Correo = form["txt_Cor"].ToString();                  
                    c.Cli_UsuarioRegistro = userRegistro;
                    

                    entTelefono t = new entTelefono();
                    t.Tel_Numero = form["txt_Telefono"].ToString();
                    t.Tel_Direccion = form["txt_Direccion"].ToString();


                    entCliente_Telefono ct = new entCliente_Telefono();                    
                    ct.Cliente = c;
                    ct.Telefono = t;
                    ct.AsiUsu = AsiUsu;
                    ct.CliTel_UsuarioRegistro = userRegistro;

                    entPedido p = new entPedido();
                    p.Ped_Cod_Experto = form["txt_CodExperto"].ToString();
                    p.Ped_Dir_Inst= form["txt_Direccion"].ToString();                    
                    p.Ped_Observaciones = form["txtobserva"].ToString();
                    p.Ped_UsuarioRegistro = userRegistro;

                    entUsuario ur = new entUsuario();
                    ur.Usu_Id = user.Usu_Id;

                    entAccionComercial ac = new entAccionComercial();
                    ac.Acc_Id = Convert.ToInt32(form["idAccCom"]);

                    entProducto pro = new entProducto();
                    pro.Pro_ID = Convert.ToInt32(form["Prod"]);

                    entDepartamento d = new entDepartamento();
                    d.idDepa = Convert.ToInt32(form["depto"]);

                    entProvincia prov = new entProvincia();
                    prov.idProv = Convert.ToInt32(form["provin"]);

                    entDistrito dis = new entDistrito();
                    dis.idDist = Convert.ToInt32(form["distrit"]);

                    p.Distrito = dis;
                    p.Provincia = prov;
                    p.Departamento = d;
                    p.Producto = pro;
                    p.AccionComercial = ac;
                    p.Usuario = ur;
                    p.ClienteTelefono = ct;

                    

                    int i = negPedido.Instancia.InsUpdPedido(p,1);
                    if (i > 0)
                    {
                        return RedirectToAction("RegistroLlamada", "AsesorVentasCall", new { mensaje = "Se Inserto Satisfactoriamente", identificador = 3, AsiLlaId= AsiUsu });
                    }
                    else
                    {
                        return RedirectToAction("RegistroLlamada", "AsesorVentasCall", new { mensaje = "Problemas al Insertar", identificador = 2, AsiLlaId = AsiUsu });
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Inicio");
                }
            }
            catch (ApplicationException x)
            {
                ViewBag.mensaje = x.Message;
                return RedirectToAction("RegistroLlamada", "AsesorVentasCall", new { mensaje = x.Message, identificador = 1, AsiLlaId = AsiUsu });
            }
            catch (Exception e)
            {

                return RedirectToAction("RegistroLlamada", "AsesorVentasCall", new { mensaje = e.Message, identificador = 2, AsiLlaId = AsiUsu });
            }
        }
        //public ActionResult ListarMisLlamadas(){
        //    try
        //    {
        //        entUsuario u = new entUsuario();
        //        if (Session["usuario"] != null) u = (entUsuario)Session["usuario"];
        //        List<entAsigncionLlamadas> Lista = negAsLlamadas.Instancia.ListaLamadasAsig(u.Usu_Id);
        //        return View(Lista);
        //    }catch (Exception e){
        //        return RedirectToAction("Error", "Error", new { mensaje = e.Message });
        //    }
        //}

        public ActionResult Venta(String telef)
        {
            try
            {
                ViewBag.tel = telef;
                String dni = "55555555";
                //   entCliente c = negCliente.Instancia.BuscaCliente(telef, dni);
                var cliente = new entCliente();
                ViewBag.cliente = cliente;
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Error", new { mensaje = e.Message });
            }
        }
        //[HttpPost]
        //public ActionResult Venta(FormCollection frm){
        //    int tipedicion = 0;
        //    int tipedicpersona = 0;
        //    int idprod = 0;
        //    try {
        //        entUsuario u = null;
        //        if (Session["usuario"] != null){
        //             u = (entUsuario)Session["usuario"];
        //        }
        //        //pedido------------------------------------------
        //        entPedido p = new entPedido();

        //       // usuario ----------------------------------------
        //        p.Usuario = u;
        //        //accion comercial ------------------------------
        //        entAccionComercial ac = new entAccionComercial();
        //        ac.Acc_Id = Convert.ToInt32(frm["idAccCom"]);
        //        p.AccionComercial = ac;

        //        p.Ped_Dir_Inst = frm["direccion"];
        //        p.Ped_Total = Convert.ToDouble(frm["apagar"]);
        //        idprod = Convert.ToInt32(frm["Prod"]);
        //        //cliente --------------------------------------
        //        entCliente c = new entCliente();
        //        c.Cli_Id = Convert.ToInt32(frm["idcliente"]);
        //        entSegmento sg = new entSegmento();
        //        sg.Seg_Id = 1;//Convert.ToInt32(frm[""]);
        //        c.Segmento = sg;
        //        c.Cli_Ruc = frm["ruc"];
        //        entDistrito d = new entDistrito();
        //        d.idDist = Convert.ToInt32(frm["distrit"]);
        //        c.Cli_Distrito = d;
        //        entProvincia pr = new entProvincia();
        //        pr.idProv = Convert.ToInt32(frm["provin"]);
        //        c.Cli_Provincia = pr;
        //        entDepartamento dpt = new entDepartamento();
        //        dpt.idDepa = Convert.ToInt32(frm["depto"]);
        //        c.Cli_Depardamento = dpt;
        //        c.Cli_Empresa = frm["empresa"];

        //        p.Cliente = c;
        //        // persona -----------------------------------
        //        entPersona per = new entPersona();
        //        per.Per_Id = Convert.ToInt32(frm["idpersona"]);
        //        per.Per_Nombres = frm["cliente"];
        //        per.Per_Apellidos = frm[""];
        //        per.Per_DNI = frm["dni"];
        //        per.Per_Celular = frm["telefref"];
        //        per.Per_Correo = frm["correo"];
        //        per.Per_Direccion = frm["direccion"];
        //        per.Per_FechaNacimiento =Convert.ToDateTime(frm["fnacim"]);
        //        per.Per_LugarNacimiento = frm["lugnacimi"];

        //        // persona_telefono --------------------------------
        //        entPersona_Telefono pt = new entPersona_Telefono();
        //        pt.persona = per;


        //        p.Cliente.Persona_telef = pt;
        //        // telefono ------------------------------------
        //        entTelefono t = new entTelefono();
        //        t.Tel_Numero = frm["telefono"];
        //        pt.telefono = t;
        //        p.Cliente.Persona_telef = pt;
        //        if (Convert.ToInt32(frm["idcliente"]) == 0)
        //        {
        //            tipedicion = 1;
        //        }
        //        else { tipedicion = 2; }
        //        if (Convert.ToInt32(frm["idpersona"]) == 0)
        //        {
        //            tipedicpersona = 1;
        //        }
        //        else {
        //            tipedicpersona = 2;
        //        }

        //        int i = negPedido.Instancia.RegistroPedido(p, idprod, tipedicion,tipedicpersona);

        //        return RedirectToAction("Venta");
        //    }
        //    catch (Exception e){
        //        return RedirectToAction("Error","Error",new { mensaje=e.Message});

        //    }
        //  }

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

        public ActionResult LlenarGrup_ComJSON(int id_detaCC_Com)
        {
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
