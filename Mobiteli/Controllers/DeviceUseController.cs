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
    public class DeviceUseController : TemplateController
    {
      
        //
        // GET: /DeviceUse/

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page, string rbSve)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Ime desc" : "";
            ViewBag.BrojSortParm = sortOrder == "Broj" ? "Broj desc" : "Broj";
            
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

            var students =(from s in db.Zaduzenje_Uredjaja select s).Include("Telefonija").Include("Uredjaji");

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Telefonija.Zaposlenici.ImePrezime.ToUpper().Contains(searchString.ToUpper())
                                       || s.Telefonija.broj_tel.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!String.IsNullOrEmpty(rbSve))
            {
                if (rbSve == "akt")
                    students = students.Where(s => s.datum_razd == null);

                if (rbSve == "isk")
                    students = students.Where(s => s.datum_razd != null);
            }

            switch (sortOrder)
            {
                case "Ime desc":
                    students = students.OrderByDescending(s => s.Telefonija.Zaposlenici.ImePrezime);
                    break;
                case "Broj":
                    students = students.OrderBy(s => s.Telefonija.broj_tel);
                    break;
                case "Broj desc":
                    students = students.OrderByDescending(s => s.Telefonija.broj_tel);
                    break;
                default:
                    students = students.OrderBy(s => s.Telefonija.Zaposlenici.ImePrezime);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize)); 

        }


        //
        // GET: /DeviceUse/Details/5

        public ViewResult Details(int id)
        {
            Zaduzenje_Uredjaja zaduzenje_uredjaja = db.Zaduzenje_Uredjaja.Single(z => z.id_zad == id);
            return View(zaduzenje_uredjaja);
        }

        //
        // GET: /DeviceUse/Create

        public ActionResult ReportPage(int id)
        {
            try
            {
                var zaduzenje_uredjaja = (from z in db.vZaduzenje where z.id_zad == id select z);
                return View(zaduzenje_uredjaja);
            }
            catch (Exception ex)
            {

                return Error(ex.ToString());
            }
        }

        public ActionResult Create()
        {
            ViewBag.id_tel = new SelectList(db.vAktivnoZap.OrderBy(t=>t.ImePrezime), "id_tel", "ImePrezime");
            ViewBag.id_ur = new SelectList(db.vUredjaji, "id_ur", "Uredjaj");
            return View();
        } 

        //
        // POST: /DeviceUse/Create

        [HttpPost]
        public ActionResult Create(Zaduzenje_Uredjaja zaduzenje_uredjaja)
        {
            if (ModelState.IsValid)
            {
                db.Zaduzenje_Uredjaja.AddObject(zaduzenje_uredjaja);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.id_tel = new SelectList(db.Telefonija, "id_tel", "broj_tel", zaduzenje_uredjaja.id_tel);
            ViewBag.id_ur = new SelectList(db.Uredjaji, "id_ur", "Uredjaj", zaduzenje_uredjaja.id_ur);
            return View(zaduzenje_uredjaja);
        }
        
        //
        // GET: /DeviceUse/Edit/5
 
        public ActionResult Edit(int id)
        {
            Zaduzenje_Uredjaja zaduzenje_uredjaja = db.Zaduzenje_Uredjaja.Single(z => z.id_zad == id);
            ViewBag.id_tel = new SelectList(db.vAktivnoZap.OrderBy(t => t.ImePrezime), "id_tel", "ImePrezime", zaduzenje_uredjaja.id_tel);
            ViewBag.id_ur = new SelectList(db.vUredjaji, "id_ur", "Uredjaj", zaduzenje_uredjaja.id_ur);
            return View(zaduzenje_uredjaja);
        }

        //
        // POST: /DeviceUse/Edit/5

        [HttpPost]
        public ActionResult Edit(Zaduzenje_Uredjaja zaduzenje_uredjaja)
        {
            if (ModelState.IsValid)
            {
                db.Zaduzenje_Uredjaja.Attach(zaduzenje_uredjaja);
                db.ObjectStateManager.ChangeObjectState(zaduzenje_uredjaja, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_tel = new SelectList(db.Telefonija, "id_tel", "broj_tel", zaduzenje_uredjaja.id_tel);
            ViewBag.id_ur = new SelectList(db.Uredjaji, "id_ur", "Uredjaj", zaduzenje_uredjaja.id_ur);
            return View(zaduzenje_uredjaja);
        }

        //
        // GET: /DeviceUse/Delete/5
 
        public ActionResult Delete(int id)
        {
            Zaduzenje_Uredjaja zaduzenje_uredjaja = db.Zaduzenje_Uredjaja.Single(z => z.id_zad == id);
            return View(zaduzenje_uredjaja);
        }

        //
        // POST: /DeviceUse/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Zaduzenje_Uredjaja zaduzenje_uredjaja = db.Zaduzenje_Uredjaja.Single(z => z.id_zad == id);
            db.Zaduzenje_Uredjaja.DeleteObject(zaduzenje_uredjaja);
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