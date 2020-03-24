using ITLAManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageITLA.Controllers
{
    public class EstudianteController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            try{ 
                using (var db = new ManageITLAContext()) 
                {
                    return View(db.Estudiantes.ToList()); 
                }
            } 
            catch (Exception){ throw; }
        }

        [HttpGet]
        public ActionResult Agregar(){ return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(Estudiantes e)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new ManageITLAContext())
                {
                    e.FechaRegistro = DateTime.Now;
                    db.Estudiantes.Add(e);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error ", ex);
                return View();
            }
            
        }

        [HttpGet]
        public ActionResult Editar(int? id = null){

            if (id == null)
                RedirectToAction(nameof(Index));

            try
            {
                using(var db = new ManageITLAContext()) 
                {
                    var EstEnEdit = db.Estudiantes.Find(id);
                    return View(EstEnEdit);
                }
            }
            catch (Exception){throw;}

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Estudiantes e)
        {
            try
            {

                if (!ModelState.IsValid)
                    return View();

                using(var db = new ManageITLAContext())
                {
                    var eEdit = db.Estudiantes.Find(e.IDEstudiante);
                    eEdit.Nombre = e.Nombre;
                    eEdit.Apellido = e.Apellido;
                    eEdit.FechaNacimiento = e.FechaNacimiento;
                    eEdit.FechaRegistro = DateTime.Now;
                    eEdit.Sexo = e.Sexo;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception){throw;}
        }

        public ActionResult Borrar(int id)
        {
            try
            {
                using(var db = new ManageITLAContext())
                {
                    Estudiantes e = db.Estudiantes.Find(id);
                    db.Estudiantes.Remove(e);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception){throw;}
        }

        [HttpGet]
        public ActionResult AgregarMateriaEstudiante(int? id = null)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            try
            {
                ViewBag.IDEstudiante = id;
                return View();
            }
            catch (Exception) { throw; }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarMateriaEstudiante(EstudianteSeleccionAsignatura esa)
        {
            try
            {
                if (!ModelState.IsValid || esa.Cuatrimestre == null)
                    return View();

                using (var db = new ManageITLAContext())
                {
                    esa.IDEstudiante = int.Parse(Request.Form["IDEstudiante"]);
                    esa.IDAsignatura = int.Parse(Request.Form["IDAsignatura"]);

                    var dtsEsa = from e in db.EstudianteSeleccionAsignatura
                                 where esa.IDEstudiante == e.IDEstudiante
                                 select e;

                    esa.FechaSeleccion = DateTime.Now;

                    foreach (var est in dtsEsa)
                    {
                        if (est.IDEstudiante == esa.IDEstudiante && est.IDAsignatura == esa.IDAsignatura)
                        {
                            ViewBag.Msg = "Este estudiante ya tiene esta materia.";
                            return View(esa);
                        }
                    }

                    db.EstudianteSeleccionAsignatura.Add(esa);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult EliminarMateriaEstudiante(int? IDEst, int? IDAsign)
        {
            try
            {
                if (IDEst == null || IDAsign == null)
                    return RedirectToAction(nameof(Index));

                using (var bd = new ManageITLAContext())
                {

                    var idAsig = from dp in bd.EstudianteSeleccionAsignatura
                                 join a in bd.Asignaturas on dp.IDAsignatura equals a.IDAsignatura
                                 where dp.IDEstudiante == IDEst && a.IDAsignatura == IDAsign
                                 select dp;

                    bd.EstudianteSeleccionAsignatura.Remove(idAsig.First());
                    bd.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception) { throw; }
        }
    }
}