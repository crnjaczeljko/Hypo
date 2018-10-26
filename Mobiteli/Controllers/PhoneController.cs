using System;
using System.Collections;
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
    public class PhoneController : Controller
    {
        private EvidencijaEntities db = new EvidencijaEntities();

        //
        // GET: /Phone/

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page, string rbSve)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Ime desc" : "";
            ViewBag.BrojSortParm = sortOrder == "Broj" ? "Broj desc" : "Broj";
            ViewBag.Title = "Zaduženje brojeva";

            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;

            ViewBag.RadioVal = 1;

            var students = from s in db.Telefonija.Include("Operateri").Include("Zaposlenici").Include("Tip_Telefona")
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Zaposlenici.ImePrezime.ToUpper().Contains(searchString.ToUpper())
                                       || s.broj_tel.ToUpper().Contains(searchString.ToUpper()));
            }

            if(rbSve == null)
                rbSve = "akt";

            if (!String.IsNullOrEmpty(rbSve))
            {
                if(rbSve == "akt")
                    students = students.Where(s => s.dat_deakt == null);
                 
                if(rbSve == "isk")
                    students = students.Where(s => s.dat_deakt != null);
            }

            switch (sortOrder)
            {
                case "Ime desc":
                    students = students.OrderByDescending(s => s.Zaposlenici.ImePrezime);
                    break;
                case "Broj":
                    students = students.OrderBy(s => s.broj_tel);
                    break;
                case "Broj desc":
                    students = students.OrderByDescending(s => s.broj_tel);
                    break;
                default:
                    students = students.OrderBy(s => s.Zaposlenici.ImePrezime);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize)); 
        }

        public ActionResult CheckVisibility(int radioValue)
        {
            return Json(new
            {
                // based on the value of the radio decide whether 
                // the textbox should be shown 
                visible = radioValue == 1
            }, JsonRequestBehavior.AllowGet);
        } 


        //
        // GET: /Phone/Details/5

        public ViewResult Details(int id)
        {
            Telefonija telefonija = db.Telefonija.Single(t => t.id_tel == id);
            return View(telefonija);
        }

        public ViewResult PotrosnjaPage()
        {
            var telefonija = db.spPotrosnja();
            return View(telefonija);
        }

        public ActionResult ReportPage(string id)
        {
            var potr = (from p in db.vPotrosnjaMjesec where p.lnk== id  orderby p.ImePrezime select p);

            return View(potr);
        }
        //
        // GET: /Phone/Create

        public ActionResult Create()
        {
            ViewBag.id_zaposlenici = new SelectList(db.Zaposlenici.OrderBy(t => t.ImePrezime), "id_zaposlenici", "ImePrezime");
            ViewBag.id_tip = new SelectList(db.Tip_Telefona, "id_tip", "Naziv");
            ViewBag.id_oper = new SelectList(db.Operateri,"id_oper","Naziv");
            return View();
        }

        public ActionResult SumarnoPage(int id)
        {
            ViewBag.id_tel = id;

            var telefonija = (from t in db.PotrosnjaSumarno where t.id_tel == id select t).ToList();
            var zb = (from t in db.PotrosnjaSumarno where t.id_tel == id select t.iznos).Sum();
            if (zb != null) ViewBag.zbroj = Math.Round((double)zb, 2).ToString("0.00");

            decimal? zb1 = (from t in telefonija select t.limit).Sum();
            ViewBag.zbrojb = Math.Round((double)zb1, 2).ToString("0.00");

            decimal? zb2 = (from t in telefonija select t.iznos_kor).Sum();
            ViewBag.zbrojk = Math.Round((double)zb2, 2).ToString("0.00");


            var kor  =
                (from tt in db.Telefonija
                 where tt.id_tel == id
                 select new { ime = tt.Zaposlenici.ImePrezime + " - " + tt.broj_tel}).FirstOrDefault();
            if (kor != null) ViewBag.Korisnik = kor.ime;
            //ViewBag.id_oper = new SelectList(db.Operateri, "id_oper", "Naziv", telefonija.id_oper);
            //ViewBag.id_zaposlenici = new SelectList(db.Zaposlenici, "id_zaposlenici", "ImePrezime", telefonija.id_zaposlenici);
            //ViewBag.id_tip = new SelectList(db.Tip_Telefona, "id_tip", "Naziv", telefonija.id_tip);
            return View(telefonija);
        }

        public ActionResult SumarnoNewPage(int id)
        {
            var telefonija = (from t in db.Telefonija where t.id_tel == id select t).FirstOrDefault();
            if (telefonija != null)
            {
                ViewBag.Korisnik = telefonija.Zaposlenici.ImePrezime;
                ViewBag.broj = telefonija.broj_tel;
                ViewBag.Limit = telefonija.Limit;
                ViewBag.id_tel = id;
                ViewBag.id_zap = telefonija.id_zaposlenici;
            }
            var god = new List<string> {"2012", "2013", "2014"};
            ViewBag.godina = new SelectList(god);

            var mj = new List<string> {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"};
            ViewBag.mjesec = new SelectList(mj); 


            //ViewBag.id_oper = new SelectList(db.Operateri, "id_oper", "Naziv", telefonija.id_oper);
            //ViewBag.id_zaposlenici = new SelectList(db.Zaposlenici, "id_zaposlenici", "ImePrezime", telefonija.id_zaposlenici);
            //ViewBag.id_tip = new SelectList(db.Tip_Telefona, "id_tip", "Naziv", telefonija.id_tip);
            return View();
        }

        public ActionResult DetaljnoPage(string id)
        {

            var telefonija = (from t in db.vEronetPotrosnja where t.idlnk == id select t).ToList();
            var zb = (from t in db.vEronetPotrosnja where t.idlnk == id select t.Iznos).Sum();
            if (zb != null) ViewBag.zbroj = Math.Round((double) zb,2).ToString("0.00");

            var kor  =
                (from tt in db.vEronetPotrosnja
                 where tt.idlnk == id
                 select tt).FirstOrDefault();

            if (kor != null) ViewBag.Korisnik = kor.MJESEC + " - " + kor.GODINA;

            //ViewBag.id_oper = new SelectList(db.Operateri, "id_oper", "Naziv", telefonija.id_oper);
            //ViewBag.id_zaposlenici = new SelectList(db.Zaposlenici, "id_zaposlenici", "ImePrezime", telefonija.id_zaposlenici);
            //ViewBag.id_tip = new SelectList(db.Tip_Telefona, "id_tip", "Naziv", telefonija.id_tip);
            return View(telefonija);
        }
        
        //
        // POST: /Phone/Create

        [HttpPost]
        public ActionResult Create(Telefonija telefonija)
        {
            if (ModelState.IsValid)
            {
                db.Telefonija.AddObject(telefonija);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            var zap = (from z in db.Zaposlenici where z.datum_prestanka == null select z);
            ViewBag.id_zaposlenici = new SelectList(zap, "id_zaposlenici", "Ime", telefonija.id_zaposlenici);
            ViewBag.id_tip = new SelectList(db.Tip_Telefona, "id_tip", "Naziv", telefonija.id_tip);
            return View(telefonija);
        }

         [HttpPost]
        public ActionResult SumarnoNewPage(PotrosnjaSumarno potr, decimal limit, int id_tel, int id_zap ,string broj_tel)
        {
            //var telefonija = (from t in db.Telefonija where t.id_tel == id select t).FirstOrDefault();
             var kor = ViewBag.Limit;
             if(ModelState.IsValid)
             {

                 // provjera da li se plaća cijeli iznos
                 var lm = (from l in db.Telefonija where l.id_tel == id_tel select l.UkupnoLimit).FirstOrDefault();
                 if(potr.broj_tel.Contains("38765") || potr.broj_tel.Contains("38766"))
                     potr.iznos = Math.Round(potr.iznos.Value *   Convert.ToDecimal( 1.17),2);

                 potr.id_tel = id_tel;
                 potr.id_zaposlenici = id_zap;
                 potr.odob_limit = limit;

                 var kor_iz = potr.iznos - potr.limit;

                 if (kor_iz < 0)
                     kor_iz = 0;

                 if (potr.iznos <= limit)
                     potr.limit = potr.iznos;

                 potr.iznos_kor = kor_iz;
                 // ako banka plaća cijeli iznos
                 if(lm)
                 {
                     potr.iznos_kor = 0;
                     potr.limit = potr.iznos;
                 }
                 
                 potr.broj_tel = broj_tel;

                 db.AddToPotrosnjaSumarno(potr);
                 db.SaveChanges();

                 return RedirectToAction("SumarnoPage", new { id = id_tel });  
             }

             var telefonija = (from t in db.Telefonija where t.id_tel == id_tel select t).FirstOrDefault();
             if (telefonija != null)
             {
                 ViewBag.Korisnik = telefonija.Zaposlenici.ImePrezime;
                 ViewBag.broj = telefonija.broj_tel;
                 ViewBag.Limit = telefonija.Limit;
                 ViewBag.id_tel = id_tel;
                 ViewBag.id_zap = telefonija.id_zaposlenici;
             }
             var god = new List<string> { "2012", "2013", "2014" };
             ViewBag.godina = new SelectList(god);

             var mj = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
             ViewBag.mjesec = new SelectList(mj); 
            return View(potr);
        }
        //
        // GET: /Phone/Edit/5
 
        public ActionResult Edit(int id)
        {
            Telefonija telefonija = db.Telefonija.Single(t => t.id_tel == id);
            ViewBag.id_zaposlenici = new SelectList(db.Zaposlenici.OrderBy(t => t.ImePrezime), "id_zaposlenici", "ImePrezime", telefonija.id_zaposlenici);
            ViewBag.id_tip = new SelectList(db.Tip_Telefona, "id_tip", "Naziv", telefonija.id_tip);
            ViewBag.id_oper = new SelectList(db.Operateri, "id_oper", "Naziv",telefonija.id_oper);
            return View(telefonija);
        }

        //
        // POST: /Phone/Edit/5

        [HttpPost]
        public ActionResult Edit(Telefonija telefonija)
        {
            if (ModelState.IsValid)
            {
                db.Telefonija.Attach(telefonija);
                db.ObjectStateManager.ChangeObjectState(telefonija, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_zaposlenici = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", telefonija.id_zaposlenici);
            ViewBag.id_tip = new SelectList(db.Tip_Telefona, "id_tip", "Naziv", telefonija.id_tip);
            return View(telefonija);
        }

        //
        // GET: /Phone/Delete/5
 
        public ActionResult Delete(int id)
        {
            Telefonija telefonija = db.Telefonija.Single(t => t.id_tel == id);
            return View(telefonija);
        }

        //
        // POST: /Phone/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Telefonija telefonija = db.Telefonija.Single(t => t.id_tel == id);
            db.Telefonija.DeleteObject(telefonija);
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