using System;
using System.Linq;
using System.Web.Mvc;
using Mobiteli.Models;

namespace Mobiteli.Controllers
{
    public class TemplateController : Controller
    {
        internal EvidencijaEntities db = new EvidencijaEntities();

        public ViewResult Error(string msg)
        {
            ViewBag.Message = msg;
            return View("Error");
        }


        public ActionResult RazduzenjePotvrda(int id)
        {
            Razduzenje telefonija = (from t in db.Razduzenje.Include("Telefonija") where t.id_raz == id select t).FirstOrDefault();
            if (telefonija != null)
            {
                // ViewBag.Korisnik = telefonija.Zaposlenici.ImePrezime;
                ViewBag.id_tel = id;
                TempData["id"] = id;
            }


            //ViewBag.id_oper = new SelectList(db.Operateri, "id_oper", "Naziv", telefonija.id_oper);
            //ViewBag.id_zaposlenici = new SelectList(db.Zaposlenici, "id_zaposlenici", "ImePrezime", telefonija.id_zaposlenici);
            //ViewBag.id_tip = new SelectList(db.Tip_Telefona, "id_tip", "Naziv", telefonija.id_tip);
            return View( telefonija);
        }
        [HttpPost]
        public ActionResult RazduzenjePotvrda(Razduzenje raz)
        {
            try
            {

                if (Request.Params["btnUpdate"] != null)
                {
                    int id =  int.Parse(TempData["id"].ToString());
                    var raz1 = (from r in db.Razduzenje where r.id_raz == id select r).FirstOrDefault();

                    if (raz1 != null)
                    {
                        raz1.datum_potvrde = DateTime.Now;
                        raz1.status = true;
                        Utils.SendEmailOdobreno(raz1.Telefonija.Zaposlenici.email);
                    }
                    db.SaveChanges();
         

                }       

                return View( "Index");
            }
            catch (Exception ex)
            {
                return Error(ex.ToString());
            }


            //ViewBag.id_oper = new SelectList(db.Operateri, "id_oper", "Naziv", telefonija.id_oper);
            //ViewBag.id_zaposlenici = new SelectList(db.Zaposlenici, "id_zaposlenici", "ImePrezime", telefonija.id_zaposlenici);
     
        }
    }
}
