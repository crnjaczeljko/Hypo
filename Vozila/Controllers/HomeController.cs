using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using PagedList;
using Vozila.Models;

namespace Vozila.Controllers
{
    public class HomeController : TemplateController
    {
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                ViewBag.Message = "Lista Rezervacija Vozila";
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
                    // ViewBag.Error = "ne postoji prijava na sistem";
                    //throw new ApplicationException("Unable to do this or that");
                    return Error("Niste prijavljeni na sistem " + wp);
                }
                OD od = (from o in Db.OD where o.id_voditelj == idzap.id_zaposlenici select o).FirstOrDefault();

                var id = (from l in Db.Lokacije where l.id_odgOsoba == idzap.id_zaposlenici select l).FirstOrDefault();

                IOrderedQueryable<Rezervacije> telefonija;

                if (id != null)
                {
                    telefonija = (from i in Db.Rezervacije
                                  where i.Lokacije.Zaposlenici.ad == wp
                                  orderby i.Zaposlenici.ImePrezime
                                  select i);
                }
                else
                {
                    if (od == null)
                    {
                        telefonija =
                            (from i in Db.Rezervacije
                             where i.Zaposlenici.ad == wp
                             orderby i.Zaposlenici.ImePrezime
                             select i);
                    }
                    else
                    {
                        telefonija =
                            (from i in Db.Rezervacije
                             where i.Zaposlenici.OD2.id_voditelj == od.id_voditelj
                             orderby i.Zaposlenici.ImePrezime, i.datum_kreiranja
                             select i);
                    }
                }

                int pageNumber = (page ?? 1);
                return View(telefonija.ToPagedList(pageNumber, PageSize));
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

       
        public ActionResult KMPage(int id)
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
                    ViewBag.id_auto = new SelectList(Db.vProvjeraAuta(rezervacije.id_polLok, rezervacije.datum_polaska,
                                                                      rezervacije.datum_dolaska,
                                                                      idzap.id_zaposlenici), "id_auto", "Lokacija",
                                                     rezervacije.id_auto);
                    ViewBag.User = idzap.ImePrezime;

