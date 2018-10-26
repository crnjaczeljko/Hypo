using System;
using System.Net.Mail;
using System.Text;
using System.Web;
using Mobiteli.Models;

namespace Mobiteli.Controllers
{
    public class Utils
    {
        private static readonly SmtpClient _smtpClient = new SmtpClient("A28000EE");
        private static readonly string Br = "<br/>";
        private static readonly string td = "<td>";
    private static readonly string tdn = "</td>";

        public static void SendEMail(MailMessage mailMessage, bool async = false)
        {
            if (true)
            {
                if (async)
                {
                    _smtpClient.SendAsync(mailMessage, null);
                }
                else
                {
                    _smtpClient.Send(mailMessage);
                }
            }
        }

        public static MailMessage InitMailMessage(string from, string to, string subject, string body)
        {
            // to = "administrator.hypoba@hypo-alpe-adria.com";
            var mm = new MailMessage(from, to, subject.Replace('\r', ' ').Replace('\n', ' '), body) { IsBodyHtml = true };
            
            return mm;
        }


        public static string FullyQualifiedApplicationPath(string page)
        {
            var context = HttpContext.Current;

            if (context == null) return null;

            var appPath = string.Format("{0}://{1}{2}{3}",
                                        context.Request.Url.Scheme,
                                        context.Request.Url.Host,
                                        context.Request.Url.Port == 80
                                            ? string.Empty
                                            : ":" + context.Request.Url.Port,
                                        context.Request.ApplicationPath);
            if (!appPath.EndsWith("/"))
                appPath += "/";

            return appPath + page;
        }
        
        private static string GetAnchor(string href, string friendlyName)
        {
            return "<a href=\"" + href + "\">" + friendlyName + "</a>";

            //return "<a href=\"javascript:void(0);\" onClick=\"window.open('" + href + "', '', " +
            //       "'fullscreen=yes, scrollbars=no, resize=no, toolbar=no, status=no, location=no, menubar=no');\">" + friendlyName + "</a>";
        }

        public static void SendEmailToAdmin( Zaposlenici zap,  Razduzenje rez)
        {
 
            string subject = string.Format("Odobriti isključenje mobitela za djelatnika {0} {1}", zap.Ime, zap.Prezime);
            string s = "Potrebno je odobriti isključenje mobitela za.";
            s += GetZaposlenikInfo(zap) + "<br> Opis :<br>" + rez.opis;
            s += "<br/>" + GetAnchor(FullyQualifiedApplicationPath("Home/RazduzenjePotvrda/" + rez.id_raz), "Kliknite ovde");
            string body = string.Format("<p style=\"font-family:arial;font-size:16px\">{0}</p>", s);
            var mm = InitMailMessage(zap.email, "PrijavaMobitel@hypo.ba", subject, body);
            SendEMail(mm);
         
        }

        public static void SendEmailOdobreno(string email)
        {

            string subject = "Info - zahtjev za isključenje mobitela";
            string s = "Po Vašem zahtjevu mobitel je isključen";
            var mm = InitMailMessage("PrijavaMobitel@hypo.ba", email, subject, s);
            SendEMail(mm);

        }
        //public static void SendEmailToZahtjevZakljuciti(Rezervacije rez)
        //{
        //    string subject = string.Format("Rezervaciju za Automobil potrebno je zakljuciti ");
        //    string s = "Rezervaciju za Automobil potrebno je zakljuciti";
        //    s += GetRezInfo(rez) + GetRezInfoZakljuciti(rez);
        //    s += "<br/>" + GetAnchor(FullyQualifiedApplicationPath("Rezervacije/ZakljucitiPage/" + rez.id_rez), "Kliknite ovde za zapisnik sa putovanja");
        //    string body = string.Format("<p style=\"font-family:arial;font-size:16px\">{0}</p>", s);
        //    var mm = InitMailMessage(rez.Zaposlenici.email,rez.Lokacije.Zaposlenici.email , subject, body);
        //    SendEMail(mm);
        //}        
        
