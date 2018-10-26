using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using HRM.Models;
using PagedList;

namespace HRM.Controllers
{
    public class ZaposleniciController : TemplateController
    {
        // private EvidencijaEntities db = new EvidencijaEntities();

        //
        // GET: /Zaposlenici/

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                int srchOd = 0;

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
                {
                    page = 1;
                }

                if (Request.Params["id_od"] != null && Request.Params["id_od"] != "")
                {
                    srchOd = int.Parse(Request.Params["id_od"]);
                }

                ViewBag.CurrentFilter = searchString + "," + srchOd;

                var od1 = (from d in db.OD select new {d.id_od, Naziv = d.Oznaka + " " + d.Naziv}).OrderBy(t => t.Naziv);

                ViewBag.id_od = new SelectList(od1, "id_od", "Naziv", srchOd);


                List<Zaposlenici> zaposlenici =
                    db.Zaposlenici.Include("OD2").Include("PripadnostUBihGrupi").Include("RadnaMjesta").ToList();


                if (!String.IsNullOrEmpty(searchString))
                {
                    zaposlenici = zaposlenici.Where(s => s.ImePrezime.ToUpper().Contains(searchString.ToUpper())
                                                         ||
                                                         s.OD2.Zaposlenici.ImePrezime.ToUpper()
                                                          .Contains(searchString.ToUpper())
                                                         || s.OD2.Naziv.ToUpper().Contains(searchString.ToUpper())
                        ).ToList();
                }
                if (srchOd > 0)
                {
                    zaposlenici = zaposlenici.Where(s => s.id_od == srchOd
                        ).ToList();
                }

                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(zaposlenici.ToPagedList(pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
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
            try
            {
                var od1 = (from d in db.OD select new {d.id_od, Naziv = d.Oznaka + " " + d.Naziv}).OrderBy(t => t.Naziv);

                ViewBag.id_od = new SelectList(od1, "id_od", "Naziv");
                ViewBag.id_pripadnost = new SelectList(db.PripadnostUBihGrupi, "id_pripadnost", "naziv");
                ViewBag.id_rm = new SelectList(db.RadnaMjesta.OrderBy(r => r.Naziv), "id_rm", "Naziv");

                var zap = new List<tipZap>();

                var tz = new tipZap();
                tz.id = 1;
                tz.Naziv = "Interni";
                zap.Add(tz);

                tz = new tipZap();
                tz.id = 2;
                tz.Naziv = "Externi";
                zap.Add(tz);

                ViewBag.tip_zaposlenja = new SelectList(zap, "id", "Naziv");

                return View();
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        //
        // POST: /Zaposlenici/Create

        [HttpPost]
        public ActionResult Create(Zaposlenici zaposlenici)
        {
            try
            {
                if (Request.Params["btnUpdate"] != null)
                {
                }
                if (ModelState.IsValid)
                {
                    db.Zaposlenici.AddObject(zaposlenici);
                    db.SaveChanges();

                    // Utils.InitMailMessageOdobriAdiEmailAccount(Zaposlenici);

                    return RedirectToAction("Index");
                }

                ViewBag.id_od = new SelectList(db.OD, "id_od", "Naziv", zaposlenici.id_od);
                ViewBag.id_pripadnost = new SelectList(db.PripadnostUBihGrupi, "id_pripadnost", "naziv",
                                                       zaposlenici.id_pripadnost);
                ViewBag.id_rm = new SelectList(db.RadnaMjesta, "id_rm", "Naziv", zaposlenici.id_rm);
                return View(zaposlenici);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        //
        // GET: /Zaposlenici/Edit/5

        public ActionResult Edit(int id)
        {
            try
            {
                Zaposlenici zaposlenici = db.Zaposlenici.Single(z => z.id_zaposlenici == id);
                var od1 = (from d in db.OD select new {d.id_od, Naziv = d.Oznaka + " " + d.Naziv}).OrderBy(t => t.Naziv);

                ViewBag.id_od = new SelectList(od1, "id_od", "Naziv", zaposlenici.id_od);
                ViewBag.id_pripadnost = new SelectList(db.PripadnostUBihGrupi, "id_pripadnost", "naziv",
                                                       zaposlenici.id_pripadnost);
                ViewBag.id_rm = new SelectList(db.RadnaMjesta.OrderBy(r => r.Naziv), "id_rm", "Naziv", zaposlenici.id_rm);
                return View(zaposlenici);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        //
        // POST: /Zaposlenici/Edit/5

        [HttpPost]
        public ActionResult Edit(Zaposlenici zaposlenici)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Zaposlenici.Attach(zaposlenici);
                    db.ObjectStateManager.ChangeObjectState(zaposlenici, EntityState.Modified);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.id_od = new SelectList(db.OD, "id_od", "Naziv", zaposlenici.id_od);
                ViewBag.id_pripadnost = new SelectList(db.PripadnostUBihGrupi, "id_pripadnost", "naziv",
                                                       zaposlenici.id_pripadnost);
                ViewBag.id_rm = new SelectList(db.RadnaMjesta, "id_rm", "Naziv", zaposlenici.id_rm);
                ViewBag.tip_zap = new SelectList(db.RadnaMjesta, "id_tip", "Naziv", zaposlenici.tip_zaposlenja);

                return View(zaposlenici);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
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