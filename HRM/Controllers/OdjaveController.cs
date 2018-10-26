using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Models;

namespace HRM.Controllers
{ 
    public class OdjaveController : Controller
    {
        private EvidencijaEntities db = new EvidencijaEntities();

        //
        // GET: /Odjave/

        public ViewResult Index()
        {
            var odjave = db.Odjave.Include("OD").Include("RadnaMjesta").Include("Zaposlenici").Include("Zaposlenici1").OrderByDescending(t => t.datum_iniciranja);
            return View(odjave.ToList());
        }

        //
        // GET: /Odjave/Details/5

        public ViewResult Details(int id)
        {
            Odjave odjave = db.Odjave.Single(o => o.OdjaveId == id);
            return View(odjave);
        }

        //
        // GET: /Odjave/Create

        public ActionResult Create()
        {
            ViewBag.id_od = new SelectList(db.OD, "id_od", "Naziv");
            ViewBag.id_rm = new SelectList(db.RadnaMjesta, "id_rm", "Naziv");
            ViewBag.id_voditelj = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime");
            ViewBag.id_zaposlenik = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime");
            return View();
        } 

        //
        // POST: /Odjave/Create

        [HttpPost]
        public ActionResult Create(Odjave odjave)
        {
            if (ModelState.IsValid)
            {
                db.Odjave.AddObject(odjave);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.id_od = new SelectList(db.OD, "id_od", "Naziv", odjave.id_od);
            ViewBag.id_rm = new SelectList(db.RadnaMjesta, "id_rm", "Naziv", odjave.id_rm);
            ViewBag.id_voditelj = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", odjave.id_voditelj);
            ViewBag.id_zaposlenik = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", odjave.id_zaposlenik);
            return View(odjave);
        }
        
        //
        // GET: /Odjave/Edit/5
 
        public ActionResult Edit(int id)
        {
            Odjave odjave = db.Odjave.Single(o => o.OdjaveId == id);
            ViewBag.id_od = new SelectList(db.OD, "id_od", "Naziv", odjave.id_od);
            ViewBag.id_rm = new SelectList(db.RadnaMjesta, "id_rm", "Naziv", odjave.id_rm);
            ViewBag.id_voditelj = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", odjave.id_voditelj);
            ViewBag.id_zaposlenik = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", odjave.id_zaposlenik);
            return View(odjave);
        }

        //
        // POST: /Odjave/Edit/5

        [HttpPost]
        public ActionResult Edit(Odjave odjave)
        {
            if (ModelState.IsValid)
            {
                db.Odjave.Attach(odjave);
                db.ObjectStateManager.ChangeObjectState(odjave, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_od = new SelectList(db.OD, "id_od", "Naziv", odjave.id_od);
            ViewBag.id_rm = new SelectList(db.RadnaMjesta, "id_rm", "Naziv", odjave.id_rm);
            ViewBag.id_voditelj = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", odjave.id_voditelj);
            ViewBag.id_zaposlenik = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", odjave.id_zaposlenik);
            return View(odjave);
        }

        //
        // GET: /Odjave/Delete/5
 
        public ActionResult Delete(int id)
        {
            Odjave odjave = db.Odjave.Single(o => o.OdjaveId == id);
            return View(odjave);
        }

        //
        // POST: /Odjave/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Odjave odjave = db.Odjave.Single(o => o.OdjaveId == id);
            db.Odjave.DeleteObject(odjave);
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