        //public static void SendEmailToPosiljatelj(Rezervacije rez)
        //{
        //    string subject = string.Format("Rezervaciju za Automobil je zaključena ");
        //    string s = "Rezervaciju za Automobil je zaključena ";
        //    s += GetRezInfo(rez) + GetRezInfoZakljuciti(rez);
        //   // s += "<br/>" + GetAnchor(FullyQualifiedApplicationPath("Rezervacije/ZapisnikPage/" + rez.id_rez), "Kliknite ovde za zapisnik sa putovanja");
        //    string body = string.Format("<p style=\"font-family:arial;font-size:16px\">{0}</p>", s);
        //    var mm = InitMailMessage(rez.Lokacije.Zaposlenici.email,rez.Zaposlenici.email , subject, body);
        //    SendEMail(mm);
        //}

        //public static void SendEmailToZahtjevOdobreno( Rezervacije rez)
        //{

        //    string subject = string.Format("Vaša Rezervacija za Automobil je odobrena ");
        //    string s = "Vaša Rezervacija za Automobil je odobrena";
        //    s +=  GetRezInfo(rez) + GetRezInfoOdobrena(rez);
        //    s += "<br/>" + GetAnchor(FullyQualifiedApplicationPath("Home/ZapisnikPage/" + rez.id_rez), "Kliknite ovde za zapisnik sa putovanja");
        //    string body = string.Format("<p style=\"font-family:arial;font-size:16px\">{0}</p>", s);
        //    var mm = InitMailMessage(rez.Lokacije.Zaposlenici.email, rez.Zaposlenici.email, subject, body);
        //    SendEMail(mm);

        //}

        //public static void SendEmailToZahtjevOdobijeno( Rezervacije rez)
        //{

        //    string subject = string.Format("Vaša Rezervacija za Automobil je odbijena ");
        //    string s = "Vaša Rezervacija za Automobil je odbijena";
        //    s +=  GetRezInfo(rez) + GetRezInfoOdbijena(rez);
        //    //s += "<br/>" + GetAnchor(FullyQualifiedApplicationPath("Rezervacije/Potvrda/" + rez.id_rez), "Kliknite ovde");
        //    string body = string.Format("<p style=\"font-family:arial;font-size:16px\">{0}</p>", s);
        //    var mm = InitMailMessage(rez.Lokacije.Zaposlenici.email, rez.Zaposlenici.email, subject, body);
        //    SendEMail(mm);

        //}
      
        //private static string GetRezInfo(Rezervacije rez)
        //{
        //    var sb = new StringBuilder(512);
        //    sb.Append("<table>");
        //    sb.Append("<tr>");
        //    if (rez.Mjesta != null) sb.Append(td + "Odredište      : " + tdn + td + rez.Mjesta.Naziv + tdn).Append(Br);
        //    sb.Append("</tr><tr>");
        //    if (rez.Lokacije != null)
        //        sb.Append(td + "Polazak iz     :" + tdn + td + rez.Lokacije.Naziv + tdn).Append(Br);
        //    sb.Append("</tr><tr>");
        //    sb.Append(td + "Vrijeme polaska: " + tdn + td + rez.datum_polaska + tdn).Append(Br);
        //    sb.Append("</tr><tr>");
        //    sb.Append(td + "Broj putnika   : " + tdn + td + rez.broj_putnika + tdn).Append(Br);
        //    sb.Append("</tr>");
        //    sb.Append("</table>");
        //    sb.Append("<hr/>");
        //    return sb.ToString();
        //}

