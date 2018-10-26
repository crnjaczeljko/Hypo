using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mobiteli.Models;

namespace Mobiteli.Controllers
{ 
    public class ServisController : Controller
    {
        private EvidencijaEntities db = new EvidencijaEntities();

        //
        // GET: /Servis/

        public ViewResult Index()
        {
            var servis = db.Servis.Include("Uredjaji");
            return View(servis.ToList());
        }

        //
        // GET: /Servis/Details/5

        public ViewResult Details(int id)
        {
            Servis servis = db.Servis.Single(s => s.id_srv == id);
            return View(servis);
        }

        //
        // GET: /Servis/Create

        public ActionResult Create()
        {
            var ur = (from d in db.Uredjaji select new {d.id_ur, uredjaj = d.Uredjaj + " - " + d.IMEI});
            ViewBag.id_ur = new SelectList(ur, "id_ur", "Uredjaj");
            return View();
        } 

        //
        // POST: /Servis/Create

        [HttpPost]
        public ActionResult Create(Servis servis)
        {
            if (ModelState.IsValid)
            {
                db.Servis.AddObject(servis);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.id_ur = new SelectList(db.Uredjaji, "id_ur", "Uredjaj", servis.id_ur);
            return View(servis);
        }
        
        //
        // GET: /Servis/Edit/5
 
        public ActionResult Edit(int id)
        {
            Servis servis = db.Servis.Single(s => s.id_srv == id);
            ViewBag.id_ur = new SelectList(db.Uredjaji, "id_ur", "Uredjaj", servis.id_ur);
            return View(servis);
        }

        //
        // POST: /Servis/Edit/5

        [HttpPost]
        public ActionResult Edit(Servis servis)
        {
            if (ModelState.IsValid)
            {
                db.Servis.Attach(servis);
                db.ObjectStateManager.ChangeObjectState(servis, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_ur = new SelectList(db.Uredjaji, "id_ur", "Uredjaj", servis.id_ur);
            return View(servis);
        }

        //
        // GET: /Servis/Delete/5
 
        public ActionResult Delete(int id)
        {
            Servis servis = db.Servis.Single(s => s.id_srv == id);
            return View(servis);
        }

        //
        // POST: /Servis/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Servis servis = db.Servis.Single(s => s.id_srv == id);
            db.Servis.DeleteObject(servis);
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