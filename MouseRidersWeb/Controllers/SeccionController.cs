﻿using MouseRidersGenNHibernate.CAD.MouseRiders;
using MouseRidersGenNHibernate.CEN.MouseRiders;
using MouseRidersGenNHibernate.EN.MouseRiders;
using MouseRidersGenNHibernate.Enumerated.MouseRiders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MouseRidersWeb.Controllers
{
    public class SeccionController : BasicController
    {
        //
        // GET: /Seccion/

        public ActionResult Index()
        {
            SessionInitialize();
            SeccionCAD cCAD = new SeccionCAD(session);
            IList<SeccionEN> result = cCAD.ReadAllDefault(0, 10);
            SessionClose();
            return View(result);
        }

        //
        // GET: /Seccion/Details/5

        public ActionResult Details(T_SeccionEnum id)
        {
            SessionInitialize();
            SeccionCAD cCAD = new SeccionCAD(session);
            SeccionEN result = cCAD.ReadOIDDefault(id);
            SessionClose();
            return View(result);
        }

        //

        //
        // GET: /Seccion/Create

        public ActionResult Create()
        {
            SeccionEN sec = new SeccionEN();
            return View(sec);
        }

        //
        // POST: /Seccion/Create

        [HttpPost]
        public ActionResult Create(SeccionEN sec)
        {
            try
            {
                SeccionCAD cCAD = new SeccionCAD();
                SeccionCEN cen = new SeccionCEN(cCAD);
                DateTime p_fecha = DateTime.Now;
                cen.CrearSeccion(sec.Seccion);

                return RedirectToAction("Details", new { id = sec.Seccion });
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Seccion/Edit/5

        public ActionResult Edit(T_SeccionEnum id)
        {
            SeccionCAD cCAD = new SeccionCAD();
            SeccionEN result = cCAD.ReadOIDDefault(id);
            return View(result);
        }

        //
        // POST: /Seccion/Edit/5

        [HttpPost]
        public ActionResult Edit(SeccionEN sec)
        {
            try
            {
                SeccionCEN cen = new SeccionCEN();
                cen.ModificarSeccion(sec.Seccion);

                return RedirectToAction("Details", new { id = sec.Seccion });
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Seccion/Delete/5

        public ActionResult Delete(T_SeccionEnum id)
        {
            SessionInitialize();
            SeccionCAD cCAD = new SeccionCAD(session);
            SeccionEN result = cCAD.ReadOIDDefault(id);
            SessionClose();
            new SeccionCEN().BorrarSeccion(id);

            return View(result);
        }

        //
        // POST: /Seccion/Delete/5

        [HttpPost]
        public ActionResult Delete(SeccionEN sec)
        {
            try
            {
                // TODO: Add delete logic here



                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
