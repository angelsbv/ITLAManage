using ITLAManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageITLA.Controllers
{
    public class ProfesorController : Controller
    {
        public ActionResult Index()
        {
			try
			{ using(var db = new ManageITLAContext()){ return View(db.Profesores.ToList()); } }
			catch (Exception){ throw; }
        }

        public ActionResult AgregarProfesor(){ return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarProfesor(Profesores p)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                using(var db = new ManageITLAContext())
                {
                    p.FechaRegistro = DateTime.Now;
                    DetalleProfesorAsignatura dpa = new DetalleProfesorAsignatura();
                    dpa.IDProfesor = p.IDProfesor;
                    dpa.IDAsignatura = p.IDAsignatura;
                    dpa.Cuatrimestre = p.Cuatrimestre;
                    dpa.FechaAsignacion = DateTime.Now;

                    db.DetalleProfesorAsignatura.Add(dpa);
                    db.Profesores.Add(p);
                    
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }
            catch (Exception){throw;}
        }

        [HttpGet]
        public ActionResult EditarProfesor(int id)
        {
            try{using(var db = new ManageITLAContext()){return View(db.Profesores.Find(id));}}
            catch (Exception){throw;}
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarProfesor(Profesores p)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                using(var db = new ManageITLAContext())
                {
                    var pEdit = db.Profesores.Find(p.IDProfesor);
                    pEdit.Nombre = p.Nombre;
                    pEdit.Apellido = p.Apellido;
                    pEdit.Sexo = p.Sexo;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception){ throw; }
        }

        public ActionResult BorrarProfesor(int id)
        {
            try 
            {
                if (!ModelState.IsValid)
                    return RedirectToAction("Index");
                using (var db = new ManageITLAContext()) 
                {
                    db.sp_borrarProfesor(id);
                    db.SaveChanges();
                    return RedirectToAction("Index"); 
                } 
            }
            catch (Exception) { throw; }
        }

        public ActionResult AsignaturasList()
        {
            using(var db = new ManageITLAContext()){ return PartialView(db.Asignaturas.ToList()); }
        }

        [HttpGet]
        public ActionResult AgregarMateriaProfesor(int id)
        {
            try
            {
                ViewBag.IDProfesor = id;
                return View();
            }
            catch (Exception){throw;}
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarMateriaProfesor(DetalleProfesorAsignatura dpa)
        {
            try
            {
                if (!ModelState.IsValid || dpa.Cuatrimestre == null)
                    return View();

               using(var db = new ManageITLAContext())
                {
                    Boolean todoBien = false;
                    dpa.IDProfesor = int.Parse(Request.Form["IDProfesor"]);
                    dpa.IDAsignatura = int.Parse(Request.Form["IDAsignatura"]);
                    var detailsprof = from p in db.DetalleProfesorAsignatura
                               where p.IDProfesor == dpa.IDProfesor
                               select p;
                    dpa.FechaAsignacion = DateTime.Now;

                    foreach (var prof in detailsprof) {
                        if (prof.IDProfesor == dpa.IDProfesor && prof.IDAsignatura == dpa.IDAsignatura)
                        {
                            ViewBag.Msg = "Este profesor ya tiene esta materia.";
                            todoBien = false;
                            return View(dpa);
                        }
                        todoBien = true;
                    }

                    if(todoBien)
                    {
                        db.DetalleProfesorAsignatura.Add(dpa);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult EliminarMateriaProfesor(int? IDProf, int? IDAsign, bool crct)
        {
            try
            {
                if (IDProf == null || IDAsign == null)
                    return RedirectToAction("Index");

                using(var bd = new ManageITLAContext())
                {

                    var idAsig = from dp in bd.DetalleProfesorAsignatura
                                 join a in bd.Asignaturas on dp.IDAsignatura equals a.IDAsignatura
                                 where dp.IDProfesor == IDProf && a.IDAsignatura == IDAsign
                                 select dp;

                    if (!crct)
                        bd.sp_borrarProfesor(IDProf);
                    else
                        bd.DetalleProfesorAsignatura.Remove(idAsig.First());

                    bd.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception){throw;}
        }

    }
}