using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using waSemana13;

namespace waSemana13.Controllers
{
    public class MedicosController : Controller
    {
        private TClinicaEntities db = new TClinicaEntities();

        // GET: Medicos
        public ActionResult Index()
        {
            var medico = db.Medico.Include(m => m.Especialidad).Include(m => m.Pais);
            return View(medico.ToList());
        }

        //metodo buscar:

        [HttpPost]
        public ActionResult Index(string papellido)
        {
            var buscar = from lista in db.Medico
                         where lista.Apellido == papellido
                         select lista;
            return View(buscar);
        }

        // GET: Medicos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Medico.Find(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // GET: Medicos/Create
        public ActionResult Create()
        {
            ViewBag.IdEspecialidad = new SelectList(db.Especialidad, "IdEspecialidad", "Nombre");
            ViewBag.IdPais = new SelectList(db.Pais, "IdPais", "Nombre");
            return View();
        }

        // POST: Medicos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMedico,Nombre,Apellido,NumeroColegiatura,DNI,Genero,IdEspecialidad,IdPais")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                db.Medico.Add(medico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEspecialidad = new SelectList(db.Especialidad, "IdEspecialidad", "Nombre", medico.IdEspecialidad);
            ViewBag.IdPais = new SelectList(db.Pais, "IdPais", "Nombre", medico.IdPais);
            return View(medico);
        }

        // GET: Medicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Medico.Find(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEspecialidad = new SelectList(db.Especialidad, "IdEspecialidad", "Nombre", medico.IdEspecialidad);
            ViewBag.IdPais = new SelectList(db.Pais, "IdPais", "Nombre", medico.IdPais);
            return View(medico);
        }

        // POST: Medicos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMedico,Nombre,Apellido,NumeroColegiatura,DNI,Genero,IdEspecialidad,IdPais")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEspecialidad = new SelectList(db.Especialidad, "IdEspecialidad", "Nombre", medico.IdEspecialidad);
            ViewBag.IdPais = new SelectList(db.Pais, "IdPais", "Nombre", medico.IdPais);
            return View(medico);
        }

        // GET: Medicos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Medico.Find(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medico medico = db.Medico.Find(id);
            db.Medico.Remove(medico);
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
