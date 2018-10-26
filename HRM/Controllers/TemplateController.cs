using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Models;

namespace HRM.Controllers
{
    public class TemplateController : Controller
    {
        internal EvidencijaEntities db = new EvidencijaEntities();

        public ViewResult Error(string msg)
        {
            ViewBag.Message = msg;
            Utils.SendEmailToError(msg);
            return View("Error");
        }
    }
}
