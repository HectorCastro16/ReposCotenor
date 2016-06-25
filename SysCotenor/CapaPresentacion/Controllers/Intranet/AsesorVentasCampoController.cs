using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidades;
using CapaNegocio;

namespace CapaPresentacion.Controllers.Intranet
{
    public class AsesorVentasCampoController : Controller
    {
        //
        // GET: /AsesorVentasCampo/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PrincipalAsesorVentasCampo()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult RegistroVenta(String mensaje, Int16? identificador)
        {
            try
            {
                ViewBag.mensaje = mensaje;
                ViewBag.identificador = identificador;
                entUsuario u = (entUsuario)Session["usuario"];
                if (u != null)
                {
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
                return RedirectToAction("RegistroVenta", "AsesorVentasCampo", new { mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {

                return RedirectToAction("RegistroVenta", "AsesorVentasCampo", new { mensaje = e.Message, identificador = 2 });
            }


        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult RegistroVenta(FormCollection form) {

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
                    //c.Cli_Id = Convert.ToInt32(form["txtIdC"]);
                    c.Segmento = s;
                    c.TipDoc = td;
                    if (s.Seg_Id == 1)
                    {
                        c.Cli_Nombre = form["txt_NomCli"].ToString();
                        c.Cli_RazonSocial = "";
                    }
                    else
                    {
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
                    //ct.AsiUsu = AsiUsu;
                    ct.CliTel_UsuarioRegistro = userRegistro;

                    entPedido p = new entPedido();
                    p.Ped_Cod_Experto = form["txt_CodExperto"].ToString();
                    p.Ped_Dir_Inst = form["txt_Direccion"].ToString();
                    p.Ped_Observaciones = form["txtobserva"].ToString();
                    p.PedidoX = form["txtCordenadaX"].ToString();
                    p.PedidoY = form["txtCoordenadaY"].ToString();
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

                    if (p.PedidoX == "" || p.PedidoY == "" || p.PedidoX == null || p.PedidoY == null) {

                        return RedirectToAction("RegistroVenta", "AsesorVentasCampo", new { mensaje = "Debe marcar en el mapa una ubicación de referencia", identificador = 2 });
                    }

                    int i = negPedido.Instancia.InsUpdPedidoCampo(p, 1);
                    if (i > 0)
                    {
                        return RedirectToAction("RegistroVenta", "AsesorVentasCampo", new { mensaje = "Se Inserto Satisfactoriamente", identificador = 3});
                    }
                    else
                    {
                        return RedirectToAction("RegistroVenta", "AsesorVentasCampo", new { mensaje = "Problemas al Insertar", identificador = 2 });
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
                return RedirectToAction("RegistroVenta", "AsesorVentasCampo", new { mensaje = x.Message, identificador = 1});
            }
            catch (Exception e)
            {

                return RedirectToAction("RegistroVenta", "AsesorVentasCampo", new { mensaje = e.Message, identificador = 2});
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
