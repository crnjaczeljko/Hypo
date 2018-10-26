using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Vozila.Models;
using PagedList;

namespace Vozila.Controllers
{
    public class TemplateController : Controller
    {
        //
        // GET: /Teplate/
        internal EvidencijaEntities Db = new EvidencijaEntities();

        protected const int PageSize = 10;

        public ViewResult Error(string msg)
        {
            ViewBag.Message = msg;
            Utils.SendEmailToError(msg);
            return View("Error");
        }

        public ViewResult Details(int id)
        {
            try
            {
                Rezervacije rezervacije = Db.Rezervacije.Single(r => r.id_rez == id);
                IQueryable<Troskovi> tr = (from t in Db.Troskovi where t.id_rez == id select t);
                ViewData["TroskoviPage"] = tr;
                ViewBag.id_rez = id;

                decimal? zb = (from t1 in Db.Troskovi where t1.id_rez == id select t1).Sum(t => t.IznosKM);
                ViewBag.Zbroj = zb;

                return View(rezervacije);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }


        public ViewResult ObavijestZakljuciti(string sortOrder, string currentFilter, string searchString, string datumOd, string datumDo, int? page)
        {
            try
            {
                List<Rezervacije> rezervacije =
                    (from r in
                         Db.Rezervacije
                     orderby r.datum_kreiranja descending
                     where (r.Status == 1) && r.datum_dolaska <= DateTime.Now
                     select r).ToList();

                //if (srchOd > 0)
                //    rezervacije = (from r1 in rezervacije where r1.id_auto == srchOd select r1).ToList();
                //if (srchLok > 0)
                //    rezervacije = (from r1 in rezervacije where r1.id_polLok == srchLok select r1).ToList();

                //ViewBag.id_auto = new SelectList(Db.vAutomobili.OrderBy(o => o.Lokacija), "id_auto", "Lokacija", srchOd);
                //ViewBag.id_lok = new SelectList(Db.Lokacije.OrderBy(o => o.Naziv), "id_lok", "Naziv", srchLok);
                //SrchObrada(searchString, sortOrder, currentFilter, ref datumOd, ref page, ref rezervacije);
                foreach (var item in rezervacije)
                {
                    Utils.SendEmailToZahtjevZapisnik(item);
                }
                // int pageNumber = (page ?? 1);
                return Error("Poslate obavijesti da se zatvore rezervacije. Broj nezatvorenih rezervacija: " + rezervacije.Count);
                // return View(rezervacije.ToPagedList(pageNumber, PageSize));
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }


        public ActionResult TroskoviCreate(int id)
        {
            //if (Request.Params["btnUpdate"] == null)
            //{
            //    return View("Index");
            //}
            try
            {
                TempData["user"] = id;
                var tr = new Troskovi {id_rez = id};
                ViewBag.id_rez = id;
                ViewBag.id_val = new SelectList(Db.Valute, "id_val", "Oznaka");
                return View(tr);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        [HttpPost]
        public ActionResult TroskoviCreate(Troskovi tr)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = int.Parse(TempData["id_rez"].ToString());
                    tr.id_rez = id;

                    // izračun iznos u domaćoj valuti
                    Valute val = (from v in Db.Valute where v.id_val == tr.id_val select v).FirstOrDefault();
                    decimal par =
                        (from p in Db.vTecajna where p.sifra_valute == val.Sifra select p.koef_srednji).FirstOrDefault
                            ();
                    if (par == 0)
                        tr.IznosKM = tr.Iznos;
                    else
                    {
                        tr.IznosKM = par*tr.Iznos;
                    }

                    Db.Troskovi.AddObject(tr);
                    Db.SaveChanges();
                    TempData.Remove("user");
                    TempData.Remove("id_rez");
                    // slanje email obavijesti za odgovornu osobu u 

                    return RedirectToAction("ZapisnikPage", new {id = tr.id_rez});
                }

                return View(tr);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }


        public ActionResult TroskoviEdit(int id)
        {
            try
            {
                Troskovi tr = (from t in Db.Troskovi where t.id_tr == id select t).FirstOrDefault();

                ViewBag.id_rez = id;
                if (tr != null) ViewBag.id_val = new SelectList(Db.Valute, "id_val", "Oznaka", tr.id_val);

                decimal? zb = (from t1 in Db.Troskovi where t1.id_rez == id select t1).Sum(t => t.IznosKM);
                ViewBag.Zbroj = zb;

                return View(tr);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        [HttpPost]
        public ActionResult TroskoviEdit(Troskovi tr)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Db.Troskovi.Attach(tr);
                    Db.ObjectStateManager.ChangeObjectState(tr, EntityState.Modified);
                    Db.SaveChanges();

                    // slanje email obavijesti za odgovornu osobu u 

                    decimal? zb = (from t1 in Db.Troskovi where t1.id_rez == tr.id_rez select t1).Sum(t => t.IznosKM);
                    ViewBag.Zbroj = zb;

                    return RedirectToAction("ZapisnikPage", new {id = tr.id_rez});
                }

                return View(tr);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        public ActionResult TroskoviDelete(int id)
        {
            try
            {
                Troskovi tr = (from t in Db.Troskovi where t.id_tr == id select t).FirstOrDefault();
                if (tr != null)
                {
                    if (tr.id_rez != null)
                    {
                        int ident = tr.id_rez.Value;

                        Db.Troskovi.DeleteObject(tr);
                        Db.SaveChanges();

                        decimal? zb = (from t1 in Db.Troskovi where t1.id_rez == id select t1).Sum(t => t.IznosKM);
                        ViewBag.Zbroj = zb;

                        return RedirectToAction("ZapisnikPage", new {id = ident});
                    }
                }
                return Error("Greška pri obradi");
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        // potvrda zaduženja
        public ActionResult ZapisnikPage(int id)
        {
            try
            {
                Rezervacije rezervacije = Db.Rezervacije.Single(r => r.id_rez == id);

                if (TempData.ContainsKey("poc_km"))
                {
                    rezervacije.Poc_KM = (decimal) TempData["poc_km"];
                }
                if (TempData.ContainsKey("zav_km"))
                {
                    rezervacije.Zav_KM = (decimal) TempData["zav_km"];
                }
                IQueryable<Troskovi> tr = (from t in Db.Troskovi where t.id_rez == id select t);
                ViewData["TroskoviPage"] = tr;
                ViewBag.id_rez = id;
                TempData["id_rez"] = id;

                decimal? zb = (from t1 in Db.Troskovi where t1.id_rez == id select t1).Sum(t => t.IznosKM);
                ViewBag.Zbroj = zb;
                return View(rezervacije);
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        [HttpPost]
        public ActionResult ZapisnikPage(Rezervacije rez)
        {
            try
            {
                if (Request.Params["btnTrosak"] != null)
                {
                    object id = TempData["id_rez"];

                    TempData["id_rez"] = id;
                    TempData["poc_km"] = rez.Poc_KM;
                    TempData["zav_km"] = rez.Zav_KM;
                    var tr = new Troskovi {id_rez = (int) id};
                    ViewBag.id_rez = id; 
                    ViewBag.id_val = new SelectList(Db.Valute, "id_val", "Oznaka", tr.id_val);
                    return View("TroskoviCreate", tr);
                }

                if (ModelState.IsValid)
                {
                    var id = (int) TempData["id_rez"];
                  
                    Rezervacije rr = (from r in Db.Rezervacije where r.id_rez == id select r).FirstOrDefault();
                    if (rr != null)
                    {
                        rr.datum_polaska = GetDatum("datpol1", "datpol2");
                        rr.datum_dolaska = GetDatum("datdol1", "datdol2");
                        rr.Poc_KM = rez.Poc_KM;
                        rr.Zav_KM = rez.Zav_KM;
                        rr.Zapisnik = rez.Zapisnik;
                        rr.Status = 3;
                    }
                    Db.SaveChanges();
                    Utils.SendEmailToZahtjevZakljuciti(rr);
                    Utils.SendEmailToInfoVozila(rr);
                }
                return RedirectToAction("ZapisnikList");
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

        public ViewResult TroskoviList(int id)
        {
            List<Troskovi> tr = (from r in Db.Troskovi where r.id_rez == id select r).ToList();
            return View(tr.ToList());
        }


        public ViewResult ZapisnikList(string sortOrder, string currentFilter, string searchString, string datumOd, string datumDo, int? page)
        {
            try
            {
                string wp = new WindowsPrincipal((WindowsIdentity) HttpContext.User.Identity).Identity.Name;

                Zaposlenici idzap = (from z in Db.Zaposlenici where z.ad == wp select z).FirstOrDefault();

                if (idzap == null)
                {
                    // Redirect("../../Shared/Error.aspx");
                    return Error("Niste prijavljeni na sistem " + wp);
                }

                    List<Rezervacije> rezervacije = (from r in Db.Rezervacije
                                                     where r.Status == 1 && r.id_zaposlenik == idzap.id_zaposlenici
                                                     && r.datum_dolaska <= DateTime.Now
                                                     orderby r.datum_kreiranja descending
                                                     select r).ToList();
               if (wp.ToLower() == "hypo\\zeljkoc"
                   || wp.ToLower() == "hypo\\nadad"
                   || wp.ToLower() == "hypo\\damirs"
                   || wp.ToLower() == "hypo\\dariom"
                   || wp.ToLower() == "hypo\\igorp"
                   || wp.ToLower() == "hypo\\igorj"
                   || wp.ToLower() == "hypo\\suhretaz"
                   || wp.ToLower() == "hypo\\sonjas"
                   || wp.ToLower() == "hypo\\ilfadm"
                   || wp.ToLower() == "hypo\\merisas"
                   || wp.ToLower() == "hypo\\snjezanav"
                   )
                {
                     rezervacije = (from r in Db.Rezervacije
                                    where r.Status == 1 && r.datum_dolaska <= DateTime.Now
                                                     orderby r.datum_kreiranja descending
                                    select r).ToList();
                }

               SrchObrada(searchString, sortOrder, currentFilter, ref datumOd, ref page, ref rezervacije);
 
               int pageNumber = (page ?? 1);
               return View(rezervacije.ToPagedList(pageNumber, PageSize));

            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
         }


        protected void SrchObrada(string searchString, string sortOrder, string currentFilter, ref string datumOd, ref int? page, ref List<Rezervacije> rezervacije)
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

            rezervacije = SrchFilter(rezervacije, srchOd, srchLok, searchString);

            SetSrchData(srchOd, srchLok);
        }

        protected static List<Rezervacije> SrchFilter(List<Rezervacije> rezervacije, int srchOd, int srchLok, string searchString)
        {
            if (srchOd > 0)
                rezervacije = (from r1 in rezervacije where r1.id_auto == srchOd select r1).ToList();

            if (srchLok > 0)
                rezervacije = (from r1 in rezervacije where r1.id_polLok == srchLok select r1).ToList();

            if (!string.IsNullOrWhiteSpace(searchString))
                rezervacije = (from r1 in rezervacije where r1.Zaposlenici.ImePrezime.ToLower().Contains(searchString.ToLower()) select r1).ToList();

            return rezervacije;
        }

        protected void SetSrchData(int srchOd, int srchLok)
        {
            ViewBag.id_auto = new SelectList(Db.vAutomobili.OrderBy(o => o.Lokacija), "id_auto", "Lokacija", srchOd);
            ViewBag.id_lok = new SelectList(Db.Lokacije.OrderBy(o => o.Naziv), "id_lok", "Naziv", srchLok);
        }
        /// <summary>
        /// izračun datuma kod ponovljenje rezarvacije sa time ako je weekend da prebaci na ponedeljak
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        protected DateTime SetDateTime(DateTime dat)
        {
            // provjera da li je subota ili nedelja
            DateTime resDat = dat;
            if (resDat.DayOfWeek == DayOfWeek.Saturday)
                resDat = resDat.AddDays(2);
            if (dat.DayOfWeek == DayOfWeek.Sunday)
                resDat = resDat.AddDays(1);

            return resDat;

        }


        /// <summary>
        /// zbrajanje datuma sa vremenom
        /// </summary>
        /// <param name="dat"></param> datum
        /// <param name="time"></param> vrijeme
        /// <returns></returns>
        protected DateTime? GetDatum(string dat, string time)
        {
            var dateTime = EditorExtension.GetValue<DateTime?>(dat);
           
            string dt = dateTime.Value.ToString("dd.MM.yyyy");

            if (dateTime != null)
            {
                var datPol = Convert.ToDateTime(dt);

                var timePolaska = EditorExtension.GetValue<DateTime?>(time);

                if (timePolaska != null)
                {
                    datPol = datPol.AddHours(timePolaska.Value.Hour).AddMinutes(timePolaska.Value.Minute);
                }
                return datPol;
            }
            return null;
        }

        protected int? GetIntVal(string value)
        {
            int? retval = null;
            var vv = EditorExtension.GetValue<object>(value);
            if (vv != null)
                retval = Convert.ToInt32(vv);
            return retval;
        }

        protected void PonRezervacije(Rezervacije valdata)
        {
            if (valdata.Ponavljanje != null && valdata.Ponavljanje > 0)
            {
                int brpon = valdata.Ponavljanje.Value;

                if (valdata.datum_polaska != null)
                {
                    DateTime datPol = valdata.datum_polaska.Value;
                    if (valdata.datum_dolaska != null)
                    {
                        DateTime datDol = valdata.datum_dolaska.Value;
                        for (int i = 1; i <= brpon; i++)
                        {
                            var rez = new Rezervacije
                                {
                                    broj_putnika = valdata.broj_putnika,
                                    relacija = valdata.relacija,
                                    id_tiprez = valdata.id_tiprez,
                                    id_polLok = valdata.id_polLok,
                                    id_zaposlenik = valdata.id_zaposlenik,
                                    datum_kreiranja = DateTime.Now,
                                    Status = 0,
                                    Kontakt_Tel = valdata.Kontakt_Tel,
                                    Opis = valdata.Opis,
                                    id_grad = valdata.id_grad,
                                    id_Putnik1 = valdata.id_Putnik1,
                                    id_Putnik2 = valdata.id_Putnik2,
                                    id_Putnik3 = valdata.id_Putnik3,
                                    id_Putnik4 = valdata.id_Putnik4,
                                    id_Putnik5 = valdata.id_Putnik5,
                                    id_Putnik6 = valdata.id_Putnik6
                                };

                            datPol = SetDateTime(datPol.AddDays(1));
                            datDol = SetDateTime(datDol.AddDays(1));
                            rez.datum_polaska = datPol;
                            rez.datum_dolaska = datDol;
                          
                            Db.Rezervacije.AddObject(rez);
                            Db.SaveChanges();
                          
                            Utils.SendEmailToOdobravatelj(rez.Zaposlenici, rez.Lokacije.Zaposlenici.email,
                                                          rez);
                        }
                    }
                }
            }
        }
    }
}