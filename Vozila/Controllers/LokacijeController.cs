using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Vozila.Models;

namespace Vozila.Controllers
{ 
    public class LokacijeController : Controller
    {
        private EvidencijaEntities db = new EvidencijaEntities();

        //
        // GET: /Lokacije/

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }

            ViewBag.CurrentFilter = searchString;
            var lokacije = db.Lokacije.Include("Zaposlenici").ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(lokacije.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Lokacije/Details/5

        public ViewResult Details(int id)
        {
            Lokacije lokacije = db.Lokacije.Single(l => l.id_lok == id);
            return View(lokacije);
        }

        //
        // GET: /Lokacije/Create

        public ActionResult Create()
        {
            ViewBag.id_odgOsoba = new SelectList(db.Zaposlenici.OrderBy(z=>z.ImePrezime), "id_zaposlenici", "ImePrezime");
            return View();
        } 

        //
        // POST: /Lokacije/Create

        [HttpPost]
        public ActionResult Create(Lokacije lokacije)
        {
            if (ModelState.IsValid)
            {
                db.Lokacije.AddObject(lokacije);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.id_odgOsoba = new SelectList(db.Zaposlenici.OrderBy(z => z.ImePrezime), "id_zaposlenici", "ImePrezime", lokacije.id_odgOsoba);
            return View(lokacije);
        }
        
        //
        // GET: /Lokacije/Edit/5
 
        public ActionResult Edit(int id)
        {
            Lokacije lokacije = db.Lokacije.Single(l => l.id_lok == id);
            ViewBag.id_odgOsoba = new SelectList(db.Zaposlenici.OrderBy(z => z.ImePrezime), "id_zaposlenici", "ImePrezime", lokacije.id_odgOsoba);
            ViewBag.id_nadOsoba = new SelectList(db.Zaposlenici.OrderBy(z => z.ImePrezime), "id_zaposlenici", "ImePrezime", lokacije.id_nadOsoba);
            
            return View(lokacije);
        }

        //
        // POST: /Lokacije/Edit/5

        [HttpPost]
        public ActionResult Edit(Lokacije lokacije)
        {
            if (ModelState.IsValid)
            {
                db.Lokacije.Attach(lokacije);
                db.ObjectStateManager.ChangeObjectState(lokacije, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_odgOsoba = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", lokacije.id_odgOsoba);
            return View(lokacije);
        }

        //
        // GET: /Lokacije/Delete/5
 
        public ActionResult Delete(int id)
        {
            Lokacije lokacije = db.Lokacije.Single(l => l.id_lok == id);
            return View(lokacije);
        }

        //
        // POST: /Lokacije/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Lokacije lokacije = db.Lokacije.Single(l => l.id_lok == id);
            db.Lokacije.DeleteObject(lokacije);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}