        //private static string GetRazInfo(Razduzenje rez)
        //{
        //    var sb = new StringBuilder(512);
        //    sb.Append("<table>");
        //    sb.Append("<tr>");
        //    if (rez.id_tel != null) sb.Append(td + "Odredište      : " + tdn + td + rez.Mjesta.Naziv + tdn).Append(Br);
        //    sb.Append("</tr><tr>");
        //    if (rez.Lokacije != null)
        //        sb.Append(td + "Polazak iz     :" + tdn + td + rez.Lokacije.Naziv + tdn).Append(Br);
        //    sb.Append("</tr><tr>");
        //    sb.Append(td + "Vrijeme polaska: " + tdn + td + rez.datum_polaska + tdn).Append(Br);
        //    sb.Append("</tr><tr>");
        //    sb.Append(td + "Broj putnika   : " + tdn + td + rez.broj_putnika + tdn).Append(Br);
        //    sb.Append("</tr>");
        //    sb.Append("</table>");
        //    sb.Append("<hr/>");
        //    return sb.ToString();
        //}

        //private static string GetRezInfoOdobrena(Rezervacije rez)
        //{
        //    var sb = new StringBuilder(512);
        //    sb.Append("<table>");
        //    sb.Append("<tr>");
        //    sb.Append(td + "Automobil      : " + tdn + td + rez.Automobil.Naziv + "  Regbr: " + rez.Automobil.RegBr + tdn).Append(Br);
        //    sb.Append("</tr><tr>");
        //    if (rez.Lokacije1 != null)
        //        sb.Append(td + "Mjesto preuzimanja vozila    :" + tdn + td + rez.Lokacije1.Naziv + tdn).Append(Br);
        //    sb.Append("</tr><tr>");
        //    if (rez.Lokacije2 != null)
        //        sb.Append(td + "Mjesto vraćanja vozila: " + tdn + td + rez.Lokacije2.Naziv + tdn).Append(Br);
        //    sb.Append("</tr><tr>");

        //    if (rez.Zaposlenici1 != null)
        //    {
        //        string put = rez.Zaposlenici1.ImePrezime;
        //        if (rez.Zaposlenici2 != null) put += ", " + rez.Zaposlenici2.ImePrezime;
        //        if (rez.Zaposlenici3 != null) put += ", " + rez.Zaposlenici3.ImePrezime;
        //        if (rez.Zaposlenici4 != null) put += ", " + rez.Zaposlenici4.ImePrezime;
        //        if (rez.Zaposlenici5 != null) put += ", " + rez.Zaposlenici5.ImePrezime;
        //        if (rez.Zaposlenici6 != null) put += ", " + rez.Zaposlenici6.ImePrezime;

        //        sb.Append(td + "Putnici: " + tdn + td + put + tdn).Append(Br);
        //    }
        //    sb.Append("</tr><tr>");            
            
        //    sb.Append(td + "Komentar   : " + tdn + td + rez.Komentar + tdn).Append(Br);
        //    sb.Append("</tr>");
        //    sb.Append("</table>");
        //    sb.Append("<hr/>");
        //    return sb.ToString();
        //}

        //private static string GetRezInfoOdbijena(Rezervacije rez)
        //{
        //    var sb = new StringBuilder(512);
        //    sb.Append("<table>");
        //    sb.Append("<tr>");
        //    if(rez.Automobil == null)
        //        sb.Append(td + "Automobil      : " + tdn + td + " Ne postoji slobodan automobil " + tdn).Append(Br);
        //    sb.Append("</tr><tr>");
        //    sb.Append(td + "Komentar   : " + tdn + td + rez.Komentar + tdn).Append(Br);
        //    sb.Append("</tr>");
        //    sb.Append("</table>");
        //    sb.Append("<hr/>");
        //    return sb.ToString();
        //}

        private static string GetZaposlenikInfo(Zaposlenici zaposlenik)
        {
            var sb = new StringBuilder(512);
            sb.Append("<table>");
            sb.Append("<tr>");
            sb.Append(td + "Ime i Prezime :" + tdn +td + zaposlenik.ImePrezime + tdn).Append(Br);
            sb.Append("</tr><tr>");
            sb.Append(td +"OD :" + tdn +td +zaposlenik.OD2.Naziv+ tdn).Append(Br);
            sb.Append("</tr><tr>");
            sb.Append(td +"Radno Mjesto :"  + tdn +td +zaposlenik.RadnaMjesta.Naziv+ tdn).Append(Br);
            sb.Append("</tr><tr>");
            sb.Append(td + "Nadređeni voditelj :" + tdn + td + zaposlenik.OD2.Zaposlenici.ImePrezime + tdn).Append(Br);
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<hr/>");

            return sb.ToString();
        }

