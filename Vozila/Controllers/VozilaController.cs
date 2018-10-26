using System.Data;
using System.Linq;
using System.Web.Mvc;
using Vozila.Models;
using PagedList;
namespace Vozila.Controllers
{ 
    public class VozilaController : Controller
    {
        private readonly EvidencijaEntities _db = new EvidencijaEntities();

        //
        // GET: /Vozila/

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }

            ViewBag.CurrentFilter = searchString;
            
            var automobil =(from i in _db.Automobil select i).ToList();


            if (!string.IsNullOrWhiteSpace(searchString))
                automobil = (from r1 in automobil where 
                                 r1.Naziv.ToLower().Contains(searchString.ToLower())
                                 || r1.RegBr.ToLower().Contains(searchString.ToLower()) 
                             select r1).ToList();


            const int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(automobil.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Vozila/Details/5

        public ViewResult Details(int id)
        {
            Automobil automobil = _db.Automobil.Single(a => a.id_auto == id);
            return View(automobil);
        }

        //
        // GET: /Vozila/Create

        public ActionResult Create()
        {
            ViewBag.id_lok = new SelectList(_db.Lokacije, "id_lok", "Naziv");
            return View();
        } 

        //
        // POST: /Vozila/Create

        [HttpPost]
        public ActionResult Create(Automobil automobil)
        {
            if (ModelState.IsValid)
            {
                automobil.Servis = false;
                _db.Automobil.AddObject(automobil);
                _db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.id_lok = new SelectList(_db.Lokacije, "id_lok", "Naziv", automobil.id_lok);
            return View(automobil);
        }
        
        //
        // GET: /Vozila/Edit/5
 
        public ActionResult Edit(int id)
        {
            Automobil automobil = _db.Automobil.Single(a => a.id_auto == id);
            ViewBag.id_lok = new SelectList(_db.Lokacije, "id_lok", "Naziv", automobil.id_lok);
            return View(automobil);
        }

        //
        // POST: /Vozila/Edit/5

        [HttpPost]
        public ActionResult Edit(Automobil automobil)
        {
            if (ModelState.IsValid)
            {
                _db.Automobil.Attach(automobil);
                _db.ObjectStateManager.ChangeObjectState(automobil, EntityState.Modified);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_lok = new SelectList(_db.Lokacije, "id_lok", "Naziv", automobil.id_lok);
            return View(automobil);
        }

        //
        // GET: /Vozila/Delete/5
 
        public ActionResult Delete(int id)
        {
            Automobil automobil = _db.Automobil.Single(a => a.id_auto == id);
            return View(automobil);
        }

        //
        // POST: /Vozila/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Automobil automobil = _db.Automobil.Single(a => a.id_auto == id);
            _db.Automobil.DeleteObject(automobil);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}