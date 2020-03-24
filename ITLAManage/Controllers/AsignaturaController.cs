using ITLAManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageITLA.Controllers
{
    public class AsignaturaController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();
                using(var db = new ManageITLAContext()){ return View(db.Asignaturas.ToList()); } 
            }
            catch (Exception){ throw; }
        }

        [HttpGet]
        public ActionResult AgregarAsignatura(){ return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarAsignatura(Asignaturas a)
        {
            try
            {
                using(var bd = new ManageITLAContext())
                {
                    a.FechaRegistro = DateTime.Now;
                    bd.Asignaturas.Add(a);
                    bd.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception){ throw; }
        }

        [HttpGet]
        public ActionResult EditarAsignatura(int id)
        {
            try
            {
                using (var bd = new ManageITLAContext())
                {
                    return View(bd.Asignaturas.Find(id));
                }

            }
            catch (Exception) { throw; }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarAsignatura(Asignaturas a) 
        {
            try
            {
                using(var bd = new ManageITLAContext())
                {
                    var asignEdit = bd.Asignaturas.Find(a.IDAsignatura);
                    a.Nombre = asignEdit.Nombre;
                    bd.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }
            catch (Exception){throw;}
        }

        public ActionResult BorrarAsignatura(int id)
        {
            try
            {
                using (var bd = new ManageITLAContext())
                {
                    bd.sp_borrarAsignatura(id);
                    bd.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception){throw;}
        }
    }
}