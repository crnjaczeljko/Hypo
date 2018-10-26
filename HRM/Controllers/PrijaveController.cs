using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using HRM.Models;
using PagedList;

namespace HRM.Controllers
{ 
    public class PrijaveController : TemplateController
    {
      
        //
        // GET: /Prijave/

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                ViewBag.Message = "Lista prijava";
                ViewBag.CurrentSort = sortOrder;
                var srchOd = 0;

                if (Request.HttpMethod == "GET")
                {
                    if (currentFilter != null)
                    {
                        string[] st = currentFilter.Split(',');
                        searchString = st[0];
                        srchOd = int.Parse(st[1]);
                    }
                }
                else
                    page = 1;


                if (Request.Params["id_od"] != null && Request.Params["id_od"] != "")
                {

                    srchOd = int.Parse(Request.Params["id_od"]);
                }

                ViewBag.CurrentFilter = searchString + "," + srchOd ;
         
                var prijave =
                    db.vPrijaveAplikacija.OrderByDescending(p=>p.sysdate) .ToList();


                if (!String.IsNullOrEmpty(searchString))
                {
                    prijave = prijave.Where(s => s.ImePrezime.ToUpper().Contains(searchString.ToUpper())
                                           || s.Voditelj.ToUpper().Contains(searchString.ToUpper())
                                           || s.OD.ToUpper().Contains(searchString.ToUpper())
                                           ).ToList();
                }
                if(srchOd >0)
                {
                    prijave = prijave.Where(s => s.id_od == srchOd
                                       ).ToList();
                }

                var od1 = (from d in db.OD select new { d.id_od, Naziv = d.Oznaka + " " + d.Naziv }).OrderBy(t => t.Naziv);

                ViewBag.id_od = new SelectList(od1, "id_od", "Naziv", srchOd);

                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(prijave.ToPagedList(pageNumber, pageSize));
            }
            catch(Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        //
        // GET: /Prijave/Details/5

        public ViewResult Details(int id)
        {
            Prijave prijave = db.Prijave.Single(p => p.id_prijave == id);

            var tr = (from t in db.vAplikacijeDetalj where t.id_prijave == id select t);
            ViewData["Prijave"] = tr; 
            return View(prijave);
        }

        //
        // GET: /Prijave/Create

        public ActionResult Create()
        {
            ViewBag.id_od = new SelectList(db.OD, "id_od", "Naziv");
            ViewBag.id_rm = new SelectList(db.RadnaMjesta, "id_rm", "Naziv");
            ViewBag.id_zaposlenik = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime");
            ViewBag.id_voditelj = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime");
            return View();
        } 

        //
        // POST: /Prijave/Create

        [HttpPost]
        public ActionResult Create(Prijave prijave)
        {
            if (ModelState.IsValid)
            {
                db.Prijave.AddObject(prijave);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.id_od = new SelectList(db.OD, "id_od", "Naziv", prijave.id_od);
            ViewBag.id_rm = new SelectList(db.RadnaMjesta, "id_rm", "Naziv", prijave.id_rm);
            ViewBag.id_zaposlenik = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", prijave.id_zaposlenik);
            ViewBag.id_voditelj = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", prijave.id_voditelj);
            return View(prijave);
        }
        
        //
        // GET: /Prijave/Edit/5
 
        public ActionResult Edit(int id)
        {
            Prijave prijave = db.Prijave.Single(p => p.id_prijave == id);
            ViewBag.id_od = new SelectList(db.OD, "id_od", "Naziv", prijave.id_od);
            ViewBag.id_rm = new SelectList(db.RadnaMjesta, "id_rm", "Naziv", prijave.id_rm);
            ViewBag.id_zaposlenik = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", prijave.id_zaposlenik);
            ViewBag.id_voditelj = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", prijave.id_voditelj);
            return View(prijave);
        }

        //
        // POST: /Prijave/Edit/5

        [HttpPost]
        public ActionResult Edit(Prijave prijave)
        {
            if (ModelState.IsValid)
            {
                db.Prijave.Attach(prijave);
                db.ObjectStateManager.ChangeObjectState(prijave, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_od = new SelectList(db.OD, "id_od", "Naziv", prijave.id_od);
            ViewBag.id_rm = new SelectList(db.RadnaMjesta, "id_rm", "Naziv", prijave.id_rm);
            ViewBag.id_zaposlenik = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", prijave.id_zaposlenik);
            ViewBag.id_voditelj = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", prijave.id_voditelj);
            return View(prijave);
        }

        //
        // GET: /Prijave/Delete/5
 
        public ActionResult Delete(int id)
        {
            Prijave prijave = db.Prijave.Single(p => p.id_prijave == id);
            return View(prijave);
        }

        //
        // POST: /Prijave/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Prijave prijave = db.Prijave.Single(p => p.id_prijave == id);
            db.Prijave.DeleteObject(prijave);
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