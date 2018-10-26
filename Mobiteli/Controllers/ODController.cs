using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mobiteli.Models;
using PagedList;

namespace Mobiteli.Controllers
{ 
    public class ODController : TemplateController
    {
        private EvidencijaEntities db = new EvidencijaEntities();

        //
        // GET: /OD/

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page, string rbSve)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Ime desc" : "";
            ViewBag.BrojSortParm = sortOrder == "Oznaka" ? "Oznaka desc" : "Oznaka";

            if (Request.HttpMethod == "GET")
            {
                if (currentFilter != null)
                {
                     searchString = currentFilter;
                }
            }
            else
            {
                page = 1;
            }

            var od =
                from o in db.OD.OrderBy(d => d.Oznaka).Include("OD2").Include("Zaposlenici").Include("Zaposlenici1")
                select o;

            if (!String.IsNullOrEmpty(searchString))
            {
                od = od.Where(s => s.Zaposlenici.ImePrezime.ToUpper().Contains(searchString.ToUpper())
                                        || s.Naziv.ToUpper().Contains(searchString.ToUpper())
                                       || s.Oznaka.ToUpper().Contains(searchString.ToUpper()));
            }
            ViewBag.CurrentFilter = searchString;

            switch (sortOrder)
            {
                case "Ime desc":
                    od = od.OrderByDescending(s => s.Naziv);
                    break;
                case "Oznaka":
                    od = od.OrderBy(s => s.Oznaka);
                    break;
                case "Oznaka desc":
                    od = od.OrderByDescending(s => s.Oznaka);
                    break;
                default:
                    od = od.OrderBy(s => s.Naziv);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(od.ToPagedList(pageNumber, pageSize)); 
        }

        //
        // GET: /OD/Details/5

        public ViewResult Details(int id)
        {
            OD od = db.OD.Single(o => o.id_od == id);
            return View(od);
        }

        //
        // GET: /OD/Create

        public ActionResult Create()
        {
            ViewBag.id_odnad = new SelectList(db.OD, "id_od", "Naziv");
            ViewBag.id_voditelj = new SelectList(db.Zaposlenici.OrderBy(z => z.ImePrezime), "id_zaposlenici", "ImePrezime");
            ViewBag.id_IzvSur = new SelectList(db.Zaposlenici.OrderBy(z => z.ImePrezime), "id_zaposlenici", "ImePrezime");
            return View();
        } 

        //
        // POST: /OD/Create

        [HttpPost]
        public ActionResult Create(OD od)
        {
            if (ModelState.IsValid)
            {
                db.OD.AddObject(od);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.id_odnad = new SelectList(db.OD, "id_od", "Naziv", od.id_odnad);
            ViewBag.id_voditelj = new SelectList(db.Zaposlenici.OrderBy(z => z.ImePrezime), "id_zaposlenici", "ImePrezime", od.id_voditelj);
            ViewBag.id_IzvSur = new SelectList(db.Zaposlenici.OrderBy(z => z.ImePrezime), "id_zaposlenici", "ImePrezime", od.id_IzvSur);
            return View(od);
        }
        
        //
        // GET: /OD/Edit/5
 
        public ActionResult Edit(int id)
        {
            try
            {
            OD od = db.OD.Single(o => o.id_od == id);
            var od1 = (from d in db.OD select new { d.id_od, Naziv = d.Oznaka + " " + d.Naziv }).OrderBy(d => d.Naziv);

            ViewBag.id_odnad = new SelectList(od1, "id_od", "Naziv", od.id_odnad);

            ViewBag.id_voditelj = new SelectList(db.Zaposlenici.OrderBy(z => z.ImePrezime), "id_zaposlenici", "ImePrezime", od.id_voditelj);
            ViewBag.id_IzvSur = new SelectList(db.Zaposlenici.OrderBy(z => z.ImePrezime), "id_zaposlenici", "ImePrezime", od.id_IzvSur);
            return View(od);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }

        }

        //
        // POST: /OD/Edit/5

        [HttpPost]
        public ActionResult Edit(OD od)
        {
            if (ModelState.IsValid)
            {
                db.OD.Attach(od);
                db.ObjectStateManager.ChangeObjectState(od, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_odnad = new SelectList(db.OD, "id_od", "Naziv", od.id_odnad);
            ViewBag.id_voditelj = new SelectList(db.Zaposlenici.OrderBy(z => z.ImePrezime), "id_zaposlenici", "ImePrezime", od.id_voditelj);
            ViewBag.id_IzvSur = new SelectList(db.Zaposlenici.OrderBy(z => z.ImePrezime), "id_zaposlenici", "ImePrezime", od.id_IzvSur);
            return View(od);
        }

        //
        // GET: /OD/Delete/5
 
        public ActionResult Delete(int id)
        {
            OD od = db.OD.Single(o => o.id_od == id);
            return View(od);
        }

        //
        // POST: /OD/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            OD od = db.OD.Single(o => o.id_od == id);
            db.OD.DeleteObject(od);
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