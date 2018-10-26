using System;
using System.Net.Mail;
using System.Text;
using System.Web;
using Vozila.Models;

namespace Vozila.Controllers
{
    public class Utils
    {
        private static readonly SmtpClient SmtpClient = new SmtpClient("A28000EE");
        private const string Br = "<br/>";
        private const string Td = "<td>";
        private const string Tdn = "</td>";

        public static void SendEMail(MailMessage mailMessage, bool async = false)
        {
            if (true)
            {
                if (async)
                {
                    SmtpClient.SendAsync(mailMessage, null);
                }
                else
                {
                    SmtpClient.Send(mailMessage);
                }
            }
        }


        public static MailMessage InitMailMessage(string from, string to, string subject, string body)
        {
            // to = "administrator.hypoba@hypo-alpe-adria.com";
            var mm = new MailMessage(from, to, subject.Replace('\r', ' ').Replace('\n', ' '), body) {IsBodyHtml = true};

            return mm;
        }


        public static string FullyQualifiedApplicationPath(string page)
        {
            HttpContext context = HttpContext.Current;

            if (context == null) return null;

            string appPath = string.Format("{0}://{1}{2}{3}",
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

        public static void SendEmailToError(string greska)
        {
            string subject = string.Format("Greška u APP rezervacije vozila ");
            MailMessage mm = InitMailMessage("postmaster@hypo-alpe-adria.ba", "zeljko.crnjac@hypo-alpe-adria.ba",
                                             subject, greska);
            SendEMail(mm);
        }

        public static void SendEmailToOdobravatelj(Zaposlenici zap, string to, Rezervacije rez)
        {
            try
            {
                string subject = string.Format("Odobriti Rezervaciju Automobila za djelatnika {0} {1}", zap.Ime,
                                               zap.Prezime);
                string s = "Potrebno je odobriti rezervaciju Automobila za.";
                s +="<table><tr><td>"+ GetZaposlenikInfo(zap) +
                    "</td><td>"+ GetRezInfo(rez) +
                    "</td></tr></table>";
                s += "<br/>" +
                     GetAnchor(FullyQualifiedApplicationPath("Rezervacije/PotvrdaPage/" + rez.id_rez), "Kliknite ovde");
                string body = string.Format("<p style=\"font-family:arial;font-size:16px\">{0}</p>", s);
                MailMessage mm = InitMailMessage(zap.email, to, subject, body);
                SendEMail(mm);
            }
            catch (Exception ex)
            {
                SendEmailToError(ex.ToString());
            }
        }
        public static void SendEmailToOdobravateljIsp(Zaposlenici zap, string to, Rezervacije rez)
        {
            try
            {
                string subject = string.Format("Odobriti Ispravljenu Rezervaciju Automobila za djelatnika {0} {1}", zap.Ime,
                                               zap.Prezime);
                string s = "Potrebno je odobriti Ispravljenu rezervaciju Automobila za.";
                s += "<table><tr><td>" + GetZaposlenikInfo(zap) +
                    "</td><td>" + GetRezInfo(rez) +
                    "</td></tr></table>";
                    s += "<br/>" +
                     GetAnchor(FullyQualifiedApplicationPath("Rezervacije/PotvrdaPage/" + rez.id_rez), "Kliknite ovde");
                string body = string.Format("<p style=\"font-family:arial;font-size:16px\">{0}</p>", s);
                MailMessage mm = InitMailMessage(zap.email, to, subject, body);
                SendEMail(mm);
            }
            catch (Exception ex)
            {
                SendEmailToError(ex.ToString());
            }
        }

        public static void SendEmailToZahtjevZakljuciti(Rezervacije rez)
        {
            try
            {
                string subject = string.Format("Rezervaciju za Automobil potrebno je zakljuciti ");
                string s = "Rezervaciju za Automobil potrebno je zakljuciti";
                s += "<table><tr><td>" + GetRezInfo(rez) +
                    "</td><td>" + GetRezInfoZakljuciti(rez) +
                    "</td></tr></table>"; 
                s += "<br/>" +
                     GetAnchor(FullyQualifiedApplicationPath("Rezervacije/ZakljucitiPage/" + rez.id_rez),
                               "Kliknite ovde za zapisnik sa putovanja");
                string body = string.Format("<p style=\"font-family:arial;font-size:16px\">{0}</p>", s);
                MailMessage mm = InitMailMessage(rez.Zaposlenici.email, rez.Lokacije.Zaposlenici.email, subject, body);
                SendEMail(mm);
            }
            catch (Exception ex)
            {
                SendEmailToError(ex.ToString());
            }
        }

        public static void SendEmailToInfoVozila(Rezervacije rez)
        {
            try
            {
                string subject = string.Format("Info o vozilu nakon putovanja");
                string s = "Nakon izvršenog putovanja zapažanja na vozilu";
                s += "<table><tr><td>" + GetRezInfo(rez) +
                    "</td><td>" + GetRezInfoZakljuciti(rez) +
                    "</td></tr></table>"; 
                s += "<br/>" +
                     GetAnchor(FullyQualifiedApplicationPath("Rezervacije/Details/" + rez.id_rez),
                               "Kliknite ovde za info o rezervaciji");
                string body = string.Format("<p style=\"font-family:arial;font-size:16px\">{0}</p>", s);
                MailMessage mm = InitMailMessage(rez.Zaposlenici.email,"vozila.info@hypo.ba", subject, body);
                SendEMail(mm);
            }
            catch (Exception ex)
            {
                SendEmailToError(ex.ToString());
            }
        }
        public static void SendEmailToPosiljatelj(Rezervacije rez)
        {
            try
            {
                string subject = string.Format("Rezervaciju za Automobil je zaključena ");
                string s = "Rezervaciju za Automobil je zaključena ";
                s += "<table><tr><td>" + GetRezInfo(rez) +
                    "</td><td>" + GetRezInfoZakljuciti(rez) +
                    "</td></tr></table>"; 
                // s += "<br/>" + GetAnchor(FullyQualifiedApplicationPath("Rezervacije/ZapisnikPage/" + rez.id_rez), "Kliknite ovde za zapisnik sa putovanja");
                string body = string.Format("<p style=\"font-family:arial;font-size:16px\">{0}</p>", s);
                MailMessage mm = InitMailMessage(rez.Lokacije.Zaposlenici.email, rez.Zaposlenici.email, subject, body);
                SendEMail(mm);
            }
            catch (Exception ex)
            {
                SendEmailToError(ex.ToString());
            }
        }

        public static void SendEmailToZahtjevOdobreno(Rezervacije rez)
        {
            try
            {
                string info = GetRezInfo(rez);
                string subject = string.Format("Vaša Rezervacija za Automobil je odobrena ");
                string s = "Vaša Rezervacija za Automobil je odobrena";
                s += "<table><tr><td>" + GetRezInfo(rez) +
                    "</td><td>" + GetRezInfoOdobrena(rez) +
                    "</td></tr></table>"; 
                s += "<br/>" +
                     GetAnchor(FullyQualifiedApplicationPath("Home/ZapisnikPage/" + rez.id_rez),
                               "Kliknite ovde za zapisnik sa putovanja");
                string body = string.Format("<p style=\"font-family:arial;font-size:16px\">{0}</p>", s);
                MailMessage mm = InitMailMessage(rez.Lokacije.Zaposlenici.email, rez.Zaposlenici.email, subject, body);

                string from = rez.Zaposlenici.email;
                // slanje email poruke putnicima
                if (rez.id_Putnik1 != null)
                    SendEmailToZahtjevPutnik(info,from, rez.Zaposlenici1.email, rez.Zaposlenici.ImePrezime);
                if (rez.id_Putnik2 != null)
                    SendEmailToZahtjevPutnik(info, from, rez.Zaposlenici2.email, rez.Zaposlenici.ImePrezime);
                if (rez.id_Putnik3 != null)
                    SendEmailToZahtjevPutnik(info, from, rez.Zaposlenici3.email, rez.Zaposlenici.ImePrezime);
                if (rez.id_Putnik4 != null)
                    SendEmailToZahtjevPutnik(info, from, rez.Zaposlenici4.email, rez.Zaposlenici.ImePrezime);
                if (rez.id_Putnik5 != null)
                    SendEmailToZahtjevPutnik(info, from, rez.Zaposlenici5.email, rez.Zaposlenici.ImePrezime);
                if (rez.id_Putnik6 != null)
                    SendEmailToZahtjevPutnik(info, from, rez.Zaposlenici6.email, rez.Zaposlenici.ImePrezime);

                SendEMail(mm);
            }
            catch (Exception ex)
            {
                SendEmailToError(ex.ToString());
            }
        }

        public static void SendEmailToZahtjevZapisnik(Rezervacije rez)
        {
            try
            {
               // string info = GetRezInfo(rez);
                string subject = string.Format("Vaša Rezervacija za Automobil nije zakljucena");
                string s = "Vaša Rezervacija za Automobil nije zakljucena";
                s += "<table><tr><td>" + GetRezInfo(rez) +
                    "</td><td>" + GetRezInfoOdobrena(rez) +
                    "</td></tr></table>";
                s += "<br/>" +
                     GetAnchor(FullyQualifiedApplicationPath("Home/ZapisnikPage/" + rez.id_rez),
                               "Kliknite ovde za zapisnik sa putovanja");
                string body = string.Format("<p style=\"font-family:arial;font-size:16px\">{0}</p>", s);
                MailMessage mm = InitMailMessage(rez.Lokacije.Zaposlenici.email, rez.Zaposlenici.email, subject, body);

                //string from = rez.Zaposlenici.email;
                //// slanje email poruke putnicima
                //if (rez.id_Putnik1 != null)
                //    SendEmailToZahtjevPutnik(info, from, rez.Zaposlenici1.email, rez.Zaposlenici.ImePrezime);
                //if (rez.id_Putnik2 != null)
                //    SendEmailToZahtjevPutnik(info, from, rez.Zaposlenici2.email, rez.Zaposlenici.ImePrezime);
                //if (rez.id_Putnik3 != null)
                //    SendEmailToZahtjevPutnik(info, from, rez.Zaposlenici3.email, rez.Zaposlenici.ImePrezime);
                //if (rez.id_Putnik4 != null)
                //    SendEmailToZahtjevPutnik(info, from, rez.Zaposlenici4.email, rez.Zaposlenici.ImePrezime);
                //if (rez.id_Putnik5 != null)
                //    SendEmailToZahtjevPutnik(info, from, rez.Zaposlenici5.email, rez.Zaposlenici.ImePrezime);
                //if (rez.id_Putnik6 != null)
                //    SendEmailToZahtjevPutnik(info, from, rez.Zaposlenici6.email, rez.Zaposlenici.ImePrezime);

                SendEMail(mm);
            }
            catch (Exception ex)
            {
                SendEmailToError(ex.ToString());
            }
        }
        public static void SendEmailToZahtjevEditOdobreno(Rezervacije rez)
        {
            try
            {
                string info = "<table><tr><td>" + GetRezInfo(rez) +
                    "</td><td>" + GetRezInfoOdobrena(rez) +
                    "</td></tr></table>"; 
               
                string subject = string.Format("Za vašu rezervaciju dodijeljeno je novo Vozilo");
                string s = "Za vašu rezervaciju dodijeljeno je novo Vozilo";
                s += info; 
                s += "<br/>" +
                     GetAnchor(FullyQualifiedApplicationPath("Home/ZapisnikPage/" + rez.id_rez),
                               "Kliknite ovde za zapisnik sa putovanja");
                string body = string.Format("<p style=\"font-family:arial;font-size:16px\">{0}</p>", s);
                MailMessage mm = InitMailMessage(rez.Lokacije.Zaposlenici.email, rez.Zaposlenici.email, subject, body);

                string from = rez.Zaposlenici.email;
                // slanje email poruke putnicima
                if (rez.id_Putnik1 != null)
                    SendEmailToZahtjevIzmjenaPutnik(info, from, rez.Zaposlenici1.email, rez.Zaposlenici.ImePrezime);
                if (rez.id_Putnik2 != null)
                    SendEmailToZahtjevIzmjenaPutnik(info, from, rez.Zaposlenici2.email, rez.Zaposlenici.ImePrezime);
                if (rez.id_Putnik3 != null)
                    SendEmailToZahtjevIzmjenaPutnik(info, from, rez.Zaposlenici3.email, rez.Zaposlenici.ImePrezime);
                if (rez.id_Putnik4 != null)
                    SendEmailToZahtjevIzmjenaPutnik(info, from, rez.Zaposlenici4.email, rez.Zaposlenici.ImePrezime);
                if (rez.id_Putnik5 != null)
                    SendEmailToZahtjevIzmjenaPutnik(info, from, rez.Zaposlenici5.email, rez.Zaposlenici.ImePrezime);
                if (rez.id_Putnik6 != null)
                    SendEmailToZahtjevIzmjenaPutnik(info, from, rez.Zaposlenici6.email, rez.Zaposlenici.ImePrezime);

                SendEMail(mm);
            }
            catch (Exception ex)
            {
                SendEmailToError(ex.ToString());
            }
        }

        public static void SendEmailToZahtjevPutnik(string info,string from, string putnikemail, string kontakt)
        {
            try
            {
                string subject = string.Format("Vaša Rezervacija za Automobil je odobrena ");
                string s = "Vaša Rezervacija za Automobil je odobrena";
                s +=  info; 
                s += "<br>Kontakt osoba je: " + kontakt;
                string body = string.Format("<p style=\"font-family:arial;font-size:16px\">{0}</p>", s);
                MailMessage mm = InitMailMessage(from, putnikemail, subject, body);

                // slanje email poruke putnicima
                SendEMail(mm);
            }
            catch (Exception ex)
            {
                SendEmailToError(ex.ToString());
            }
        }
       
        public static void SendEmailToZahtjevIzmjenaPutnik(string info, string from, string putnikemail, string kontakt)
        {
            try
            {
                string subject = string.Format("Za vašu rezervaciju dodijeljeno je novo Vozilo");
                string s = "Za vašu rezervaciju dodijeljeno je novo Vozilo";
                s += info;
                s += "<br>Kontakt osoba je: " + kontakt;
                string body = string.Format("<p style=\"font-family:arial;font-size:16px\">{0}</p>", s);
                MailMessage mm = InitMailMessage(from, putnikemail, subject, body);

                // slanje email poruke putnicima
                SendEMail(mm);
            }
            catch (Exception ex)
            {
                SendEmailToError(ex.ToString());
            }
        }

        public static void SendEmailToZahtjevOdobijeno(Rezervacije rez)
        {
            try
            {
                string subject = string.Format("Vaša Rezervacija za Automobil je odbijena ");
                string s = "Vaša Rezervacija za Automobil je odbijena";
                s += "<table><tr><td>" + GetRezInfo(rez) +
                    "</td><td>" + GetRezInfoOdbijena(rez) +
                    "</td></tr></table>"; 
                  
                //s += "<br/>" + GetAnchor(FullyQualifiedApplicationPath("Rezervacije/Potvrda/" + rez.id_rez), "Kliknite ovde");
                string body = string.Format("<p style=\"font-family:arial;font-size:16px\">{0}</p>", s);
                MailMessage mm = InitMailMessage(rez.Lokacije.Zaposlenici.email, rez.Zaposlenici.email, subject, body);
                SendEMail(mm);
            }
            catch (Exception ex)
            {
                SendEmailToError(ex.ToString());
            }
        }

        public static void SendEmailToZahtjevVraceno(Rezervacije rez)
        {
            try
            {
                string subject = string.Format("Vaša Rezervacija za Automobil je vraćena za doradu");
                string s = "Vaša Rezervacija za Automobil je vraćena za doradu";
                s += GetRezInfo(rez);
                s += "<br/>" + GetAnchor(FullyQualifiedApplicationPath("Home/Edit/" + rez.id_rez), "Kliknite ovde");
                string body = string.Format("<p style=\"font-family:arial;font-size:16px\">{0}</p>", s);
                MailMessage mm = InitMailMessage(rez.Lokacije.Zaposlenici.email, rez.Zaposlenici.email, subject, body);
                SendEMail(mm);
            }
            catch (Exception ex)
            {
                SendEmailToError(ex.ToString());
            }
        }
        public static void SendEmailToZahtjevOtkazano(Rezervacije rez)
        {
            try
            {
                string subject = string.Format("Vaša Rezervacija za Automobil je otkazana ");
                string s = "Vaša Rezervacija za Automobil je otkazana";
                s += "<table><tr><td>" + GetRezInfo(rez) +
                    "</td><td>" + GetRezInfoOdbijena(rez) +
                    "</td></tr></table>"; 
                   
                //s += "<br/>" + GetAnchor(FullyQualifiedApplicationPath("Rezervacije/Potvrda/" + rez.id_rez), "Kliknite ovde");
                string body = string.Format("<p style=\"font-family:arial;font-size:16px\">{0}</p>", s);
                MailMessage mm = InitMailMessage(rez.Lokacije.Zaposlenici.email, rez.Zaposlenici.email, subject, body);
                SendEMail(mm);
            }
            catch (Exception ex)
            {
                SendEmailToError(ex.ToString());
            }
        }

        private static string GetRezInfo(Rezervacije rez)
        {
            try
            {
                var sb = new StringBuilder(512);
                sb.Append("<table>");
                sb.Append("<tr>");
                if (rez.Mjesta != null)
                    sb.Append(Td + "Odredište      : " + Tdn + Td + rez.Mjesta.Naziv + Tdn).Append(Br);
                sb.Append("</tr><tr>");
                if (rez.Lokacije != null)
                    sb.Append(Td + "Polazak iz     :" + Tdn + Td + rez.Lokacije.Naziv + Tdn).Append(Br);
                sb.Append("</tr><tr>");
                sb.Append(Td + "Vrijeme polaska: " + Tdn + Td + rez.datum_polaska + Tdn).Append(Br);
                sb.Append("</tr><tr>");
                sb.Append("</tr><tr>");
                sb.Append(Td + "Vrijeme povratka: " + Tdn + Td + rez.datum_dolaska + Tdn).Append(Br);
                sb.Append("</tr><tr>");
                sb.Append(Td + "Broj putnika   : " + Tdn + Td + rez.broj_putnika + Tdn).Append(Br);
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<hr/>");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                SendEmailToError(ex.ToString());
                return "";
            }
        }

        private static string GetRezInfoOdobrena(Rezervacije rez)
        {
            try
            {
                var sb = new StringBuilder(512);
                sb.Append("<table>");
                sb.Append("<tr>");
                if (rez.id_auto != null)
                {
                    sb.Append(Td + "Automobil      : " + Tdn + Td + rez.Automobil.Naziv + "  Regbr: " +
                              rez.Automobil.RegBr +
                              Tdn).Append(Br);
                }
                sb.Append("</tr><tr>");
                if (rez.Lokacije1 != null)
                    sb.Append(Td + "Mjesto preuzimanja vozila    :" + Tdn + Td + rez.Lokacije1.Naziv + Tdn).Append(Br);
                sb.Append("</tr><tr>");
                if (rez.Lokacije2 != null)
                    sb.Append(Td + "Mjesto vraćanja vozila: " + Tdn + Td + rez.Lokacije2.Naziv + Tdn).Append(Br);
                sb.Append("</tr><tr>");

                if (rez.Zaposlenici1 != null)
                {
                    string put = rez.Zaposlenici1.ImePrezime;

                    if (rez.Zaposlenici2 != null) put += ", " + rez.Zaposlenici2.ImePrezime;
                    if (rez.Zaposlenici3 != null) put += ", " + rez.Zaposlenici3.ImePrezime;
                    if (rez.Zaposlenici4 != null) put += ", " + rez.Zaposlenici4.ImePrezime;
                    if (rez.Zaposlenici5 != null) put += ", " + rez.Zaposlenici5.ImePrezime;
                    if (rez.Zaposlenici6 != null) put += ", " + rez.Zaposlenici6.ImePrezime;

                    sb.Append(Td + "Putnici: " + Tdn + Td + put + Tdn).Append(Br);
                }
                sb.Append("</tr><tr>");

                sb.Append(Td + "Komentar   : " + Tdn + Td + rez.Komentar + Tdn).Append(Br);
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<hr/>");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                SendEmailToError(ex.ToString());
                return "";
            }
        }

        private static string GetRezInfoOdbijena(Rezervacije rez)
        {
            try
            {
                var sb = new StringBuilder(512);
                sb.Append("<table>");
                sb.Append("<tr>");
                if (rez.Automobil == null)
                    sb.Append(Td + "Automobil      : " + Tdn + Td + " Ne postoji slobodan automobil " + Tdn).Append(Br);
                sb.Append("</tr><tr>");
                sb.Append(Td + "Komentar   : " + Tdn + Td + rez.Komentar + Tdn).Append(Br);
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<hr/>");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                SendEmailToError(ex.ToString());
                return "";
            }
        }

        private static string GetZaposlenikInfo(Zaposlenici zaposlenik)
        {
            try
            {
                var sb = new StringBuilder(512);
                sb.Append("<table>");
                sb.Append("<tr>");
                sb.Append(Td + "Ime i Prezime :" + Tdn + Td + zaposlenik.ImePrezime + Tdn).Append(Br);
                sb.Append("</tr><tr>");
                sb.Append(Td + "OD :" + Tdn + Td + zaposlenik.OD2.Naziv + Tdn).Append(Br);
                sb.Append("</tr><tr>");
                sb.Append(Td + "Radno Mjesto :" + Tdn + Td + zaposlenik.RadnaMjesta.Naziv + Tdn).Append(Br);
                sb.Append("</tr><tr>");
                sb.Append(Td + "Nadređeni voditelj :" + Tdn + Td + zaposlenik.OD2.Zaposlenici.ImePrezime + Tdn)
                  .Append(Br);
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<hr/><br>");

                return sb.ToString();
            }
            catch (Exception ex)
            {
                SendEmailToError(ex.ToString());
                return "";
            }
        }
        private static string GetRezInfoZakljuciti(Rezervacije rez)
        {
            try
            {
                var sb = new StringBuilder(512);
                sb.Append("<table>");
                sb.Append("<tr>");
                if (rez.Automobil != null)
                    sb.Append(Td + "Automobil      : " + Tdn + Td + rez.Automobil.Naziv + "  Regbr: " +
                              rez.Automobil.RegBr + Tdn).Append(Br);
                sb.Append("</tr><tr>");
                if (rez.Lokacije1 != null)
                    sb.Append(Td + "Mjesto preuzimanja vozila    :" + Tdn + Td + rez.Lokacije1.Naziv + Tdn).Append(Br);
                sb.Append("</tr><tr>");
                if (rez.Lokacije2 != null)
                    sb.Append(Td + "Mjesto vraćanja vozila: " + Tdn + Td + rez.Lokacije2.Naziv + Tdn).Append(Br);
                sb.Append("</tr><tr>");

                if (rez.Zaposlenici1 != null)
                {
                    string put = rez.Zaposlenici1.ImePrezime;
                    if (rez.Zaposlenici2 != null) put += ", " + rez.Zaposlenici2.ImePrezime;
                    if (rez.Zaposlenici3 != null) put += ", " + rez.Zaposlenici3.ImePrezime;
                    if (rez.Zaposlenici4 != null) put += ", " + rez.Zaposlenici4.ImePrezime;
                    if (rez.Zaposlenici5 != null) put += ", " + rez.Zaposlenici5.ImePrezime;
                    if (rez.Zaposlenici6 != null) put += ", " + rez.Zaposlenici6.ImePrezime;

                    sb.Append(Td + "Putnici: " + Tdn + Td + put + Tdn).Append(Br);
                }
                sb.Append("</tr><tr>");
                sb.Append(Td + "Komentar   : " + Tdn + Td + rez.Komentar + Tdn).Append(Br);
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append(Td + "Početna kilometraža   : " + Tdn + Td + rez.Poc_KM + Tdn).Append(Br);
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append(Td + "Završna kilometraža   : " + Tdn + Td + rez.Zav_KM + Tdn).Append(Br);
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append(Td + "Zapisnik   : " + Tdn + Td + rez.Zapisnik + Tdn).Append(Br);
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<hr/>");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                SendEmailToError(ex.ToString());
                return "";
            }
        }

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