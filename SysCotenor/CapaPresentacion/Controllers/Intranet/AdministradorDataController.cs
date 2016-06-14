using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
                    var lsProductosSVA = new SelectList(lsSVA, "Pro_ID", "Pro_Nombre");
                    ViewBag.ListaProductosSVA = lsProductosSVA;

                    List<entTipDoc> lsTd = negTipDoc.Instancia.ListaTipDoc();
                    var lsTipDoc = new SelectList(lsTd, "td_id", "td_nombre");
                    ViewBag.ListaTipDoc = lsTipDoc;

                    return View();
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

        public List<Int32> DividirCadenaRetornaListInt32(String texto)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] text = texto.Split(delimiterChars);
            List<Int32> numeros = new List<Int32>();

            foreach (string s in text)
            {
                numeros.Add(Convert.ToInt32(s));
            }
            return numeros;
        }

        [HttpPost]
        public ActionResult InsertarDataCliente(FormCollection form)
        {

            try
            {

                entSegmento s = new entSegmento();
                s.Seg_Id = Convert.ToInt32(form["txt_Segmento"]);

                entTipDoc td = new entTipDoc();
                td.td_id = Convert.ToInt32(form["txt_TipDoc"]);

                entCliente c = new entCliente();
                c.Segmento = s;
                c.TipDoc = td;
                c.Cli_Nombre = form["txt_Nom"].ToString();
                c.Cli_RazonSocial = form["txt_Rs"].ToString();
                c.Cli_Numero_Documento = form["txt_NumDocumento"].ToString();
                //String FechNac = form["txt_FecNac"].ToString();
                c.Cli_FechaNacimiento = Convert.ToDateTime(form["txt_FecNac"].ToString());
                c.Cli_LugarNacimiento = form["txt_LugNac"].ToString();
                c.Cli_Correo = form["txt_CliCorreo"].ToString();
                c.Cli_Telefono_Referencia = form["txt_TelRef"].ToString();

                entTelefono t = new entTelefono();
                t.Tel_Numero = form["txt_Telefono"].ToString();
                t.Tel_Producto = form["txt_Producto"].ToString();
                t.Tel_Direccion = form["txt_Direccion"].ToString();
                t.Tel_FechaAlta = Convert.ToDateTime(form["txt_Fecha_Alta"]);
                //String pru = form["txt_F1"].ToString();
                t.Tel_F1 = Convert.ToDouble(form["txt_F1"]);
                t.Tel_F2 = Convert.ToDouble(form["txt_F2"]);
                t.Tel_F3 = Convert.ToDouble(form["txt_F3"]);

                List < Int32 > SVAS = null;
                String selectSVA = form["txt_svas"];
                SVAS = (selectSVA != null) ? DividirCadenaRetornaListInt32(selectSVA) : null;

                entCliente_Telefono ct = new entCliente_Telefono();
                ct.Cliente = c;
                ct.Telefono = t;

                Session["ClienteTelefono"] = ct;
                Session["ClienteTelefono"] = SVAS;



                return RedirectToAction("InsertarDataCliente", "AdministradorData", new { mensaje = "Se Guardo Satisfactoriamente", identificador = 3 });
            }
            catch (Exception e)
            {
                return RedirectToAction("InsertarDataCliente", "AdministradorData", new { mensaje = e, identificador = 2 });
            }


        }
    }
}
