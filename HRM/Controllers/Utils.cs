using System;
using System.Net.Mail;
using System.Text;
using System.Web;
using HRM.Models;

namespace HRM.Controllers
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
            var mm = new MailMessage(from, to, subject.Replace('\r', ' ').Replace('\n', ' '), body) {IsBodyHtml = true};

            return mm;
        }

        public static MailMessage InitMailMessage(string to, string subject, string body)
        {
            // to = "administrator.hypoba@hypo-alpe-adria.com";
            var mm = new MailMessage("IT.OPERATIONS@hypo.ba", to, subject.Replace('\r', ' ').Replace('\n', ' '), body) { IsBodyHtml = true };
            return mm;
        }

        public static string FullyQualifiedApplicationPath(string page)
        {
            HttpContext context = HttpContext.Current;

            if (context == null) return null;

            string appPath = string.Format("{0}://{1}{2}{3}",
                                           context.Request.Url.Scheme,
                                           "hypoappserver",
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

        public static MailMessage InitMailMessageOdobriAdiEmailAccount(Zaposlenici zaposlenik, int prijavaid)
        {
            
            const string to = "novidjelatnik@hypo.ba";
            string subject = string.Format("Odobriti Active Directory i Email account za djelatnika {0} {1}", zaposlenik.Ime, zaposlenik.Prezime);
            string s = "Potrebno je odobriti izdavanje AD i Email accounta.";
            s += GetZaposlenikInfo(zaposlenik, false, true, true, false, false) + "<br/><br/>"
                + GetAnchor(FullyQualifiedApplicationPath("IT/Potvrda.aspx?prijava=" + prijavaid), "Kliknite ovde");
            string body = string.Format("<p style=\"font-family:arial;font-size:16px\">{0}</p>", s);
            return InitMailMessage(to, subject, body);
        }

        private static string GetZaposlenikInfo(Zaposlenici zaposlenik, bool showJmbg, bool showVoditelj, bool showPocetakRada, bool showEmail, bool showAd)
        {
            var sb = new StringBuilder(512);
            sb.Append(Br).Append(Br);
            sb.AppendFormat("Ime i Prezime: {0} {1}", zaposlenik.Ime, zaposlenik.Prezime).Append(Br);
            sb.AppendFormat("OD: {0}", zaposlenik.OD2.Naziv).Append(Br);
            sb.AppendFormat("Radno Mjesto: {0}", zaposlenik.RadnaMjesta.Naziv).Append(Br);
            if (showJmbg)
            {
                sb.AppendFormat("JMBG: {0}", zaposlenik.jmbg).Append(Br);
            }
            if (showVoditelj)
            {
                sb.AppendFormat("Nadređeni voditelj: {0} {1}", zaposlenik.OD2.Zaposlenici.Ime, zaposlenik.OD2.Zaposlenici.Prezime).Append(Br);
            }
            if (showPocetakRada)
            {
                sb.AppendFormat("Početak rada: {0:dd.MM.yyyy}", zaposlenik.datum_pocetka_rada).Append(Br);
            }
            if (showEmail)
            {
                sb.AppendFormat("Email: {0}", zaposlenik.email).Append(Br);
            }
            if (showAd)
            {
                sb.AppendFormat("AD: {0}", zaposlenik.ad).Append(Br);
            }
            sb.Append(Br);

            return sb.ToString();
        }

 

    }
}