        //private static string GetRezInfoZakljuciti(Rezervacije rez)
        //{
        //    var sb = new StringBuilder(512);
        //    sb.Append("<table>");
        //    sb.Append("<tr>");
        //    if (rez.Automobil != null)
        //        sb.Append(td + "Automobil      : " + tdn + td + rez.Automobil.Naziv + "  Regbr: " + rez.Automobil.RegBr + tdn).Append(Br);
        //    sb.Append("</tr><tr>");
        //    if (rez.Lokacije1 != null)
        //        sb.Append(td + "Mjesto preuzimanja vozila    :" + tdn + td + rez.Lokacije1.Naziv + tdn).Append(Br);
        //    sb.Append("</tr><tr>");
        //    if (rez.Lokacije2 != null)
        //        sb.Append(td + "Mjesto vraćanja vozila: " + tdn + td + rez.Lokacije2.Naziv + tdn).Append(Br);
        //    sb.Append("</tr><tr>");

        //    if (rez.Zaposlenici1 != null)
        //    {
        //        string put = rez.Zaposlenici1.ImePrezime;
        //        if (rez.Zaposlenici2 != null) put += ", " + rez.Zaposlenici2.ImePrezime;
        //        if (rez.Zaposlenici3 != null) put += ", " + rez.Zaposlenici3.ImePrezime;
        //        if (rez.Zaposlenici4 != null) put += ", " + rez.Zaposlenici4.ImePrezime;
        //        if (rez.Zaposlenici5 != null) put += ", " + rez.Zaposlenici5.ImePrezime;
        //        if (rez.Zaposlenici6 != null) put += ", " + rez.Zaposlenici6.ImePrezime;

        //        sb.Append(td + "Putnici: " + tdn + td + put + tdn).Append(Br);
        //    }
        //    sb.Append("</tr><tr>");
        //    sb.Append(td + "Komentar   : " + tdn + td + rez.Komentar + tdn).Append(Br);
        //    sb.Append("</tr>");
        //    sb.Append("<tr>");
        //    sb.Append(td + "Početna kilometraža   : " + tdn + td + rez.Poc_KM + tdn).Append(Br);
        //    sb.Append("</tr>");
        //    sb.Append("<tr>");
        //    sb.Append(td + "Završna kilometraža   : " + tdn + td + rez.Zav_KM + tdn).Append(Br);
        //    sb.Append("</tr>");
        //    sb.Append("<tr>");
        //    sb.Append(td + "Zapisnik   : " + tdn + td + rez.Zapisnik + tdn).Append(Br);
        //    sb.Append("</tr>");
        //    sb.Append("</table>");
        //    sb.Append("<hr/>");
        //    return sb.ToString();
        //}

        //public static MailMessage InitMailMessageOdobriAdiEmailAccount(Prijave prijava)
        //{
        //    Zaposlenici zaposlenik = prijava.Zaposlenici;
        //    const string to = "novidjelatnik@hypo.ba";
        //    string subject = string.Format("Odobriti Active Directory i Email account za djelatnika {0} {1}", zaposlenik.Ime, zaposlenik.Prezime);
        //    string s = "Potrebno je odobriti izdavanje AD i Email accounta.";
        //    s += GetZaposlenikInfo(zaposlenik, false, true, true, false, false) + "<br/><br/>" + GetAnchor(FullyQualifiedApplicationPath("IT/Potvrda.aspx?prijava=" + prijava.id_prijave), "Kliknite ovde");
        //    string body = string.Format("<p style=\"font-family:arial;font-size:16px\">{0}</p>", s);
        //    return InitMailMessage(to, subject, body);
        //}

    }
}