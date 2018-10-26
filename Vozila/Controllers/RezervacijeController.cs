using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Microsoft.Reporting.WebForms;
using PagedList;
using Vozila.Models;

namespace Vozila.Controllers
{
    public class RezervacijeController : TemplateController
    {
        //
        // GET: /Rezervacije/

        public ViewResult Index( string sortOrder, string currentFilter, string searchString,string datumOd, string datumDo, int? page)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;

                var srchOd = 0;
                var srchLok = 0;

                if (Request.HttpMethod == "GET")
                {
                    if (currentFilter != null)
                    {
                        string[] st = currentFilter.Split(',');
                        datumOd = st[0];
                        srchOd = int.Parse(st[1]);
                        srchLok = int.Parse(st[2]);
                    }
                }
                else
                {
                    page = 1;
                }

                
               
                if (Request.Params["id_auto"] != null && Request.Params["id_auto"] != "")
                {

                    srchOd = int.Parse(Request.Params["id_auto"]);
                }
                if (Request.Params["id_lok"] != null && Request.Params["id_lok"] != "")
                {
                    srchLok = int.Parse(Request.Params["id_lok"]);
                }

ViewBag.CurrentFilter = datumOd + "," + srchOd + "," + srchLok;

                List<vPregledVozila> rezervacije = (from r in Db.vPregledVozila
                                                    orderby r.datum_polaska descending
                                                    where (r.Status != 4 && r.Status != 2)
                                                    select r).ToList();

                if (!string.IsNullOrWhiteSpace( datumOd) && !string.IsNullOrWhiteSpace(datumDo))
                {
                    
                    DateTime datod = Convert.ToDateTime(datumOd);
                    DateTime datdo = Convert.ToDateTime(datumDo + " 23:59:59");

                    rezervacije = (from r in rezervacije
                                   where
                                       (r.datum_polaska < datod && r.datum_dolaska > datod
                                        && r.datum_polaska < datdo && r.datum_dolaska > datdo)
                                       || (r.datum_polaska > datod && r.datum_dolaska > datod
                                           && r.datum_polaska < datdo && r.datum_dolaska < datdo)
                                       || (r.datum_polaska < datod && r.datum_dolaska > datod
                                           && r.datum_polaska < datdo && r.datum_dolaska < datdo)
                                       || (r.datum_polaska > datod && r.datum_dolaska > datod
                                           && r.datum_polaska < datdo && r.datum_dolaska > datdo)
                                   select r).ToList();
                }

                if( srchOd >0 )
                    rezervacije = (from r1 in rezervacije where r1.id_auto == srchOd select r1).ToList();
                if (srchLok > 0)
                    rezervacije = (from r1 in rezervacije where r1.id_polLok == srchLok select r1).ToList();

                if(!string.IsNullOrWhiteSpace(searchString))
                    rezervacije = (from r1 in rezervacije where r1.ImePrezime.ToLower().Contains(searchString.ToLower()) select r1).ToList();
                
                ViewBag.id_auto = new SelectList(Db.vAutomobili.OrderBy(o => o.Lokacija), "id_auto", "Lokacija", srchOd);
                ViewBag.id_lok = new SelectList(Db.Lokacije.OrderBy(o => o.Naziv), "id_lok", "Naziv", srchLok);

                int pageNumber = (page ?? 1);
                return View(rezervacije.ToPagedList(pageNumber, PageSize));
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        public ViewResult SlobodnaVozila(string sortOrder, string currentFilter, string datumOd, string datumDo,
                                         int? page)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;

                if (Request.HttpMethod == "GET")
                {
                    if (currentFilter != null)
                    {
                        string[] st = currentFilter.Split('-');
                        datumOd = st[0];
                        datumDo = st[1];
                    }
                }
                else
                {
                    page = 1;
                }

                DateTime datod = DateTime.Now.Date;
                DateTime datdo = DateTime.Now.Date.AddDays(1);

                if (datumOd != null && datumDo != null)
                {
                    datod = Convert.ToDateTime(datumOd);
                    datdo = Convert.ToDateTime(datumDo);
                }
                else
                {
                    datumOd = datod.ToString("dd.MM.yyyy");
                    datumDo = datdo.ToString("dd.MM.yyyy");
                }

                List<procSlobodnaAuta_Result> upit = Db.procSlobodnaAuta(datod, datdo, 0).ToList();

