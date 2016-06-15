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
                //FormCollection form = new FormCollection(Request.Unvalidated.Form);

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
                //c.Cli_FechaNacimiento = Convert.ToDateTime(form["txt_FecNac"].ToString());
                //c.Cli_LugarNacimiento = form["txt_LugNac"].ToString();
                //c.Cli_Correo = form["txt_CliCorreo"].ToString();
                //c.Cli_Telefono_Referencia = form["txt_TelRef"].ToString();
                //c.Cli_Estado = form["txt_Estado"].ToString();
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
                else {
                    return RedirectToAction("InsertarDataCliente", "AdministradorData", new { mensaje = "Problemas al Insertar", identificador = 2 });
                }


                    
            }
            catch (Exception e)
            {
                return RedirectToAction("InsertarDataCliente", "AdministradorData", new { mensaje = e, identificador = 2 });
            }


        }
    }
}
