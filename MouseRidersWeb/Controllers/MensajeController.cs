﻿using MouseRidersGenNHibernate.CAD.MouseRiders;
using MouseRidersGenNHibernate.CEN.MouseRiders;
using MouseRidersGenNHibernate.EN.MouseRiders;
using MouseRidersWeb.Assembler;
using MouseRidersWeb.Controllers;
using MouseRidersWeb.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MouseRidersWeb.Controllers
{
    public class MensajeController : BasicController
    {

        public ActionResult Index()
        {
            SessionInitialize();
            MensajeCAD cCAD = new MensajeCAD(session);
            IList<MensajeEN> resultEN = cCAD.ReadAllDefault(0, 10);
            IList<MensajeDTO> result = new List<MensajeDTO>();
            for (int i = 0; i < resultEN.Count; i++)
            {
                result.Add(new MensajeAssembler().Convert(resultEN[i]));
            }
            SessionClose();
            return View(result);
        }

        public ActionResult MensajePorTipo(MouseRidersGenNHibernate.Enumerated.MouseRiders.T_MensajeEnum tipo)
        {
            
            SessionInitialize();
            MensajeCAD cCAD = new MensajeCAD(session);
            IList<MensajeEN> resultEN = cCAD.ReadFilter(tipo,null);
            IList<MensajeDTO> result = new List<MensajeDTO>();
            for (int i = 0; i < resultEN.Count; i++)
            {
                result.Add(new MensajeAssembler().Convert(resultEN[i]));
            }
            SessionClose();
            return View(result);
        }


        public ActionResult Delete(int id)
        {
            SessionInitialize();
            MensajeCAD cCAD = new MensajeCAD(session);
            MensajeEN result = cCAD.ReadOIDDefault(id);
            MensajeDTO resultfinal = new MensajeAssembler().Convert(result);
            SessionClose();
            return View(resultfinal);
        }

        //
        // POST: /Articulo/Delete/5

        [HttpPost]
        public ActionResult Delete(MensajeEN art)
        {
            try
            {
                //Varios intentos de FOREIGN KEY
                SessionInitialize();
                MensajeCEN cen = new MensajeCEN();
                MensajeEN result = cen.ReadOID(art.Id);
                MensajeCAD cad = new MensajeCAD(session);
                result.Es_enviado = null;
                result.Es_enviadoN = null;
                result.Es_recibido = null;
                 cen.RelacionaMensaje(result.Id, 0);
                cad.ModificarMensaje(result);
                //cen.BorrarMensaje(art.Id);
                SessionClose();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: /Mensaje/Create

        public ActionResult Create()
        {
            MensajeEN men = new MensajeEN();
            MensajeDTO result = new MensajeAssembler().Convert(men);
            return View(result);
        }

        //
        // POST: /Mensaje/Create
        
        [HttpPost]
        public ActionResult Create(MensajeDTO men)
        {
            
                MensajeCAD cCAD = new MensajeCAD();
                MensajeCEN cen = new MensajeCEN(cCAD);

                UsuarioCAD uCAD = new UsuarioCAD();
                UsuarioCEN ucen = new UsuarioCEN(uCAD);
                string aux1 = Session["user_email"].ToString();
                int oid = ucen.ReadFilterPorEmail(aux1).Id;

                IList<int> aux = new List<int>();
                UsuarioEN pepe = ucen.ReadFilterPorEmail(men.Destinatario);
                if (pepe != null)
                {
                    aux.Add(pepe.Id);
                }
                else
                {
                    
                    return RedirectToAction("Details", "Usuario", new { id = Session["user_id"].ToString(), error="si" });
                }
                
                int id = cen.CrearMensaje(men.Asunto, men.Texto, "default", men.Tipo, oid, aux);

                return RedirectToAction("Details", new { id = id });
       
        }

        // GET: /Mensaje/Create

        public ActionResult Sugerir()
        {
            if (Session["user_email"] != null && Session["user_email"].ToString() != "")
            {
                MensajeEN men = new MensajeEN();
                MensajeDTO result = new MensajeAssembler().Convert(men);
                return View(result);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // POST: /Mensaje/Create

        [HttpPost]
        public ActionResult Sugerir(MensajeEN men)
        {
            if (Session["user_email"] != null) { 
            MensajeCAD cCAD = new MensajeCAD();
            MensajeCEN cen = new MensajeCEN(cCAD);
            IList<int> aux = new List<int>();
            aux.Add(32768);
            int id = cen.CrearMensaje(men.Asunto, men.Texto, "default", MouseRidersGenNHibernate.Enumerated.MouseRiders.T_MensajeEnum.sugerencia, Convert.ToInt32(Session["user_id"].ToString()), aux);

            return RedirectToAction("Details", new { id = id });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        // GET: /Mensaje/Create

        public ActionResult Redactor()
        {if (Session["user_email"] != null && Session["user_email"].ToString() != "")
            {
            MensajeEN men = new MensajeEN();
            MensajeDTO result = new MensajeAssembler().Convert(men);
            return View(result);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // POST: /Mensaje/Create

        [HttpPost]
        public ActionResult Redactor(MensajeEN men)
        {
            if (Session["user_email"] != null) { 
            MensajeCAD cCAD = new MensajeCAD();
            MensajeCEN cen = new MensajeCEN(cCAD);
            IList<int> aux = new List<int>();
            aux.Add(32768);
            int id = cen.CrearMensaje(men.Asunto, men.Texto, "default", MouseRidersGenNHibernate.Enumerated.MouseRiders.T_MensajeEnum.redactor, Convert.ToInt32(Session["user_id"].ToString()), aux);

            return RedirectToAction("Details", new { id = id });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: /Mensaje/Create

        public ActionResult Administrador()
        {if (Session["user_email"] != null && Session["user_email"].ToString() != "")
            {
            MensajeEN men = new MensajeEN();
            MensajeDTO result = new MensajeAssembler().Convert(men);
            return View(result);
}
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // POST: /Mensaje/Create

        [HttpPost]
        public ActionResult Administrador(MensajeEN men)
        {
            if (Session["user_email"] != null) { 
            MensajeCAD cCAD = new MensajeCAD();
            MensajeCEN cen = new MensajeCEN(cCAD);
            IList<int> aux = new List<int>();
            aux.Add(32768);
            int id = cen.CrearMensaje(men.Asunto, men.Texto, "default", MouseRidersGenNHibernate.Enumerated.MouseRiders.T_MensajeEnum.admin, Convert.ToInt32(Session["user_id"].ToString()), aux);

            return RedirectToAction("Details", new { id = id });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: /Mensaje/Create

        public ActionResult Solicitar()
        {if (Session["user_email"] != null && Session["user_email"].ToString() != "")
            {
            MensajeEN men = new MensajeEN();
            MensajeDTO result = new MensajeAssembler().Convert(men);
            return View(result);
}
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // POST: /Mensaje/Create

        [HttpPost]
        public ActionResult Solicitar(MensajeEN men)
        {
            if (Session["user_email"] != null)
            { 
            MensajeCAD cCAD = new MensajeCAD();
            MensajeCEN cen = new MensajeCEN(cCAD);
            IList<int> aux = new List<int>();
            aux.Add(32768);
            int id = cen.CrearMensaje(men.Asunto, men.Texto, "default", MouseRidersGenNHibernate.Enumerated.MouseRiders.T_MensajeEnum.solicitud, Convert.ToInt32(Session["user_id"].ToString()), aux);

            return RedirectToAction("Details", new { id = id });
}
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: /Mensaje/Details/5
        public ActionResult Details(int id)
        {
            SessionInitialize();
            MensajeCAD cCAD = new MensajeCAD(session);
            MensajeEN result = cCAD.ReadOIDDefault(id);
            MensajeDTO resultfinal = new MensajeAssembler().Convert(result);
            SessionClose();
            return View(resultfinal);
        }
    }
}

