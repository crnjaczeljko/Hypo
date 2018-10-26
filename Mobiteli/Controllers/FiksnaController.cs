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
    public class FiksnaController : Controller
    {
        private EvidencijaEntities db = new EvidencijaEntities();

        //
        // GET: /Fiksna/

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Org desc" : "";
            ViewBag.BrojSortParm = sortOrder == "Broj ug" ? "Broj ug desc" : "Oznaka";


            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }

            var od = (from o in db.Fiksna.Include("OD").Include("Operateri") select o);

            if (!String.IsNullOrEmpty(searchString))
            {
                od = od.Where(s => s.OD.Naziv.ToUpper().Contains(searchString.ToUpper())
                                        || s.broj_ugovora.ToUpper().Contains(searchString.ToUpper())
                                       || s.centrala_broj.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "Org desc":
                    od = od.OrderByDescending(s => s.OD.Naziv);
                    break;
                case "Broj ug":
                    od = od.OrderBy(s => s.broj_ugovora);
                    break;
                case "Broj ug desc":
                    od = od.OrderByDescending(s => s.broj_ugovora);
                    break;
                default:
                    od = od.OrderBy(s => s.OD.Naziv);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(od.ToPagedList(pageNumber, pageSize)); 
        }

        public ActionResult SumarnoPage(int id)
        {
            var telefonija = (from t in db.PotrosnjaSumarnoFiksna where t.id_tel == id select t).ToList();

            var zb = (from t in db.PotrosnjaSumarnoFiksna where t.id_tel == id select t.iznos).Sum();
            if (zb != null) ViewBag.zbroj = Math.Round((double)zb, 2).ToString("0.00");
            ViewBag.id_tel = id;
            var kor =
                    (from tt in db.Fiksna
                     where tt.id_fiks == id
                     select new { ime = tt.OD.Naziv + " - " + tt.centrala_broj }).FirstOrDefault();
            if (kor != null) ViewBag.Korisnik = kor.ime;
            return View(telefonija);
        }

        public ActionResult SumarnoNewPage(int id)
        {
            var telefonija = (from t in db.Fiksna where t.id_fiks == id select t).FirstOrDefault();
            if (telefonija != null)
            {
                ViewBag.Korisnik = telefonija.OD.Naziv;
                ViewBag.broj = telefonija.centrala_broj;
                ViewBag.id_tel = id;
                ViewBag.id_od = telefonija.id_od;
            }
            var god = new List<string> { "2012", "2013", "2014" };
            ViewBag.godina = new SelectList(god);

            var mj = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            ViewBag.mjesec = new SelectList(mj);


            //ViewBag.id_oper = new SelectList(db.Operateri, "id_oper", "Naziv", telefonija.id_oper);
            //ViewBag.id_zaposlenici = new SelectList(db.Zaposlenici, "id_zaposlenici", "ImePrezime", telefonija.id_zaposlenici);
            //ViewBag.id_tip = new SelectList(db.Tip_Telefona, "id_tip", "Naziv", telefonija.id_tip);
            return View();
        }

        public ActionResult SumarnoEditPage(int id)
        {
            var telefonija1 = (from t in db.Fiksna where t.id_fiks == id select t).FirstOrDefault();
            if (telefonija1 != null)
            {
                ViewBag.Korisnik = telefonija1.OD.Naziv;
                ViewBag.broj = telefonija1.centrala_broj;
                ViewBag.id_tel = id;
                ViewBag.id_od = telefonija1.id_od;
            }

            var telefonija = (from t in db.PotrosnjaSumarnoFiksna where t.id_tel == id select t).FirstOrDefault();
     


            //ViewBag.id_oper = new SelectList(db.Operateri, "id_oper", "Naziv", telefonija.id_oper);
            //ViewBag.id_zaposlenici = new SelectList(db.Zaposlenici, "id_zaposlenici", "ImePrezime", telefonija.id_zaposlenici);
            //ViewBag.id_tip = new SelectList(db.Tip_Telefona, "id_tip", "Naziv", telefonija.id_tip);
            return View(telefonija);
        }

        public ViewResult PotrosnjaPage()
        {
            var telefonija = db.spFiksnoPotrosnja();
            return View(telefonija);
        }

        public ActionResult ReportPage(string id)
        {
            var potr = (from p in db.vPotrosnjaFiksnoMjesec where p.lnk == id orderby p.Naziv select p);

            return View(potr);
        }
        //
        // GET: /Fiksna/Details/5

        public ViewResult Details(int id)
        {
            Fiksna fiksna = db.Fiksna.Single(f => f.id_fiks == id);
            return View(fiksna);
        }

        //
        // GET: /Fiksna/Create

        public ActionResult Create()
        {
            ViewBag.id_od = new SelectList(db.OD, "id_od", "Naziv");
            ViewBag.id_oper = new SelectList(db.Operateri, "id_oper", "Naziv");
            return View();
        } 

        //
        // POST: /Fiksna/Create

        [HttpPost]
        public ActionResult Create(Fiksna fiksna)
        {
            if (ModelState.IsValid)
            {
                db.Fiksna.AddObject(fiksna);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.id_od = new SelectList(db.OD, "id_od", "Naziv", fiksna.id_od);
            ViewBag.id_oper = new SelectList(db.Operateri, "id_oper", "Naziv", fiksna.id_oper);
            return View(fiksna);
        }

        [HttpPost]
        public ActionResult SumarnoNewPage(PotrosnjaSumarnoFiksna potr, int id_tel, int id_od, string broj_tel)
        {
            //var telefonija = (from t in db.Telefonija where t.id_tel == id select t).FirstOrDefault();
            if (ModelState.IsValid)
            {

                // provjera da li se plaća cijeli iznos
                potr.id_tel = id_tel;
                potr.id_Od = id_od;
                potr.broj_tel = broj_tel;

                db.AddToPotrosnjaSumarnoFiksna(potr);
                db.SaveChanges();

                return RedirectToAction("SumarnoPage", new { id = id_tel });
            }

            var telefonija = (from t in db.Fiksna where t.id_fiks == id_tel select t).FirstOrDefault();
            if (telefonija != null)
            {
                ViewBag.Korisnik = telefonija.OD.Naziv;
                ViewBag.broj = telefonija.centrala_broj;
                ViewBag.id_tel = id_tel;
                ViewBag.id_od = telefonija.id_od;
            }
            var god = new List<string> { "2012", "2013", "2014" };
            ViewBag.godina = new SelectList(god);

            var mj = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            ViewBag.mjesec = new SelectList(mj);
            return View(potr);
        }

        //
        // GET: /Fiksna/Edit/5
 
        public ActionResult Edit(int id)
        {
            Fiksna fiksna = db.Fiksna.Single(f => f.id_fiks == id);
            ViewBag.id_od = new SelectList(db.OD, "id_od", "Naziv", fiksna.id_od);
            ViewBag.id_oper = new SelectList(db.Operateri, "id_oper", "Naziv", fiksna.id_oper);
            return View(fiksna);
        }

        //
        // POST: /Fiksna/Edit/5

        [HttpPost]
        public ActionResult Edit(Fiksna fiksna)
        {
            if (ModelState.IsValid)
            {
                db.Fiksna.Attach(fiksna);
                db.ObjectStateManager.ChangeObjectState(fiksna, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_od = new SelectList(db.OD, "id_od", "Naziv", fiksna.id_od);
            ViewBag.id_oper = new SelectList(db.Operateri, "id_oper", "Naziv", fiksna.id_oper);


            return View(fiksna);
        }

        [HttpPost]
        public ActionResult SumarnoEditPage(PotrosnjaSumarnoFiksna fiksna)
        {
            if (ModelState.IsValid)
            {
                var telefonija = (from t in db.PotrosnjaSumarnoFiksna where t.id_sum == fiksna.id_sum select t).FirstOrDefault();

                if (telefonija != null)
                {
                    telefonija.iznos = fiksna.iznos;
                    telefonija.preth_dug = fiksna.preth_dug;
                    telefonija.preplaceno = fiksna.preplaceno;
                    db.SaveChanges();
                }

                var telefonija1 = (from t in db.PotrosnjaSumarnoFiksna where  t.id_sum == fiksna.id_sum select t).FirstOrDefault();
                if (telefonija1 != null)
                {
                    ViewBag.Korisnik = telefonija1.OD.Naziv;
                    ViewBag.broj = telefonija1.broj_tel;
                    ViewBag.id_tel = fiksna.id_sum;
                    ViewBag.id_od = telefonija1.id_Od;
                }
                return RedirectToAction("Index");
            }
            //ViewBag.id_od = new SelectList(db.OD, "id_od", "Naziv", fiksna.id_od);
            //ViewBag.id_oper = new SelectList(db.Operateri, "id_oper", "Naziv", fiksna.id_oper);
            return View(fiksna);
        }
        //
        // GET: /Fiksna/Delete/5
 
        public ActionResult Delete(int id)
        {
            Fiksna fiksna = db.Fiksna.Single(f => f.id_fiks == id);
            return View(fiksna);
        }

        //
        // POST: /Fiksna/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Fiksna fiksna = db.Fiksna.Single(f => f.id_fiks == id);
            db.Fiksna.DeleteObject(fiksna);
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