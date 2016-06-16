using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using CapaEntidades;
using CapaNegocio;

namespace CapaPresentacion.Controllers.Intranet
{
    public class AdministradorDataController : Controller
    {
        //
        // GET: /AdministradorData/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PrincipalAdministradorData()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult InsertarDataCliente(String mensaje, Int16? identificador)
        {
            try
            {
                ViewBag.mensaje = mensaje;
                ViewBag.identificador = identificador;
                entUsuario u = (entUsuario)Session["usuario"];
                if (u != null)
                {
                    List<entProducto> lsSVA = negProducto.Instancia.ListaProductoSVA();
                    //var lsProductosSVA = new SelectList(lsSVA, "Pro_ID", "Pro_Nombre");
                    ViewBag.ListaProductosSVA = lsSVA;

                    List<entTipDoc> lsTd = negTipDoc.Instancia.ListaTipDoc();
                    //var lsTipDoc = new SelectList(lsTd, "td_id", "td_nombre");
                    ViewBag.ListaTipDoc = lsTd;

                    List<entUsuario> lsSupCall = negUsuario.Instancia.ListaSupersCall(u.Sucursal.Suc_Id);
                    //var lsTipDoc = new SelectList(lsTd, "td_id", "td_nombre");
                    ViewBag.ListaSupCall = lsSupCall;

                    List<entCliente_Telefono> lsCliTel = negCliente_Telefono.Instancia.ListaClientesParaAsignar();
                    //ViewBag.ListaCliTel = lsCliTel;
                    var model = new entCliente_TelefonoView { Cliente_Telefono = lsCliTel };

                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Inicio");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("InsertarDataCliente", "AdministradorData", new { mensaje = e.Message, identificador = 2 });
            }
        }

        public List<entProducto> DividirCadenaRetornaListSVAS(String texto)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] text = texto.Split(delimiterChars);
            List<entProducto> SVAS = new List<entProducto>();

            foreach (string s in text)
            {
                entProducto p = new entProducto();
                p.Pro_ID = Convert.ToInt32(s);
                SVAS.Add(p);
            }
            return SVAS;
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult InsertarDataCliente(FormCollection form)
        {
            try
            {
                entUsuario us = (entUsuario)Session["usuario"];
                if (us != null)
                {

                    entSegmento s = new entSegmento();
                    s.Seg_Id = Convert.ToInt32(form["txt_Segmento"]);

                    entTipDoc td = new entTipDoc();
                    td.td_id = Convert.ToInt32(form["txt_TipDoc"]);

                    //para capturar el usuario en sesion////////////
                    entUsuario user = (entUsuario)Session["usuario"];
                    String userRegistro = user.Persona.NombreCompleto;
                    ///////////////////////////////////////////

                    List<entProducto> lsSVAS = new List<entProducto>();
                    String selectSVA = form["txt_svas"];
                    lsSVAS = (selectSVA != null) ? DividirCadenaRetornaListSVAS(selectSVA) : null;

                    entCliente c = new entCliente();
                    c.Segmento = s;
                    c.TipDoc = td;
                    c.Cli_Nombre = form["txt_Nom"].ToString();
                    c.Cli_RazonSocial = form["txt_Rs"].ToString();
                    c.Cli_Numero_Documento = form["txt_NumDocumento"].ToString();
                    c.Cli_UsuarioRegistro = userRegistro;
                    c.SVAS = lsSVAS;

                    entTelefono t = new entTelefono();
                    t.Tel_Numero = form["txt_Telefono"].ToString();
                    t.Tel_Producto = form["txt_Producto"].ToString();
                    t.Tel_Direccion = form["txt_Direccion"].ToString();
                    t.Tel_FechaAlta = Convert.ToDateTime(form["txt_Fecha_Alta"]);
                    t.Tel_F1 = Convert.ToDouble(form["txt_F1"]);
                    t.Tel_F2 = Convert.ToDouble(form["txt_F2"]);
                    t.Tel_F3 = Convert.ToDouble(form["txt_F3"]);

                    entCliente_Telefono ct = new entCliente_Telefono();
                    ct.Cliente = c;
                    ct.Telefono = t;
                    ct.CliTel_UsuarioRegistro = userRegistro;

                    //Session["ClienteTelefono"] = ct;
                    //Session["sva"] = SVAS;

                    int i = negCliente_Telefono.Instancia.InsUpdCliente(ct, 1);
                    if (i > 0)
                    {
                        return RedirectToAction("InsertarDataCliente", "AdministradorData", new { mensaje = "Se Inserto Satisfactoriamente", identificador = 3 });
                    }
                    else
                    {
                        return RedirectToAction("InsertarDataCliente", "AdministradorData", new { mensaje = "Problemas al Insertar", identificador = 2 });
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Inicio");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("InsertarDataCliente", "AdministradorData", new { mensaje = e, identificador = 2 });
            }


        }

        public ActionResult AsesoresVentasXSuper(Int32 IDSupCall)
        {
            try
            {
                entUsuario us = (entUsuario)Session["usuario"];
                if (us != null)
                {
                    Int32 idSupCall = Convert.ToInt32(IDSupCall);
                    List<entUsuario> lsAsexSup = negUsuario.Instancia.ListaUsuarios(idSupCall,us.Sucursal.Suc_Id);
                    ViewBag.ListaAseXSup = lsAsexSup;
                    return PartialView();
                }
                else
                {
                    return RedirectToAction("Index", "Inicio");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("InsertarDataCliente", "AdministradorData", new { mensaje = e.Message, identificador = 2 });
            }

        }        

        [HttpPost]
        public ActionResult AsignaClienteUsuario(entCliente_TelefonoView model)
        {
            List<entCliente_Telefono> Lista = model.Cliente_Telefono;
            Int32 cantselect = 0;
            foreach (entCliente_Telefono item in Lista) {
                if (item.IsSelected == true) {
                    cantselect++;
                }
            }
            if (cantselect != 0)
            {
                return RedirectToAction("InsertarDataCliente", "AdministradorData", new { mensaje = "PRUEBA!!!! - Se Asigno Data Satisfactoriamente", identificador = 3 });
            }
            else {
                return RedirectToAction("InsertarDataCliente", "AdministradorData", new { mensaje = "PRUEBA!!!!! -Debe Seleccionar por lo menos un Cliente", identificador = 2 });
            }
            
            //return View(model);
        }
    }
}