                ViewBag.CurrentFilter = datumOd + "-" + datumDo;
                ViewBag.Page = "SlobodnaVozila";

                int pageNumber = (page ?? 1);
                return View(upit.ToPagedList(pageNumber, PageSize));
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        public ActionResult ReportPage(string id)
        {
            try
            {
                //var potr = (from p in db.vPotrosnjaMjesec where p.lnk == id orderby p.ImePrezime select p);
                var rez = Db.vOdobrenoList.ToList().OrderByDescending(t => t.datum_polaska);

                var localReport = new LocalReport { ReportPath = Server.MapPath("~/Rpt/Raspored.rdlc") };
                var reportDataSource = new ReportDataSource("DataSet1", rez);
                localReport.DataSources.Add(reportDataSource);
                const string reportType = "Excel";
                string mimeType;
                string encoding;
                string fileNameExtension;

                //The DeviceInfo settings should be changed based on the reportType
                //http://msdn2.microsoft.com/en-us/library/ms155397.aspx
                const string deviceInfo = "<DeviceInfo>" +
                                          "  <OutputFormat>Excel</OutputFormat>" +
                                          "  <PageWidth>11in</PageWidth>" +
                                          "  <PageHeight>8.5in</PageHeight>" +
                                          "  <MarginTop>0.5in</MarginTop>" +
                                          "  <MarginLeft>1in</MarginLeft>" +
                                          "  <MarginRight>1in</MarginRight>" +
                                          "  <MarginBottom>0.5in</MarginBottom>" +
                                          "</DeviceInfo>";
                Warning[] warnings;
                string[] streams;

                //Render the report
                byte[] renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension,
                                                          out streams, out warnings);

                //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension);
                return File(renderedBytes, mimeType);

                //return View(rez);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        [HttpPost]
        public ActionResult ReportPage(List<vOdobrenoList> rezervacije)
        {
            try
            {
                //var potr = (from p in db.vPotrosnjaMjesec where p.lnk == id orderby p.ImePrezime select p);
                // var rez = db.vOdobrenoList.ToList();

                return View(rezervacije);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

 
        public ViewResult OdobrenoList(string sortOrder, string currentFilter, string searchString, string datumOd, string datumDo, int? page)
        {
            try
            {


                List<Rezervacije> rezervacije =
                    (from r in
                         Db.Rezervacije
                     orderby r.datum_kreiranja descending
                     where (r.Status == 1)
                     select r).ToList();

                //if (srchOd > 0)
                //    rezervacije = (from r1 in rezervacije where r1.id_auto == srchOd select r1).ToList();
                //if (srchLok > 0)
                //    rezervacije = (from r1 in rezervacije where r1.id_polLok == srchLok select r1).ToList();

                //ViewBag.id_auto = new SelectList(Db.vAutomobili.OrderBy(o => o.Lokacija), "id_auto", "Lokacija", srchOd);
                //ViewBag.id_lok = new SelectList(Db.Lokacije.OrderBy(o => o.Naziv), "id_lok", "Naziv", srchLok);
                SrchObrada(searchString, sortOrder, currentFilter, ref datumOd, ref page, ref rezervacije);
                int pageNumber = (page ?? 1);
                return View(rezervacije.ToPagedList(pageNumber, PageSize));
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        public ViewResult OdbijenoList(string sortOrder, string currentFilter, string searchString, string datumOd, string datumDo, int? page)
        {
            try
            {
                //ViewBag.CurrentSort = sortOrder;
                //if (Request.HttpMethod == "GET")
                //{
                //    searchString = currentFilter;
                //}
                //else
                //{
                //    page = 1;
                //}

                //ViewBag.CurrentFilter = searchString;

                List<Rezervacije> rezervacije =
                    (from r in
                         Db.Rezervacije.Include("Automobil")
                           .Include("Lokacije")
                           .Include("Lokacije1")
                           .Include("Lokacije2")
                           .Include("Mjesta")
                           .Include("Zaposlenici")
                           .Include("Zaposlenici1")
                           .Include("Zaposlenici2")
                           .Include("Zaposlenici3")
                           .Include("Zaposlenici4")
                           .Include("Zaposlenici5")
                           .Include("Zaposlenici6")
                           .Include("TipRezervacije")
                     orderby r.datum_kreiranja descending
                     where (r.Status == 2)
                     select r).ToList();

                SrchObrada(searchString, sortOrder, currentFilter, ref datumOd, ref page, ref rezervacije);

                int pageNumber = (page ?? 1);
                return View(rezervacije.ToPagedList(pageNumber, PageSize));
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        public ViewResult ZatvorenoList(string sortOrder, string currentFilter, string searchString, string datumOd, string datumDo, int? page)
        {
            try
            {

                List<Rezervacije> rezervacije =
                    (from r in
                         Db.Rezervacije.Include("Automobil")
                           .Include("Lokacije")
                           .Include("Lokacije1")
                           .Include("Lokacije2")
                           .Include("Mjesta")
                           .Include("Zaposlenici")
                           .Include("Zaposlenici1")
                           .Include("Zaposlenici2")
                           .Include("Zaposlenici3")
                           .Include("Zaposlenici4")
                           .Include("Zaposlenici5")
                           .Include("Zaposlenici6")
                           .Include("TipRezervacije")
                     orderby r.datum_kreiranja descending
                     where (r.Status == 4)
                     select r).ToList();

                SrchObrada(searchString, sortOrder, currentFilter, ref datumOd, ref page, ref rezervacije);

                int pageNumber = (page ?? 1);
                return View(rezervacije.ToPagedList(pageNumber, PageSize));
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        public ViewResult PregledVozila()
        {
            try
            {
                IQueryable<vRezervacija> rezervacije = (from v in Db.vRezervacija select v);
                return View(rezervacije.ToList());
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        //
        // GET: /Rezervacije/Details/5

        //
        // GET: /Rezervacije/Create

        public ActionResult Create()
        {
            try
            {
                ViewBag.Message = "Kreiranje nove rezervacije";

                ViewBag.id_auto = new SelectList(Db.Automobil, "id_auto", "Naziv");
                ViewBag.DatPol = DateTime.Now;
                ViewData["TipRezervacije"] = Db.TipRezervacije;

                ViewData["id_lok"] = Db.Lokacije;

                List<Mjesta> mj = Db.Mjesta.ToList();
                var mj1 = new Mjesta { id_mjesto = 0, Naziv = "" };
                mj.Add(mj1);
                ViewData["id_grad"] = mj.OrderBy(t => t.Naziv);

                List<Zaposlenici> zap = (from z in Db.Zaposlenici where z.datum_prestanka == null select z).ToList();
                var z1 = new Zaposlenici { ImePrezime = "", id_zaposlenici = 0 };
                zap.Add(z1);
                ViewData["id_zaposlenik"] = zap.OrderBy(t => t.ImePrezime);

                return View(new Rezervacije());
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        //
        // POST: /Rezervacije/Create

        [HttpPost]
        public ActionResult Create(Rezervacije rezervacije)
        {
            try
            {
                // bool isValid = true;
                var valdata = new Rezervacije
                    {
                        id_zaposlenik = GetIntVal("id_zaposlenik"),
                        id_tiprez = GetIntVal("id_tiprez"),
                        relacija = EditorExtension.GetValue<string>("relacija"),
                        id_polLok = GetIntVal("id_lok"),
                        broj_putnika = GetIntVal("BrojPutnika"),
                        Kontakt_Tel = EditorExtension.GetValue<string>("Kontakt_Tel"),
                        Opis = EditorExtension.GetValue<string>("Opis"),
                        id_grad = GetIntVal("id_grad"),
                        datum_kreiranja = DateTime.Now,
                        Status = 0,
                        datum_polaska = GetDatum("Pol1", "Pol2"),
                        datum_dolaska = GetDatum("datdol1", "datdol2")
                    };

                var pt = GetIntVal("id_Putnik1");
                if (pt > 0) valdata.id_Putnik1 = pt;
                pt = GetIntVal("id_Putnik2");
                if (pt > 0) valdata.id_Putnik2 = pt;
                pt = GetIntVal("id_Putnik3");
                if (pt > 0) valdata.id_Putnik3 = pt;
                pt = GetIntVal("id_Putnik4");
                if (pt > 0) valdata.id_Putnik4 = pt;
                pt = GetIntVal("id_Putnik5");
                if (pt > 0) valdata.id_Putnik5 = pt;
                pt = GetIntVal("id_Putnik6");
                if (pt > 0) valdata.id_Putnik6 = pt;

                valdata.Ponavljanje = EditorExtension.GetValue<string>("pon") == null ? 0
                    : EditorExtension.GetValue<int>("pon");

                Db.Rezervacije.AddObject(valdata);
                Db.SaveChanges();

                Utils.SendEmailToOdobravatelj(valdata.Zaposlenici, valdata.Lokacije.Zaposlenici.email,
                                              valdata);

                // ako je broj ponavljanja unešene kreiranje nove rezervacije sa danom +

                PonRezervacije(valdata);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        //
        // GET: /Rezervacije/Edit/5

        //public ActionResult Edit(int id)
        //{
        //    Rezervacije rezervacije = db.Rezervacije.Single(r => r.id_rez == id);
        //    ViewBag.id_auto = new SelectList(db.Automobil, "id_auto", "Naziv", rezervacije.id_auto);
        //    ViewBag.id_polLok = new SelectList(db.Lokacije, "id_lok", "Naziv", rezervacije.id_polLok);
        //    ViewBag.id_lok_razduzenje = new SelectList(db.Lokacije, "id_lok", "Naziv", rezervacije.id_lok_razduzenje);
        //    ViewBag.id_lok_zaduzenje = new SelectList(db.Lokacije, "id_lok", "Naziv", rezervacije.id_lok_zaduzenje);
        //    ViewBag.id_grad = new SelectList(db.Mjesta, "id_mjesto", "Naziv", rezervacije.id_grad);
        //    ViewBag.id_zaposlenik = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", rezervacije.id_zaposlenik);
        //    ViewBag.id_Putnik1 = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", rezervacije.id_Putnik1);
        //    ViewBag.id_Putnik2 = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", rezervacije.id_Putnik2);
        //    ViewBag.id_Putnik3 = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", rezervacije.id_Putnik3);
        //    ViewBag.id_Putnik4 = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", rezervacije.id_Putnik4);
        //    ViewBag.id_Putnik5 = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", rezervacije.id_Putnik5);
        //    ViewBag.id_Putnik6 = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", rezervacije.id_Putnik6);
        //    ViewBag.id_tiprez = new SelectList(db.TipRezervacije, "id_tiprez", "Naziv", rezervacije.id_tiprez);
        //    return View(rezervacije);
        //}

        //
        // POST: /Rezervacije/Edit/5

        //[HttpPost]
        //public ActionResult Edit(Rezervacije rezervacije)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Rezervacije.Attach(rezervacije);
        //        db.ObjectStateManager.ChangeObjectState(rezervacije, EntityState.Modified);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.id_auto = new SelectList(db.Automobil, "id_auto", "Naziv", rezervacije.id_auto);
        //    ViewBag.id_polLok = new SelectList(db.Lokacije, "id_lok", "Naziv", rezervacije.id_polLok);
        //    ViewBag.id_lok_razduzenje = new SelectList(db.Lokacije, "id_lok", "Naziv", rezervacije.id_lok_razduzenje);
        //    ViewBag.id_lok_zaduzenje = new SelectList(db.Lokacije, "id_lok", "Naziv", rezervacije.id_lok_zaduzenje);
        //    ViewBag.id_grad = new SelectList(db.Mjesta, "id_mjesto", "Naziv", rezervacije.id_grad);
        //    ViewBag.id_zaposlenik = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", rezervacije.id_zaposlenik);
        //    ViewBag.id_Putnik1 = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", rezervacije.id_Putnik1);
        //    ViewBag.id_Putnik2 = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", rezervacije.id_Putnik2);
        //    ViewBag.id_Putnik3 = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", rezervacije.id_Putnik3);
        //    ViewBag.id_Putnik4 = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", rezervacije.id_Putnik4);
        //    ViewBag.id_Putnik5 = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", rezervacije.id_Putnik5);
        //    ViewBag.id_Putnik6 = new SelectList(db.Zaposlenici, "id_zaposlenici", "Ime", rezervacije.id_Putnik6);
        //    ViewBag.id_tiprez = new SelectList(db.TipRezervacije, "id_tiprez", "Naziv", rezervacije.id_tiprez);
        //    return View(rezervacije);
        //}

        //
        // GET: /Rezervacije/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                Rezervacije rezervacije = Db.Rezervacije.Single(r => r.id_rez == id);
                return View(rezervacije);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        //
        // POST: /Rezervacije/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Rezervacije rezervacije = Db.Rezervacije.Single(r => r.id_rez == id);
                Db.Rezervacije.DeleteObject(rezervacije);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        protected override void Dispose(bool disposing)
        {
            Db.Dispose();
            base.Dispose(disposing);
        }

        public ViewResult PotvrdaList(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
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

                string wp = new WindowsPrincipal((WindowsIdentity)HttpContext.User.Identity).Identity.Name;

                Zaposlenici idzap = (from z in Db.Zaposlenici where z.ad == wp select z).FirstOrDefault();

                if (idzap == null)
                {
                    // Redirect("../../Shared/Error.aspx");
                    return Error("Niste prijavljeni na sistem");
                }

                List<Rezervacije> rezervacije = (from r in Db.Rezervacije
                                                 where r.Status == 0 && r.Lokacije.id_odgOsoba == idzap.id_zaposlenici
                                                 orderby r.datum_kreiranja descending
                                                 select r).ToList();

                int pageNumber = (page ?? 1);
                return View(rezervacije.ToPagedList(pageNumber, PageSize));
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        // potvrda zaduženja
        public ActionResult PotvrdaEditPage(int id)
        {
            try
            {
                Rezervacije rezervacije = (from r1 in Db.Rezervacije
                                           where r1.id_rez == id
                                           select r1).FirstOrDefault();

                //Db.Rezervacije.Single(r => r.id_rez == id);

                ViewData["id_lokacija"] = Db.Lokacije;

                string wp = new WindowsPrincipal((WindowsIdentity)HttpContext.User.Identity).Identity.Name;
                Zaposlenici idzap = (from z in Db.Zaposlenici where z.ad == wp select z).FirstOrDefault();

                if (idzap != null)
                {
                    if (rezervacije != null)
                        ViewData["id_auto"] = Db.vProvjeraAutaEdit(rezervacije.id_polLok, rezervacije.datum_polaska,
                                                                   rezervacije.datum_dolaska, idzap.id_zaposlenici, id).ToList();
                }

                List<Zaposlenici> zap = (from z in Db.Zaposlenici where z.datum_prestanka == null select z).ToList();
                var z1 = new Zaposlenici { ImePrezime = "", id_zaposlenici = 0 };
                zap.Add(z1);
                ViewData["id_zaposlenik"] = zap.OrderBy(t => t.ImePrezime).ToList(); //,"id_zaposlenici","ImePrezime");

                //zap.OrderBy(t => t.ImePrezime);

                TempData["id_rez"] = id;

                return View(rezervacije);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        [HttpPost]
        public ActionResult PotvrdaEditPage(Rezervacije rezervacije)
        {
            try
            {
                //   if (ModelState.IsValid)
                int id = Convert.ToInt32(TempData["id_rez"]);

                Rezervacije rez = (from r in Db.Rezervacije where r.id_rez == id select r).FirstOrDefault();

                if (rez == null)
                    return Error("Greška pri obradi rezervacije");

                if (rez.Status != null)
                {
                    int stat = rez.Status.Value;

                    if (Request.Params["btnUpdate"] != null)
                    {
                        SetRezerOdobreno(rezervacije, rez);
                    }
                    if (Request.Params["btnCancel"] != null)
                    {
                        rez.Status = 2;
                        rez.odobreno = false;
                        rez.datum_odobrenja = DateTime.Now;
                    }
                    if (Request.Params["btnVratiti"] != null)
                    {
                        rez.Status = 0;
                        rez.odobreno = false;
                        stat = 2;
                        rez.datum_odobrenja = null;
                    }

                    rez.Komentar = rezervacije.Komentar;
                    Db.SaveChanges();

                    InfoRezOdobrenoEdit(rez, stat);
                }
                TempData.Remove("id_rez");

                return RedirectToAction("PotvrdaList");
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        /// <summary>
        /// Slanje info o izmjenama na rezervaciji nakon odobrenja
        /// </summary>
        /// <param name="rez"></param>
        /// <param name="stat"></param>
        private static void InfoRezOdobrenoEdit(Rezervacije rez, int stat)
        {
            if (rez.odobreno && stat == 1)
            {
                Utils.SendEmailToZahtjevEditOdobreno(rez);
            }
            if (rez.odobreno == false && stat == 1)
            {
                Utils.SendEmailToZahtjevOdobijeno(rez);
            }

            if (rez.odobreno == false && stat == 2)
            {
                Utils.SendEmailToZahtjevVraceno(rez);
            }
        }

        private void SetRezerOdobreno(Rezervacije rezervacije, Rezervacije valdata)
        {
            valdata.broj_putnika = rezervacije.broj_putnika;
            var pt = GetIntVal("id_zaposlenik");
            if (pt > 0) valdata.id_zaposlenik = pt;

            pt = GetIntVal("id_Putnik1");
            if (pt > 0) valdata.id_Putnik1 = pt;
            pt = GetIntVal("id_Putnik2");
            if (pt > 0) valdata.id_Putnik2 = pt;
            pt = GetIntVal("id_Putnik3");
            if (pt > 0) valdata.id_Putnik3 = pt;
            pt = GetIntVal("id_Putnik4");
            if (pt > 0) valdata.id_Putnik4 = pt;
            pt = GetIntVal("id_Putnik5");
            if (pt > 0) valdata.id_Putnik5 = pt;
            pt = GetIntVal("id_Putnik6");
            if (pt > 0) valdata.id_Putnik6 = pt;

            pt = GetIntVal("id_auto");
            if (pt > 0) valdata.id_auto = pt;

            pt = GetIntVal("id_lok_zaduzenje");
            if (pt > 0) valdata.id_lok_zaduzenje = pt;
            pt = GetIntVal("id_lok_razduzenje");
            if (pt > 0) valdata.id_lok_razduzenje = GetIntVal("id_lok_razduzenje");

            valdata.Status = 1;
            valdata.odobreno = true;
            valdata.datum_odobrenja = DateTime.Now;
        }

        // potvrda zaduženja
        public ActionResult PotvrdaPage(int id)
        {
            try
            {
                Rezervacije rezervacije = Db.Rezervacije.Single(r => r.id_rez == id);

                if (rezervacije.Status == 1)
                {
                    return Error("Rezervacija je već odobrena");
                }

                ViewData["id_lokacija"] = Db.Lokacije;

                string wp = new WindowsPrincipal((WindowsIdentity)HttpContext.User.Identity).Identity.Name;
                Zaposlenici idzap = (from z in Db.Zaposlenici where z.ad == wp select z).FirstOrDefault();

                if (idzap != null)
                {
                    //ViewBag.id_auto = new SelectList(Db.vProvjeraAuta(rezervacije.id_polLok, rezervacije.datum_polaska,
                    //                                                  rezervacije.datum_dolaska,
                    //                                                  idzap.id_zaposlenici), "id_auto", "Lokacija",
                    //                                 rezervacije.id_auto);

                    ViewData["id_auto"] = Db.vProvjeraAuta(rezervacije.id_polLok, rezervacije.datum_polaska,
                                                           rezervacije.datum_dolaska, idzap.id_zaposlenici).ToList();
                }

                List<Zaposlenici> zap = (from z in Db.Zaposlenici where z.datum_prestanka == null select z).ToList();
                var z1 = new Zaposlenici { ImePrezime = "", id_zaposlenici = 0 };
                zap.Add(z1);
                ViewData["id_zaposlenik"] = zap.OrderBy(t => t.ImePrezime).ToList(); //,"id_zaposlenici","ImePrezime");

                //zap.OrderBy(t => t.ImePrezime);

                TempData["id_rez"] = id;

                return View(rezervacije);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        [HttpPost]
        public ActionResult PotvrdaPage(Rezervacije rezervacije)
        {
            try
            {
                //   if (ModelState.IsValid)
                int id = Convert.ToInt32(TempData["id_rez"]);
                TempData.Remove("id_rez");

                Rezervacije rez = (from r in Db.Rezervacije where r.id_rez == id select r).FirstOrDefault();
                if (rez == null)
                    return Error("Greška pri obradi rezervacije");

                if (Request.Params["btnRefresh"] != null)
                {
                    SetRezerOdobreno(rezervacije, rez);
                }

                if (rez.Status != null)
                {
                    int stat = rez.Status.Value;

                    if (Request.Params["btnUpdate"] != null)
                    {
                        SetRezerOdobreno(rezervacije, rez);
                    }

                    if (Request.Params["btnCancel"] != null)
                    {
                        rez.Status = 2;
                        rez.odobreno = false;
                        rez.datum_odobrenja = DateTime.Now;
                    }
                    if (Request.Params["btnVratiti"] != null)
                    {
                        rez.Status = 0;
                        rez.odobreno = false;
                        stat = 1;
                        rez.datum_odobrenja = null;
                    }

                    rez.Komentar = rezervacije.Komentar;
                    Db.SaveChanges();

                    SendInfoOdobreno(rez, stat);
                }

                //var rezlist =
                //    (from r in db.Rezervacije where r.Status == 0 orderby r.datum_kreiranja descending select r).ToList();

                return RedirectToAction("PotvrdaList");
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        private static void SendInfoOdobreno(Rezervacije rez, int stat)
        {
            if (rez.odobreno && stat == 0)
            {
                Utils.SendEmailToZahtjevOdobreno(rez);
            }
            if (rez.odobreno == false && stat == 0)
            {
                Utils.SendEmailToZahtjevOdobijeno(rez);
            }

            if (rez.odobreno == false && stat == 1)
            {
                Utils.SendEmailToZahtjevVraceno(rez);
            }
        }

        // potvrda zaduženja
        public ActionResult ZakljucitiPage(int id)
        {
            try
            {
                Rezervacije rezervacije = Db.Rezervacije.Single(r => r.id_rez == id);

                // provjera da li postoji rezervacija na ime koja nije zaključena
                List<Rezervacije> r2 = (from r1 in Db.Rezervacije
                                        where r1.id_rez == id && r1.Status == 4
                                        select r1).ToList();

                if (r2.Count > 0)
                {
                    return Error("Rezervacija je već zaključena");
                }

                IQueryable<Troskovi> tr = (from t in Db.Troskovi where t.id_rez == id select t);
                ViewData["TroskoviPage"] = tr;
                ViewBag.id_rez = id;
                return View(rezervacije);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        [HttpPost]
        public ActionResult ZakljucitiPage(Rezervacije rez)
        {
            try
            {
                if (Request.Params["btnUpdate"] != null && ModelState.IsValid)
                {
                    Rezervacije rr = (from r in Db.Rezervacije where r.id_rez == rez.id_rez select r).FirstOrDefault();
                    if (rr != null)
                    {
                        rr.Status = 4;
                        rr.datum_zakljucenja = DateTime.Now;
                    }
                    Db.SaveChanges();
                    Utils.SendEmailToPosiljatelj(rr);
                }
                return RedirectToAction("ZakljucitiList");
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        // potvrda zaduženja
        public ActionResult OtkazatiPage(int id)
        {
            try
            {
                Rezervacije rezervacije = Db.Rezervacije.Single(r => r.id_rez == id);

                // provjera da li postoji rezervacija na ime koja nije zaključena
                List<Rezervacije> r2 = (from r1 in Db.Rezervacije
                                        where r1.id_rez == id && r1.Status == 2
                                        select r1).ToList();

                if (r2.Count > 0)
                {
                    return Error("Rezervacija je već otkazana");
                }

                IQueryable<Troskovi> tr = (from t in Db.Troskovi where t.id_rez == id select t);
                ViewData["TroskoviPage"] = tr;
                ViewBag.id_rez = id;
                return View(rezervacije);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        [HttpPost]
        public ActionResult OtkazatiPage(Rezervacije rez)
        {
            try
            {
                if (Request.Params["btnUpdate"] != null && ModelState.IsValid)
                {
                    Rezervacije rr = (from r in Db.Rezervacije where r.id_rez == rez.id_rez select r).FirstOrDefault();
                    if (rr != null)
                    {
                        rr.Status = 2;
                        rr.datum_zakljucenja = DateTime.Now;
                    }
                    Db.SaveChanges();
                    Utils.SendEmailToZahtjevOtkazano(rr);
                }
                return RedirectToAction("OdobrenoList");
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        public ViewResult ZakljucitiList(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
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
                string wp = new WindowsPrincipal((WindowsIdentity)HttpContext.User.Identity).Identity.Name;

                Zaposlenici idzap = (from z in Db.Zaposlenici where z.ad == wp select z).FirstOrDefault();

                if (idzap == null)
                {
                    // Redirect("../../Shared/Error.aspx");
                    return Error("Niste prijavljeni na sistem");
                }

                List<Rezervacije> rezervacije = (from r in Db.Rezervacije
                                                 where r.Status == 3 && r.Lokacije.id_odgOsoba == idzap.id_zaposlenici
                                                 orderby r.datum_kreiranja descending
                                                 select r).ToList();

                //.Include("Automobil").Include("Zaposlenici").Include("Zaposlenici1").Include("Zaposlenici2").Include("Zaposlenici3").Include("Zaposlenici4")
                //.Include("Zaposlenici5").Include("Zaposlenici6").OrderByDescending(t => t.datum_kreiranja);

                int pageNumber = (page ?? 1);
                return View(rezervacije.ToPagedList(pageNumber, PageSize));
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }
    }
}