﻿
using MouseRidersWeb.Assembler;
using MouseRidersGenNHibernate.CAD.MouseRiders;
using MouseRidersGenNHibernate.CEN.MouseRiders;
using MouseRidersWeb.DTO;
using MouseRidersGenNHibernate.EN.MouseRiders;
using MouseRidersWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.IO;

namespace MouseRidersWeb.Controllers
{
    public class UsuarioController : BasicController
    {


        public ActionResult Bloquear(int id)
        {
            SessionInitialize();
            UsuarioCAD cCAD = new UsuarioCAD(session);
            UsuarioEN result = cCAD.ReadOIDDefault(id);
            BloqueoCEN bloqueo = new BloqueoCEN();
            IList<DenunciaEN> denuncias = result.RecibeD;
            IList<int> iddenuncia = new List<int>();
            foreach (DenunciaEN entry in denuncias)
            {
                iddenuncia.Add(entry.Id);
            }
            DateTime algoaux = DateTime.Now;
            algoaux.AddDays(7);
            bloqueo.CrearBloqueo(iddenuncia, id, DateTime.Now, algoaux);
            SessionClose();
            return RedirectToAction("VerDenunciasRecibidas", "Usuario", new { id = id });
        }

        //
        // GET:

        public ActionResult Upload1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {

                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Contenido/FotosUsuario"), fileName);
                    file.SaveAs(path);

                    SessionInitialize();
                    UsuarioCAD cCAD = new UsuarioCAD(session);
                    int id = Int32.Parse(Session["user_id"].ToString());

                    UsuarioEN usu = cCAD.ReadOID(id);
                    UsuarioCEN cen = new UsuarioCEN();
                    cen.ModificarUsuario(usu.Id, usu.Email, usu.Nombre, usu.Apellidos, usu.Pais, usu.Telefono, usu.Puntuacion, usu.FechaRegistro, usu.Contrasenya, usu.Nombreusuario, fileName);
                    SessionClose();
                }

