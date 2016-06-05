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
                    //var lsProductosSVA = new SelectList(lsSVA, "Pro_ID", "Pro_Nombre");
                    ViewBag.ListaProductosSVA = lsSVA;

                    List<entSegmento> lsSeg = negSegmento.Instancia.ListaSegmento();
                    var lssegmento = new SelectList(lsSeg, "Seg_Id", "Seg_Nombre");
                    ViewBag.ListaSegmento = lssegmento;
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
                String selectSVA = form["txt_svas"];
                String seg = form["txt_Segmento"];
                List<Int32> SVAS = DividirCadenaRetornaListInt32(selectSVA);
                //Int32 avalor = Convert.ToInt32(selectSVA);

                return RedirectToAction("InsertarDataCliente", "AdministradorData", new { mensaje ="Se Guardo Satisfactoriamente", identificador = 3 });
            }
            catch (Exception e)
            {
                return RedirectToAction("InsertarDataCliente", "AdministradorData", new { mensaje = e, identificador = 2 });
            }


        }
    }
}
