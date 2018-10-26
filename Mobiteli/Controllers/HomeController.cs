using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using Mobiteli.Models;
using PagedList;

namespace Mobiteli.Controllers
{
    public class HomeController : TemplateController
    {
        public ActionResult Index()
        {
            try
            {

            string wp = new WindowsPrincipal((WindowsIdentity) HttpContext.User.Identity).Identity.Name;

            Zaposlenici idzap = (from z in db.Zaposlenici where z.ad == wp select z).FirstOrDefault();

            if (idzap == null)
            {
                // Redirect("../../Shared/Error.aspx");

                return Error("Niste prijavljeni na sistem");
            }
            OD od = (from o in db.OD where o.id_voditelj == idzap.id_zaposlenici select o).FirstOrDefault();

            if (od == null)
            {
                IOrderedQueryable<Telefonija> telefonija =
                    (from i in db.Telefonija.Include("Operateri").Include("Zaposlenici").Include("Tip_Telefona")
                     where i.Zaposlenici.ad == wp
                     orderby i.Zaposlenici.ImePrezime
                     select i);

                return View(telefonija.ToList());
            }
            else
            {
                ObjectResult<Telefonija> telefonija = db.spPregledTelOD(idzap.id_zaposlenici);
                return View(telefonija.ToList());
            }


            }
            catch (Exception ex)
            {

                return Error(ex.ToString());
            }
        }


        public ActionResult SimCard(int id, string sortOrder, string currentFilter, string searchString, int? page,
                                    string rbSve)
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

            IQueryable<Telefonija> students =
                (from s in db.Telefonija where s.id_zaposlenici == id select s).Include("Operateri").Include(
                    "Zaposlenici").Include("Tip_Telefona");

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Zaposlenici.ImePrezime.ToUpper().Contains(searchString.ToUpper())
                                               || s.broj_tel.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!String.IsNullOrEmpty(rbSve))
            {
                if (rbSve == "akt")
                    students = students.Where(s => s.dat_deakt == null);

                if (rbSve == "isk")
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

        public ActionResult Mobile(int id)
        {
            IQueryable<Zaduzenje_Uredjaja> zaduzenjeUredjaja =
                (from z in db.Zaduzenje_Uredjaja where z.Telefonija.id_zaposlenici == id select z).Include("Telefonija")
                    .Include("Uredjaji");

            return View(zaduzenjeUredjaja.ToList());
        }

        public ActionResult SumarnoPage(int id)
        {
            List<PotrosnjaSumarno> telefonija = (from t in db.PotrosnjaSumarno
                                                 where t.id_tel == id
                                                       && (t.mjesec != "06" && t.mjesec != "07"
                                                           && t.mjesec != "6" && t.mjesec != "7"
                                                           && t.godina == "2012")
                                                 select t).ToList();

            decimal? zb = (from t in telefonija select t.iznos).Sum();
            ViewBag.zbroj = Math.Round((double) zb, 2).ToString("0.00");

            decimal? zb1 = (from t in telefonija select t.iznos_kor).Sum();
            ViewBag.zbrojk = Math.Round((double) zb1, 2).ToString("0.00");

            var kor =
                (from tt in db.Telefonija
                 where tt.id_tel == id
                 select new {ime = tt.Zaposlenici.ImePrezime + " - " + tt.broj_tel}).FirstOrDefault();
            if (kor != null) ViewBag.Korisnik = kor.ime;

            return View(telefonija);
        }

        public ActionResult DetaljnoPage(string id)
        {
            List<vEronetPotrosnja> telefonija = (from t in db.vEronetPotrosnja where t.idlnk == id select t).ToList();
            double? zb = (from t in db.vEronetPotrosnja where t.idlnk == id select t.Iznos).Sum();
            if (zb != null) ViewBag.zbroj = Math.Round((double) zb, 2).ToString("0.00");

            return View(telefonija);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult RazduzenjeView(int id)
        {
            Telefonija telefonija = (from t in db.Telefonija where t.id_tel == id select t).FirstOrDefault();
            if (telefonija != null)
            {
                ViewBag.Korisnik = telefonija.Zaposlenici.ImePrezime;
                ViewBag.id_tel = id;
                TempData["id"] = id;
            }


            //ViewBag.id_oper = new SelectList(db.Operateri, "id_oper", "Naziv", telefonija.id_oper);
            //ViewBag.id_zaposlenici = new SelectList(db.Zaposlenici, "id_zaposlenici", "ImePrezime", telefonija.id_zaposlenici);
            //ViewBag.id_tip = new SelectList(db.Tip_Telefona, "id_tip", "Naziv", telefonija.id_tip);
            return View();
        }

        [HttpPost]
        public ActionResult RazduzenjeView(Razduzenje raz)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    raz.id_tel = int.Parse(TempData["id"].ToString());
                    db.AddToRazduzenje(raz);
                    db.SaveChanges();
                    TempData.Remove("id");

                    string wp = new WindowsPrincipal((WindowsIdentity) HttpContext.User.Identity).Identity.Name;

                    Zaposlenici idzap = (from z in db.Zaposlenici where z.ad == wp select z).FirstOrDefault();

                    if (idzap == null)
                    {
                        // Redirect("../../Shared/Error.aspx");

                        return Error("Niste prijavljeni na sistem");
                    }

                    Utils.SendEmailToAdmin(idzap, raz);
                }


                //ViewBag.id_oper = new SelectList(db.Operateri, "id_oper", "Naziv", telefonija.id_oper);
                //ViewBag.id_zaposlenici = new SelectList(db.Zaposlenici, "id_zaposlenici", "ImePrezime", telefonija.id_zaposlenici);
                //ViewBag.id_tip = new SelectList(db.Tip_Telefona, "id_tip", "Naziv", telefonija.id_tip);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }
        }

  
    }
}