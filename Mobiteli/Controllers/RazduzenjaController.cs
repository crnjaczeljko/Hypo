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
    public class RazduzenjaController : TemplateController
    {
    
        //
        // GET: /Razduzenja/

        public ViewResult Index()
        {
            var razduzenje = db.Razduzenje.Include("Telefonija");
            return View(razduzenje.ToList());
        }

        //
        // GET: /Razduzenja/Details/5

  

        //
        // POST: /Razduzenja/Edit/5

        [HttpPost]
        public ActionResult Edit(Razduzenje razduzenje)
        {
            if (ModelState.IsValid)
            {
                db.Razduzenje.Attach(razduzenje);
                db.ObjectStateManager.ChangeObjectState(razduzenje, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_tel = new SelectList(db.Telefonija, "id_tel", "broj_tel", razduzenje.id_tel);
            return View(razduzenje);
        }

        //
        // GET: /Razduzenja/Delete/5

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}