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
    public class UredjajiController : Controller
    {
        private EvidencijaEntities db = new EvidencijaEntities();

        //
        // GET: /Uredjaji/

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page, string rbSve)
        {
            //ViewBag.CurrentSort = sortOrder;
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Ime desc" : "";
            //ViewBag.BrojSortParm = sortOrder == "Broj" ? "Broj desc" : "Broj";

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

            var students =(from d in  db.Uredjaji select d).OrderBy(s=>s.id_ur) .Include("Modeli_Uredjaja").Include("Operateri");

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Modeli_Uredjaja.Naziv.ToUpper().Contains(searchString.ToUpper())
                                       || s.IMEI.ToUpper().Contains(searchString.ToUpper())
                                        || s.Operateri.Naziv.ToUpper().Contains(searchString.ToUpper())
                                       );
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize)); 
            //var uredjaji = db.Uredjaji.Include("Modeli_Uredjaja").Include("Operateri");
            //return View(uredjaji.ToList());
        }

        //
        // GET: /Uredjaji/Details/5

        public ViewResult Details(int id)
        {
            Uredjaji uredjaji = db.Uredjaji.Single(u => u.id_ur == id);
            return View(uredjaji);
        }

        //
        // GET: /Uredjaji/Create

        public ActionResult Create()
        {
            ViewBag.id_model = new SelectList(db.Modeli_Uredjaja, "id_model", "Naziv");
            ViewBag.id_oper = new SelectList(db.Operateri, "id_oper", "Naziv");
            return View();
        } 

        //
        // POST: /Uredjaji/Create

        [HttpPost]
        public ActionResult Create(Uredjaji uredjaji)
        {
            if (ModelState.IsValid)
            {
                db.Uredjaji.AddObject(uredjaji);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.id_model = new SelectList(db.Modeli_Uredjaja, "id_model", "Naziv", uredjaji.id_model);
            ViewBag.id_oper = new SelectList(db.Operateri, "id_oper", "Naziv", uredjaji.id_oper);
            return View(uredjaji);
        }
        
        //
        // GET: /Uredjaji/Edit/5
 
        public ActionResult Edit(int id)
        {
            Uredjaji uredjaji = db.Uredjaji.Single(u => u.id_ur == id);
            ViewBag.id_model = new SelectList(db.Modeli_Uredjaja, "id_model", "Naziv", uredjaji.id_model);
            ViewBag.id_oper = new SelectList(db.Operateri, "id_oper", "Naziv", uredjaji.id_oper);
            return View(uredjaji);
        }

        //
        // POST: /Uredjaji/Edit/5

        [HttpPost]
        public ActionResult Edit(Uredjaji uredjaji)
        {
            if (ModelState.IsValid)
            {
                db.Uredjaji.Attach(uredjaji);
                db.ObjectStateManager.ChangeObjectState(uredjaji, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_model = new SelectList(db.Modeli_Uredjaja, "id_model", "Naziv", uredjaji.id_model);
            ViewBag.id_oper = new SelectList(db.Operateri, "id_oper", "Naziv", uredjaji.id_oper);
            return View(uredjaji);
        }

        //
        // GET: /Uredjaji/Delete/5
 
        public ActionResult Delete(int id)
        {
            Uredjaji uredjaji = db.Uredjaji.Single(u => u.id_ur == id);
            return View(uredjaji);
        }

        //
        // POST: /Uredjaji/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Uredjaji uredjaji = db.Uredjaji.Single(u => u.id_ur == id);
            db.Uredjaji.DeleteObject(uredjaji);
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