                    ViewData["id_auto"] = Db.vProvjeraAuta(rezervacije.id_polLok, rezervacije.datum_polaska,
                                                           rezervacije.datum_dolaska, idzap.id_zaposlenici).ToList();
                }

                List<Zaposlenici> zap = (from z in Db.Zaposlenici where z.datum_prestanka == null select z).ToList();
                var z1 = new Zaposlenici { ImePrezime = "", id_zaposlenici = 0 };
                zap.Add(z1);
                ViewData["id_zaposlenik"] = zap.OrderBy(t => t.ImePrezime).ToList();
                ViewData["TipRezervacije"] = Db.TipRezervacije;

                ViewData["id_grad"] = Db.Mjesta;
                TempData["id_rez"] = id;

                return View(rezervacije);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        public ActionResult Edit(int id)
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
                    ViewBag.id_auto = new SelectList(Db.vProvjeraAuta(rezervacije.id_polLok, rezervacije.datum_polaska,
                                                                      rezervacije.datum_dolaska,
                                                                      idzap.id_zaposlenici), "id_auto", "Lokacija",
                                                     rezervacije.id_auto);
                    ViewBag.User = idzap.ImePrezime;

                    ViewData["id_auto"] = Db.vProvjeraAuta(rezervacije.id_polLok, rezervacije.datum_polaska,
                                                           rezervacije.datum_dolaska, idzap.id_zaposlenici).ToList();
                }
                
                List<Zaposlenici> zap = (from z in Db.Zaposlenici where z.datum_prestanka == null select z).ToList();
                var z1 = new Zaposlenici { ImePrezime = "", id_zaposlenici = 0 };
                zap.Add(z1);
                ViewData["id_zaposlenik"] = zap.OrderBy(t => t.ImePrezime).ToList();
                ViewData["TipRezervacije"] = Db.TipRezervacije;

                ViewData["id_grad"] = Db.Mjesta;
                TempData["id_rez"] = id;

                return View(rezervacije);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        [HttpPost]
        public ActionResult Edit(Rezervacije rez)
        {
            try
            {
                ViewBag.Message = "Ažuriranje rezervacije";
                int id = Convert.ToInt32(TempData["id_rez"]);
                TempData.Remove("id_rez");

                var valdata = (from r in Db.Rezervacije where r.id_rez == id select r).FirstOrDefault();

                // bool isValid = true;

                valdata.id_tiprez = GetIntVal("id_tiprez");
                valdata.relacija = EditorExtension.GetValue<string>("relacija");
                valdata.id_polLok = GetIntVal("id_lok");
               // valdata.id_zaposlenik = idzap.id_zaposlenici;
                valdata.datum_polaska = GetDatum("Pol1", "Pol2");
                valdata.datum_kreiranja = DateTime.Now;
                valdata.Status = 0;
                valdata.datum_dolaska = GetDatum("datdol1", "datdol2");
                valdata.Opis = EditorExtension.GetValue<string>("Opis");
                valdata.id_grad = GetIntVal("id_grad");
                valdata.broj_putnika = GetIntVal("BrojPutnika");
                valdata.Kontakt_Tel = EditorExtension.GetValue<string>("Kontakt_Tel");

                valdata.id_Putnik1 = GetIntVal("id_Putnik1");
                valdata.id_Putnik2 = GetIntVal("id_Putnik2");
                valdata.id_Putnik3 = GetIntVal("id_Putnik3");
                valdata.id_Putnik4 = GetIntVal("id_Putnik4");
                valdata.id_Putnik5 = GetIntVal("id_Putnik5");
                valdata.id_Putnik6 = GetIntVal("id_Putnik6");

                valdata.Ponavljanje = EditorExtension.GetValue<string>("pon") == null ? 0
                    : EditorExtension.GetValue<int>("pon");


              //  Db.Rezervacije.AddObject(valdata);
                Db.SaveChanges();

                Utils.SendEmailToOdobravateljIsp(valdata.Zaposlenici, valdata.Lokacije.Zaposlenici.email,
                                              valdata);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                ViewBag.Message = "Kreiranje nove rezervacije";

                string wp = new WindowsPrincipal((WindowsIdentity)HttpContext.User.Identity).Identity.Name;

                Zaposlenici idzap = (from z in Db.Zaposlenici where z.ad == wp select z).FirstOrDefault();

                if (idzap == null)
                {
                    // Redirect("../../Shared/Error.aspx");
                    return Error("Niste prijavljeni na sistem " + wp);
                }

                // provjera da li postoji rezervacija na ime koja nije zaključena
                var r = (from r1 in Db.Rezervacije
                         where r1.id_zaposlenik == idzap.id_zaposlenici && (r1.Status == 0 || r1.Status == 1 || r1.Status == 3)
                         select r1).ToList();

                if (r.Count > 0)
                {
                    return Error("Imate već poslatu rezervaciju, koja nije zaključena");
                }

                ViewBag.User = idzap.ImePrezime;

                ViewBag.id_auto = new SelectList(Db.Automobil, "id_auto", "Naziv");
                ViewBag.DatPol = DateTime.Now;
                ViewData["TipRezervacije"] = Db.TipRezervacije;

                ViewData["id_lok"] = Db.Lokacije;

                List<Mjesta> mj = Db.Mjesta.ToList();
                var mj1 = new Mjesta {id_mjesto = 0, Naziv = ""};
                mj.Add(mj1);
                ViewData["id_grad"] = mj.OrderBy(t => t.Naziv);

                List<Zaposlenici> zap = (from z in Db.Zaposlenici where z.datum_prestanka == null select z).ToList();
                var z1 = new Zaposlenici {ImePrezime = "", id_zaposlenici = 0};
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
        public ActionResult Create(FormCollection form)
        {
            try
            {
                string wp = new WindowsPrincipal((WindowsIdentity)HttpContext.User.Identity).Identity.Name;

                Zaposlenici idzap = (from z in Db.Zaposlenici where z.ad == wp select z).FirstOrDefault();

                if (idzap == null)
                {
                    // Redirect("../../Shared/Error.aspx");
                    return Error("Niste prijavljeni na sistem " + wp);
                }

                // bool isValid = true;

                var valdata = new Rezervacije
                    {
                        id_tiprez = GetIntVal("id_tiprez"),
                        relacija =  EditorExtension.GetValue<string>("relacija"),
                        id_polLok = GetIntVal("id_lok"),
                        id_zaposlenik = idzap.id_zaposlenici,
                        datum_polaska = GetDatum("Pol1", "Pol2"),
                        datum_kreiranja = DateTime.Now,
                        Status = 0,
                        datum_dolaska = GetDatum("datdol1", "datdol2"),
                        Opis = EditorExtension.GetValue<string>("Opis"),
                        id_grad = GetIntVal("id_grad"),
                        broj_putnika = GetIntVal("BrojPutnika"),
                        Kontakt_Tel = EditorExtension.GetValue<string>("Kontakt_Tel")
                    };

                var pt =  GetIntVal("id_Putnik1");
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

        protected int? GetIntVal1(string value)
        {
            int? retval = null;
            var vv = EditorExtension.GetValue<object>(value);
            if (vv != null)
                retval = Convert.ToInt32(vv);
            return retval;
        }

        public ViewResult OdobrenoList(string sortOrder, string currentFilter, string searchString, string datumOd, string datumDo, int? page)
        {
            try
            {       List<Rezervacije> rezervacije =
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
                     where (r.Status == 1)
                     select r).ToList();

            SrchObrada(searchString,sortOrder, currentFilter, ref datumOd, ref page, ref rezervacije);

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