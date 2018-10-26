using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vozila.Models;

namespace Vozila.Controllers
{
    public class ComboBoxController : Controller
    {
        //
        // GET: /Combo/
 internal EvidencijaEntities Db = new EvidencijaEntities();

        public ActionResult CbMjestaPartial()
        {
                   
            List<Mjesta> mj = Db.Mjesta.ToList();
            var mj1 = new Mjesta { id_mjesto = 0, Naziv = "" };
            mj.Add(mj1);
            ViewData["id_grad"] = mj.OrderBy(t => t.Naziv);
            return PartialView();
        }
        public ActionResult CbZaposlenikPartial()
        {
            ViewData["id_zaposlenik"] = GetZaposlenici();
            return PartialView();
        }

        public ActionResult CbPutnik1Partial()
        {
            ViewData["id_zaposlenik"] = GetZaposlenici();
            return PartialView();
        }
        public ActionResult CbPutnik2Partial()
        {
            ViewData["id_zaposlenik"] = GetZaposlenici();
            return PartialView();
        }
        public ActionResult CbPutnik3Partial()
        {
            ViewData["id_zaposlenik"] = GetZaposlenici();
            return PartialView();
        }
        public ActionResult CbPutnik4Partial()
        {
            ViewData["id_zaposlenik"] = GetZaposlenici();
            return PartialView();
        }
        public ActionResult CbPutnik5Partial()
        {
            ViewData["id_zaposlenik"] = GetZaposlenici();
            return PartialView();
        }
        public ActionResult CbPutnik6Partial()
        {
            ViewData["id_zaposlenik"] = GetZaposlenici();
            return PartialView();
        }
        private List<Zaposlenici> GetZaposlenici()
        {
            List<Zaposlenici> zap = (from z in Db.Zaposlenici where z.datum_prestanka == null select z).ToList();
            var z1 = new Zaposlenici {ImePrezime = "", id_zaposlenici = 0};
            zap.Add(z1);
            return zap.OrderBy(t => t.ImePrezime).ToList();
        }
    }
}