                ViewBag.Message = "Upload successful";
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "Upload failed";
                return RedirectToAction("Uploads");
            }
        }


        //
        // GET: /Usuario/

        public ActionResult Index()
        {
            if (Session["user_rol"] != null && (Session["user_rol"].ToString() == "2" || Session["user_rol"].ToString() == "4"))
            {
                SessionInitialize();
                UsuarioCAD cCAD = new UsuarioCAD(session);
                IList<UsuarioEN> resultEN = cCAD.ReadAllDefault(0, 999);
                IList<UsuarioDTO> result = new List<UsuarioDTO>();
                for (int i = 0; i < resultEN.Count; i++)
                {
                    result.Add(new UsuarioAssembler().Convert(resultEN[i]));
                }
                SessionClose();
                return View(result);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // GET: /Usuario/Details/5

        public ActionResult Details(int id)
        {
            SessionInitialize();
            UsuarioCAD cCAD = new UsuarioCAD(session);
            UsuarioEN result = cCAD.ReadOIDDefault(id);
            UsuarioDTO resultfinal = new UsuarioAssembler().Convert(result);
            SessionClose();
            return View(resultfinal);
        }

        //
        // GET: /Usuario/MensajesPrivados/5

        public ActionResult MensajesPrivados(int id)
        {
            SessionInitialize();
            UsuarioCAD cCAD = new UsuarioCAD(session);
            UsuarioEN result = cCAD.ReadOIDDefault(id);
            UsuarioDTO resultfinal = new UsuarioAssembler().ConvertConCorreoRecibido(result);
            SessionClose();
            return View(resultfinal);
        }

        //
        // GET: /Usuario/Denuncias/5

        public ActionResult VerDenunciasRecibidas(int id)
        {
            SessionInitialize();
            UsuarioCAD cCAD = new UsuarioCAD(session);
            UsuarioEN cCEN = cCAD.ReadOIDDefault(id);
            IList<DenunciaEN> resultEN = cCEN.RecibeD;
            IList<DenunciaDTO> resultfinal = new List<DenunciaDTO>();
            foreach (DenunciaEN entry in resultEN)
                resultfinal.Add(new DenunciaAssembler().Convert(entry));
            SessionClose();
            return View(resultfinal);
        }
        //
        // GET: /Usuario/Denuncias/5

        public ActionResult VerMensajesRecibidos(int id)
        {
            SessionInitialize();
            UsuarioCAD cCAD = new UsuarioCAD(session);
            UsuarioEN result = cCAD.ReadOIDDefault(id);
            UsuarioDTO resultfinal = new UsuarioAssembler().ConvertConCorreoRecibido(result);
            SessionClose();
            return View(resultfinal);
        }
        //
        // GET: /Usuario/Denuncias/5

        public ActionResult VerMensajesEnviados(int id)
        {
            SessionInitialize();
            UsuarioCAD cCAD = new UsuarioCAD(session);
            UsuarioEN result = cCAD.ReadOIDDefault(id);
            UsuarioDTO resultfinal = new UsuarioAssembler().ConvertConCorreoEnviado(result);
            SessionClose();
            return View(resultfinal);
        }

        //
        // GET: /Denuncia/Create

        public ActionResult Denunciar(int id)
        {
            DenunciaEN denuncia = new DenunciaEN();
            DenunciaDTO result = new DenunciaAssembler().Convert(denuncia);
            return View(result);
        }

        //
        // POST: /Denuncia/Create

        [HttpPost]
        public ActionResult Denunciar(DenunciaEN denuncia)
        {
            try
            {
                DenunciaCAD cCAD = new DenunciaCAD();
                DenunciaCEN cen = new DenunciaCEN(cCAD);
                DateTime p_fecha = DateTime.Now;
                int id = cen.CrearDenuncia(denuncia.Motivo, denuncia.Es_creada.Id, p_fecha, denuncia.Es_recibida.Id);
                return RedirectToAction("Details", new { id = id });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Login()
        {
            UsuarioDTO result = new UsuarioDTO();
            return View(result);
        }

        [HttpPost]
        public ActionResult Login(UsuarioEN model)
        {
            SessionInitialize();
            UsuarioCAD cCAD = new UsuarioCAD(session);
            int user_rol = new UsuarioCEN(cCAD).Autenticar(model.Nombreusuario, model.Contrasenya);
            SessionClose();
            if (user_rol == -1)
            {
                // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
                ModelState.AddModelError("", "El nombre de usuario o la contraseña especificados son incorrectos.");
                return View(new UsuarioAssembler().Convert(model));
            }
            else
            {
                UsuarioEN usuario = new UsuarioCEN().ReadFilterAuth(model.Nombreusuario);
                Session["user_rol"] = "" + user_rol;
                Session["user_id"] = usuario.Id;
                Session["user_name"] = usuario.Nombreusuario;
                Session["user_email"] = usuario.Email;
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult Logout()
        {
            Session["user_rol"] = null;
            Session["user_id"] = null;
            Session["user_name"] = null;
            Session["user_email"] = null;
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Usuario/Create

        public ActionResult Create()
        {
            UsuarioEN usu = new UsuarioEN();
            UsuarioDTO result = new UsuarioAssembler().Convert(usu);
            return View(result);
        }

        //
        // POST: /Usuario/Create

        [HttpPost]
        public ActionResult Create(UsuarioEN usu)
        {
            try
            {
                UsuarioCEN cen = new UsuarioCEN();
                DateTime p_fecha = DateTime.Now;
                usu.Fotoperfil = "default.jpg";
                int id = cen.CrearUsuario(usu.Email, usu.Nombre, usu.Apellidos, usu.Pais, usu.Telefono, 0, p_fecha, usu.Contrasenya, usu.Nombreusuario, usu.Fotoperfil);
                Session["user_rol"] = "1";
                Session["user_id"] = id;
                Session["user_name"] = usu.Nombreusuario;
                Session["user_email"] = usu.Email;
                return RedirectToAction("Details", new { id = id });
            }
            catch (Exception en)
            {
                throw en;
            }
            finally
            {

            }
        }

        //
        // GET: /Usuario/Edit/5

        public ActionResult Edit(int id)
        {
            UsuarioCAD cCAD = new UsuarioCAD();
            UsuarioEN resultEN = cCAD.ReadOIDDefault(id);
            UsuarioDTO result = new UsuarioAssembler().Convert(resultEN);
            return View(result);
        }

        //
        // POST: /Usuario/Edit/5

        [HttpPost]
        public ActionResult Edit(UsuarioEN usu)
        {
            try
            {
                UsuarioCEN cen = new UsuarioCEN();
                cen.ModificarUsuario(usu.Id, usu.Email, usu.Nombre, usu.Apellidos, usu.Pais, usu.Telefono, usu.Puntuacion, usu.FechaRegistro, usu.Contrasenya, usu.Nombreusuario, usu.Fotoperfil);

                return RedirectToAction("Details", new { id = usu.Id });
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Usuario/Delete/5

        public ActionResult Delete(int id)
        {
            SessionInitialize();
            UsuarioCAD cCAD = new UsuarioCAD(session);
            UsuarioEN result = cCAD.ReadOIDDefault(id);
            UsuarioDTO resultfinal = new UsuarioAssembler().Convert(result);
            SessionClose();

            return View(resultfinal);
        }

        //
        // POST: /Usuario/Delete/5

        [HttpPost]
        public ActionResult Delete(UsuarioEN usu)
        {
            try
            {
                new UsuarioCEN().BorrarUsuario(usu.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
