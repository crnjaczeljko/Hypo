using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mobiteli.Models;
using PagedList;
using System.Linq;

namespace Mobiteli.Controllers
{

    public class ZaposleniciController : Controller
    {
        private EvidencijaEntities db = new EvidencijaEntities();

        //
        // GET: /Zaposlenici/

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page, string rbSve)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Ime desc" : "";
            ViewBag.BrojSortParm = sortOrder == "OD" ? "OD desc" : "OD";

            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;

            var zaposlenik = from z in db.Zaposlenici.Include("OD2").Include("RadnaMjesta").Include("PripadnostUBihGrupi") select z;

            if (!String.IsNullOrEmpty(searchString))
            {
                zaposlenik = zaposlenik.Where(s => s.ImePrezime.ToUpper().Contains(searchString.ToUpper())
                                       || s.OD2.Naziv.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!String.IsNullOrEmpty(rbSve))
            {
                if (rbSve == "akt")
                    zaposlenik = zaposlenik.Where(s => s.datum_prestanka == null);

                if (rbSve == "isk")
                    zaposlenik = zaposlenik.Where(s => s.datum_prestanka  != null);
            }

            switch (sortOrder)
            {
                case "Ime desc":
                    zaposlenik = zaposlenik.OrderByDescending(s => s.Ime);
                    break;
                case "OD":
                    zaposlenik = zaposlenik.OrderBy(s => s.id_od);
                    break;
                case "OD desc":
                    zaposlenik = zaposlenik.OrderByDescending(s => s.id_od);
                    break;
                default:
                    zaposlenik = zaposlenik.OrderBy(s => s.Ime);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(zaposlenik.ToPagedList(pageNumber, pageSize)); 
        }

        //
        // GET: /Zaposlenici/Details/5

        public ViewResult Details(int id)
        {
            Zaposlenici zaposlenici = db.Zaposlenici.Single(z => z.id_zaposlenici == id);
            return View(zaposlenici);
        }

        //
        // GET: /Zaposlenici/Create

        public ActionResult Create()
        {
            ViewBag.id_od = new SelectList(db.OD, "id_od", "Naziv");
            ViewBag.id_rm = new SelectList(db.RadnaMjesta, "id_rm", "Naziv");
            ViewBag.id_pripadnost = new SelectList(db.PripadnostUBihGrupi, "id_pripadnost", "Naziv");
            
            return View();
        } 

        //
        // POST: /Zaposlenici/Create

        [HttpPost]
        public ActionResult Create(Zaposlenici zaposlenici)
        {
            if (ModelState.IsValid)
            {
                db.Zaposlenici.AddObject(zaposlenici);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.id_od = new SelectList(db.OD, "id_od", "Naziv", zaposlenici.id_od);
            return View(zaposlenici);
        }
        
        //
        // GET: /Zaposlenici/Edit/5
 
        public ActionResult Edit(int id)
        {

            Zaposlenici zaposlenici = db.Zaposlenici.Single(z => z.id_zaposlenici == id);
            var od1 = (from d in db.OD select new { d.id_od, Naziv = d.Oznaka + " " + d.Naziv }).OrderBy(t=>t.Naziv);

            ViewBag.id_od = new SelectList(od1, "id_od", "Naziv", zaposlenici.id_od);
            //ViewBag.id_od = new SelectList(db.OD, "id_od", "Naziv", zaposlenici.id_od);
            ViewBag.id_rm = new SelectList(db.RadnaMjesta, "id_rm", "Naziv", zaposlenici.id_rm);

            List<tipZap> zap = new List<tipZap>();

            tipZap tz = new tipZap();
            tz.id = 1;
            tz.Naziv = "Interni";
            zap.Add(tz);

            tz = new tipZap();
            tz.id = 2;
            tz.Naziv = "Externi";
            zap.Add(tz);
            ViewBag.id_pripadnost = new SelectList(zap, "id", "Naziv", zaposlenici.id_pripadnost);
            ViewBag.tip_zaposlenja = new SelectList(db.PripadnostUBihGrupi, "id_pripadnost", "Naziv", zaposlenici.tip_zaposlenja);

            return View(zaposlenici);
        }

        //
        // POST: /Zaposlenici/Edit/5

        [HttpPost]
        public ActionResult Edit(Zaposlenici zaposlenici)
        {
            if (ModelState.IsValid)
            {
                db.Zaposlenici.Attach(zaposlenici);
                db.ObjectStateManager.ChangeObjectState(zaposlenici, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_od = new SelectList(db.OD, "id_od", "Naziv", zaposlenici.id_od);
            return View(zaposlenici);
        }

        //
        // GET: /Zaposlenici/Delete/5
 
        public ActionResult Delete(int id)
        {
            Zaposlenici zaposlenici = db.Zaposlenici.Single(z => z.id_zaposlenici == id);
            return View(zaposlenici);
        }

        //
        // POST: /Zaposlenici/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Zaposlenici zaposlenici = db.Zaposlenici.Single(z => z.id_zaposlenici == id);
            db.Zaposlenici.DeleteObject(zaposlenici);
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