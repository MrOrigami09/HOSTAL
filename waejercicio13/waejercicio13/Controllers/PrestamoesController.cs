using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using waejercicio13;

namespace waejercicio13.Controllers
{
    public class PrestamoesController : Controller
    {
        private BD_BIBLIOTECAEntities db = new BD_BIBLIOTECAEntities();

        // GET: Prestamoes
        public ActionResult Index()
        {
            var prestamo = db.Prestamo.Include(p => p.Estudiante).Include(p => p.Libro);
            return View(prestamo.ToList());
        }
        public ActionResult BuscarPorFecha(DateTime? fechaInicio, DateTime? fechaFin)
        {
            var prestamos = db.Prestamo.AsQueryable();

            if (fechaInicio.HasValue && fechaFin.HasValue)
            {
                prestamos = prestamos.Where(p => p.FechaPrestamo >= fechaInicio.Value && p.FechaPrestamo <= fechaFin.Value);
            }
            else if (fechaInicio.HasValue)
            {
                prestamos = prestamos.Where(p => p.FechaPrestamo >= fechaInicio.Value);
            }
            else if (fechaFin.HasValue)
            {
                prestamos = prestamos.Where(p => p.FechaPrestamo <= fechaFin.Value);
            }

            return View("Index", prestamos.ToList());
        }


        // GET: Prestamoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            return View(prestamo);
        }

        // GET: Prestamoes/Create
        public ActionResult Create()
        {
            ViewBag.CodAlumno = new SelectList(db.Estudiante, "CodAlumno", "Nombres");
            ViewBag.ISBN = new SelectList(db.Libro, "ISBN", "Titulo");
            return View();
        }

        // POST: Prestamoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodPrestamo,ISBN,CodAlumno,FechaPrestamo,FechaDevolucion")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                db.Prestamo.Add(prestamo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodAlumno = new SelectList(db.Estudiante, "CodAlumno", "Nombres", prestamo.CodAlumno);
            ViewBag.ISBN = new SelectList(db.Libro, "ISBN", "Titulo", prestamo.ISBN);
            return View(prestamo);
        }

        // GET: Prestamoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodAlumno = new SelectList(db.Estudiante, "CodAlumno", "Nombres", prestamo.CodAlumno);
            ViewBag.ISBN = new SelectList(db.Libro, "ISBN", "Titulo", prestamo.ISBN);
            return View(prestamo);
        }

        // POST: Prestamoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodPrestamo,ISBN,CodAlumno,FechaPrestamo,FechaDevolucion")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prestamo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodAlumno = new SelectList(db.Estudiante, "CodAlumno", "Nombres", prestamo.CodAlumno);
            ViewBag.ISBN = new SelectList(db.Libro, "ISBN", "Titulo", prestamo.ISBN);
            return View(prestamo);
        }

        // GET: Prestamoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            return View(prestamo);
        }

        // POST: Prestamoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prestamo prestamo = db.Prestamo.Find(id);
            db.Prestamo.Remove(prestamo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
