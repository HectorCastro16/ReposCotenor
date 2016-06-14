using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;

namespace CapaPresentacion.Controllers.Intranet
{
    public class ComunController : Controller
    {
        //
        // GET: /Comun/

        #region class_precio + imagen para editar---------------------------------------------------------------------
        private static class PrecioImage
        {
            public static int idprecio { get; set; }
            public static Double precio { get; set; }
            public static String image { get; set; }
        }
        #endregion class_precio

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult BuscaCliente(String sms,int? identificador,String checkStatus)
        {
            try
            {
                var documento = negCliente.Instancia.ListaDoc();
                ViewBag.doc = documento;
                ViewBag.sms = sms;
                ViewBag.check = checkStatus;
                if (Session["ObjLista"] != null){
                    ViewBag.ObjLista = Session["ObjLista"];
                    Session.Remove("ObjLista");
                }
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult BuscaCliente(FormCollection frm,String chkotro)
        { int idtipdoc = 0;
            try
            {
                List<entCliente_Telefono> Lista = null;
                String check = "";
                String telef = frm["txttelfB"];
                if (chkotro != null) { idtipdoc = Convert.ToInt32(frm["select1"]);  check = "checked"; } else idtipdoc = 0;
                String numdoc = frm["txtnumdoc"];
                Lista = negCliente.Instancia.BuscaCliente(telef, idtipdoc, numdoc);
                Session["ObjLista"] = Lista;
                return RedirectToAction("BuscaCliente",new {checkStatus= check });
            }catch (ApplicationException a){
                return RedirectToAction("BuscaCliente", new { sms = a.Message, identificador = 1 });
            }
            catch (Exception e)
            {
                return RedirectToAction("BuscaCliente", new { sms = e.Message, identificador = 2 });
            }
        }

        public ActionResult PublicaArt(FormCollection frm, HttpPostedFileBase imagen)
        {
            try
            {
                entUsuario u = null;
                if (Session["usuario"] != null) { u = (entUsuario)Session["usuario"]; }
                entArticulo a = new entArticulo();
                a.usuario = u;
                a.Art_Url = frm["url"];
                a.Art_Titulo = frm["titulo"];
                a.Art_Descripcion = frm["cuerpo"];
                if (imagen != null && imagen.ContentLength > 0)
                {
                    a.Art_Image = Path.GetFileName(imagen.FileName);

                }
                else { a.Art_Image = "sin imagen"; }
                int i = negUsuario.Instancia.PubArticulo(a);
                if (i > 0)
                {
                    if (imagen != null && imagen.ContentLength > 0)
                    {
                        var name = Path.GetFileName(imagen.FileName);
                        var ruta = Path.Combine(Server.MapPath("~/assets/img/ArticulosImg"), name);
                        imagen.SaveAs(ruta);
                    }
                }

                return RedirectToAction("Perfil", new { mensaje = "Publicado.!" });
            }
            catch (Exception err)
            {
                return RedirectToAction("Error", "Error", new { mensaje = err.Message });
            }
        }
        public ActionResult Perfil(String mensaje)
        {
            try
            {
                entUsuario u = null;
                if (Session["usuario"] != null)
                {
                    u = (entUsuario)Session["usuario"];
                }
                List<entSecurity> s = null;
                s = negUsuario.Instancia.ReturUltimoLogeo(u.Usu_Id);
                ViewBag.security = s;
                ViewBag.sms = mensaje;
                return View();

            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Error", new { mensaje = e.Message });
            }
        }

        public ActionResult NoticiasEtc(String search)
        {
            try
            {
                if (search != null)
                {
                    entArticulo a = negUsuario.Instancia.BuscaArt(search);
                    ViewBag.art = a;
                    return View();
                }
                else
                {
                    List<entArticulo> Lista = null;
                    Lista = negUsuario.Instancia.ListaArt();
                    return View(Lista);
                }
            }
            catch (Exception ex)
            {

                return RedirectToAction("Error", "Error", new { mensaje = ex.Message });
            }
        }
        public ActionResult NuewUpdateProducto(int? idprod, String mensaje)
        {
            try
            {
                entProducto p = new entProducto();
                if (idprod != null)
                {
                    p = negProducto.Instancia.BuscaProd(Convert.ToInt32(idprod));
                    PrecioImage.idprecio = p.Precio.Pre_ID;
                    PrecioImage.precio = p.Precio.Pre_producto;
                    PrecioImage.image = p.Pro_Imagen;
                }

                List<entCategoria> cat = negProducto.Instancia.ListCatego();
                var categoria = new SelectList(cat, "Cat_Id", "Cat_Nombre");
                ViewBag.c = categoria;
                ViewBag.message = mensaje;
                return View(p);
            }
            catch (Exception error)
            {
                return RedirectToAction("Error", "Error", new { mensaje = error.Message });
            }

        }
        [HttpPost]
        public ActionResult NuewUpdateProducto(entProducto p, HttpPostedFileBase image)
        {
            try
            {

                entProducto pr = p;
                String usuario = "";
                int tipoEdit = 0, teprecio = 0;
                if (p.Pro_ID != 0)
                {
                    tipoEdit = 2;
                    // si no se cambio la imagen se mantiene con la que tenia ya en bd / si no, se le asigna nueva imagen
                    if (image == null) pr.Pro_Imagen = PrecioImage.image;
                    else pr.Pro_Imagen = Path.GetFileName(image.FileName);
                    // si analiza si precio se edito/ si se edito entonces se hace la insercion de el nuevo precio en bd- si no se mantiene con el mismo(id + precio)
                    if (p.Precio.Pre_producto != PrecioImage.precio)
                    {
                        teprecio = 1;
                    }
                    else
                    {
                        p.Precio.Pre_ID = PrecioImage.idprecio;
                    }
                } // desde aqui aplica para para productos nuevos.
                else
                {
                    tipoEdit = 1; teprecio = 1;
                    if (image != null && image.ContentLength > 0)
                    {
                        pr.Pro_Imagen = Path.GetFileName(image.FileName);
                    }
                    else { pr.Pro_Imagen = "defaultimg.jpg"; }
                }
                if (Session["usuario"] != null)
                {
                    entUsuario u = (entUsuario)Session["usuario"]; usuario = u.Usu_Login;
                }


                int i = 0; negProducto.Instancia.InsUpdProducto(pr, tipoEdit, usuario, teprecio);
                if (i > 0)
                {
                    if (image != null && image.ContentLength > 0)
                    {
                        var nombrearchivo = Path.GetFileName(image.FileName);
                        var ruta = Path.Combine(Server.MapPath("~/assets/img/Producto"), nombrearchivo);
                        image.SaveAs(ruta);

                    }
                }
                String mensajet = ""; if (tipoEdit == 2) mensajet = "Se Edito de Manera Correcta";
                else if (tipoEdit == 1) mensajet = "Se Inserto de Manera Correcta";
                return RedirectToAction("NuewUpdateProducto", new { mensaje = mensajet });
            }
            catch (Exception error)
            {
                return RedirectToAction("Error", "Error", new { mensaje = error.Message });
            }
        }


        public ActionResult ProductosList()
        {
            try
            {

                List<entProducto> ListaP = null;
                ListaP = negProducto.Instancia.ListProd();
                return View(ListaP);
            }
            catch (Exception error)
            {
                return RedirectToAction("Error", "Error", new { mensaje = error.Message });
            }

        }
        public ActionResult ActualizarPass(FormCollection valida)
        {
            try
            {
                Int32 iduser = 0;
                String newpass = "", passactual = "";
                if (Session["usuario"] != null)
                {
                    entUsuario u = (entUsuario)Session["usuario"];
                    iduser = u.Usu_Id;
                    passactual = valida["passactual"];
                    newpass = valida["pass2"].ToString();
                }
                int i = negUsuario.Instancia.ActualizaPass(iduser, newpass, passactual);
                return RedirectToAction("Ajustes", new { mensaje = "Su contraseña se actualizao de manera correcta" });

            }
            catch (ArithmeticException ex)
            {
                return RedirectToAction("Ajustes", new { mensaje = ex.Message, tiperror = 2 });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Error", new { mensaje = e.Message });
            }
        }

        public ActionResult Ajustes(String mensaje, int? tiperror)
        {
            if (mensaje != null)
            {
                ViewBag.terror = tiperror;
                ViewBag.message = mensaje;
            }
            return View();
        }


        public ActionResult ListaUsuarios(String mensaje, Int16? identificador)
        {
            try
            {
                ViewBag.mensaje = mensaje;
                ViewBag.identificador = identificador;
                entUsuario u = (entUsuario)Session["usuario"];
                if (u != null)
                {
                    Int32 sucursalId = u.Sucursal.Suc_Id;
                    Int32 UsuarioId = u.Usu_Id;
                    List<entUsuario> lista = negUsuario.Instancia.ListaUsuarios(UsuarioId, sucursalId);
                    return View(lista);
                }
                else
                {
                    return RedirectToAction("Index", "Inicio");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("ListaUsuarios", "Comun", new { mensaje = e.Message, identificador = 2 });
            }
        }

        public ActionResult DetalleUsuario(Int32 UsuarioId)
        {
            try
            {
                entUsuario u = (entUsuario)Session["usuario"];
                if (u != null)
                {
                    Int32 UsuaIDSuper = u.Usu_Id;
                    entUsuario us = negUsuario.Instancia.DetalleUsuario(UsuarioId, UsuaIDSuper);
                    return View(us);
                }
                else
                {
                    return RedirectToAction("Index", "Inicio");
                }
            }
            catch (ApplicationException x)
            {
                ViewBag.mensaje = x.Message;
                return RedirectToAction("ListaUsuarios", "Comun", new { mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {
                return RedirectToAction("ListaUsuarios", "Comun", new { mensaje = e.Message, identificador = 2 });
            }
        }

        public ActionResult InsUsuario(String mensaje, Int16? identificador)
        {
            try
            {
                ViewBag.mensaje = mensaje;
                ViewBag.identificador = identificador;
                entUsuario u = (entUsuario)Session["usuario"];
                if (u != null)
                {
                    List<entTipoUsuario> t = null;

                    if (u.TipoUsuario.TipUsu_Id == 3)
                    {
                        t = negTipoUsuario.Instancia.ListaTipoUsuarioxId(8);
                    }
                    if (u.TipoUsuario.TipUsu_Id == 6)
                    {
                        t = negTipoUsuario.Instancia.ListaTipoUsuarioxId(9);
                    }
                    var lsTipoUsuario = new SelectList(t, "TipUsu_Id", "TipUsu_Nombre");
                    ViewBag.ListaTipoUsuario = lsTipoUsuario;

                    List<entSucursal> s = negSucursal.Instancia.ListaSucursalxId(u.Sucursal.Suc_Id);
                    var lsSucursal = new SelectList(s, "Suc_Id", "Suc_Nombre");
                    ViewBag.ListaSucursal = lsSucursal;

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
                return RedirectToAction("ListaUsuarios", "Comun", new { mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {

                return RedirectToAction("ListaUsuarios", "Comun", new { mensaje = e.Message, identificador = 2 });
            }
        }

        [HttpPost]
        public ActionResult InsUsuario(entUsuario u, HttpPostedFileBase archivo, FormCollection form)
        {
            try
            {
                entUsuario us = (entUsuario)Session["usuario"];
                if (us != null)
                {
                    String dominio = form["dominio"];
                    String fecNac = form["txtFecNac"];
                    String fecHasta = form["txtFecHasta"];
                    if (dominio != "Otros" && dominio != "Seleccionar")
                        u.Persona.Per_Correo += "@" + dominio;

                    u.Persona.Per_FechaNacimiento = Convert.ToDateTime(fecNac);
                    u.Usu_FechaHasta = Convert.ToDateTime(fecHasta);

                    if (archivo != null && archivo.ContentLength > 0)
                    {
                        u.Persona.Per_Foto = Path.GetFileName(archivo.FileName);
                    }
                    else
                    {
                        u.Persona.Per_Foto = "foto.png";
                    }
                    //para capturar el usuario en sesion////////////
                    entUsuario user = (entUsuario)Session["usuario"];
                    String userRegistro = user.Persona.NombreCompleto;

                    u.Usu_UsuarioRegistro = userRegistro;
                    ///////////////////////////////////////////
                    int i = negUsuario.Instancia.InsUpdUsuario(u, 1);
                    if (i > 0)
                    {
                        if (archivo != null && archivo.ContentLength > 0)
                        {
                            var namearchivo = Path.GetFileName(archivo.FileName);
                            var ruta = Path.Combine(Server.MapPath("~/assets/img/Fotos"), namearchivo);
                            archivo.SaveAs(ruta);
                        }
                        return RedirectToAction("ListaUsuarios", new { mensaje = "Se Inserto Satisfactoriamente", identificador = 3 });
                    }
                    else
                    {
                        return RedirectToAction("InsUsuario", "Comun", new { mensaje = "Problemas al Registrar", identificador = 2 });
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
                return RedirectToAction("InsUsuario", "Comun", new { mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {
                return RedirectToAction("InsUsuario", "Comun", new { mensaje = e.Message, identificador = 2 });
            }
        }

        public ActionResult UpdUsuario(Int16 UsuarioId, String mensaje, Int16? identificador)
        {
            try
            {
                ViewBag.mensaje = mensaje;
                ViewBag.identificador = identificador;
                entUsuario u = (entUsuario)Session["usuario"];
                if (u != null)
                {
                    List<entTipoUsuario> t = null;
                    if (u.TipoUsuario.TipUsu_Id == 3)
                    {
                        t = negTipoUsuario.Instancia.ListaTipoUsuarioxId(8);
                    }
                    if (u.TipoUsuario.TipUsu_Id == 6)
                    {
                        t = negTipoUsuario.Instancia.ListaTipoUsuarioxId(9);
                    }
                    var lsTipoUsuario = new SelectList(t, "TipUsu_Id", "TipUsu_Nombre");
                    ViewBag.ListaTipoUsuario = lsTipoUsuario;

                    List<entSucursal> s = negSucursal.Instancia.ListaSucursalxId(u.Sucursal.Suc_Id);
                    var lsSucursal = new SelectList(s, "Suc_Id", "Suc_Nombre");
                    ViewBag.ListaSucursal = lsSucursal;
                    entUsuario us = negUsuario.Instancia.DetalleUsuario(UsuarioId, u.Usu_Id);
                    return View(us);
                }
                else
                {
                    return RedirectToAction("Index", "Inicio");
                }
            }
            catch (ApplicationException x)
            {
                ViewBag.mensaje = x.Message;
                return RedirectToAction("ListaUsuarios", "Comun", new { mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {
                return RedirectToAction("ListaUsuarios", "Comun", new { mensaje = e.Message, identificador = 2 });
            }
        }

        [HttpPost]
        public ActionResult UpdUsuario(entUsuario u, HttpPostedFileBase archivo, FormCollection form)
        {
            try
            {
                entUsuario us = (entUsuario)Session["usuario"];
                if (us != null)
                {
                    String fecNac = form["txtFecNac"];
                    String fecHasta = form["txtFecHasta"];
                    String foto = form["txtFoto"];
                    u.Persona.Per_FechaNacimiento = Convert.ToDateTime(fecNac);
                    u.Usu_FechaHasta = Convert.ToDateTime(fecHasta);

                    if (archivo != null && archivo.ContentLength > 0)
                    {
                        u.Persona.Per_Foto = Path.GetFileName(archivo.FileName);
                    }
                    else
                    {
                        u.Persona.Per_Foto = foto;
                    }
                    //para capturar el usuario en sesion////////////
                    entUsuario user = (entUsuario)Session["usuario"];
                    String userModificacion = user.Persona.NombreCompleto;

                    u.Usu_UsuarioModificacion = userModificacion;
                    ///////////////////////////////////////////
                    int i = negUsuario.Instancia.InsUpdUsuario(u, 2);

                    if (i > 0)
                    {
                        if (archivo != null && archivo.ContentLength > 0)
                        {
                            var namearchivo = Path.GetFileName(archivo.FileName);
                            var ruta = Path.Combine(Server.MapPath("~/assets/img/Fotos"), namearchivo);
                            archivo.SaveAs(ruta);
                        }
                        return RedirectToAction("ListaUsuarios", new { mensaje = "Se Edito Satisfactoriamente", identificador = 3 });
                    }
                    else
                    {
                        return RedirectToAction("UpdUsuario", "Comun", new { UsuarioId = u.Usu_Id, mensaje = "Problemas al Editar", identificador = 2 });
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
                return RedirectToAction("UpdUsuario", "Comun", new { UsuarioId = u.Usu_Id, mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {
                return RedirectToAction("UpdUsuario", "Comun", new { UsuarioId = u.Usu_Id, mensaje = e.Message, identificador = 2 });
            }
        }

        public ActionResult DelUsuario(FormCollection form)
        {
            try
            {
                entUsuario us = (entUsuario)Session["usuario"];
                if (us != null)
                {
                    String idUsuDel = form["txtUserId"];
                    Int16 idUser = Convert.ToInt16(idUsuDel);
                    entUsuario u = new entUsuario();
                    u.Usu_Id = idUser;
                    //para capturar el usuario en sesion////////////
                    entUsuario user = (entUsuario)Session["usuario"];
                    String userModificacion = user.Persona.NombreCompleto;

                    u.Usu_UsuarioModificacion = userModificacion;
                    ///////////////////////////////////////////
                    int i = negUsuario.Instancia.DelBloActUsurio(u, 3);
                    return RedirectToAction("ListaUsuarios", new { mensaje = "Se Elimino Satisfactoriamente", identificador = 3 });
                }
                else
                {
                    return RedirectToAction("Index", "Inicio");
                }
            }
            catch (ApplicationException x)
            {
                ViewBag.mensaje = x.Message;
                return RedirectToAction("ListaUsuarios", "Comun", new { mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {
                return RedirectToAction("ListaUsuarios", "Comun", new { mensaje = e.Message, identificador = 2 });
            }
        }

        public ActionResult BloUsuario(FormCollection form)
        {
            try
            {
                entUsuario us = (entUsuario)Session["usuario"];
                if (us != null)
                {
                    String idUsuBlo = form["txtUserId"];
                    Int16 idUser = Convert.ToInt16(idUsuBlo);
                    entUsuario u = new entUsuario();
                    u.Usu_Id = idUser;
                    //para capturar el usuario en sesion////////////
                    entUsuario user = (entUsuario)Session["usuario"];
                    String userModificacion = user.Persona.NombreCompleto;

                    u.Usu_UsuarioModificacion = userModificacion;
                    ///////////////////////////////////////////
                    int i = negUsuario.Instancia.DelBloActUsurio(u, 4);
                    return RedirectToAction("ListaUsuarios");
                    //return RedirectToAction("ListaUsuarios", new { mensaje = "Se Elimino Satisfactoriamente", identificador = 3 });
                }
                else
                {
                    return RedirectToAction("Index", "Inicio");
                }
            }
            catch (ApplicationException x)
            {
                ViewBag.mensaje = x.Message;
                return RedirectToAction("ListaUsuarios", "Comun", new { mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {
                return RedirectToAction("ListaUsuarios", "Comun", new { mensaje = e.Message, identificador = 2 });
            }
        }
        public ActionResult ActUsuario(FormCollection form)
        {
            try
            {
                entUsuario us = (entUsuario)Session["usuario"];
                if (us != null)
                {
                    String idUsuAct = form["txtUserId"];
                    Int16 idUser = Convert.ToInt16(idUsuAct);
                    entUsuario u = new entUsuario();
                    u.Usu_Id = idUser;
                    //para capturar el usuario en sesion////////////
                    entUsuario user = (entUsuario)Session["usuario"];
                    String userModificacion = user.Persona.NombreCompleto;

                    u.Usu_UsuarioModificacion = userModificacion;
                    ///////////////////////////////////////////
                    int i = negUsuario.Instancia.DelBloActUsurio(u, 5);
                    return RedirectToAction("ListaUsuarios");
                    //return RedirectToAction("ListaUsuarios", new { mensaje = "Se Elimino Satisfactoriamente", identificador = 3 });
                }
                else
                {
                    return RedirectToAction("Index", "Inicio");
                }
            }
            catch (ApplicationException x)
            {
                ViewBag.mensaje = x.Message;
                return RedirectToAction("ListaUsuarios", "Comun", new { mensaje = x.Message, identificador = 1 });
            }
            catch (Exception e)
            {
                return RedirectToAction("ListaUsuarios", "Comun", new { mensaje = e.Message, identificador = 2 });
            }
        }

    }
}
