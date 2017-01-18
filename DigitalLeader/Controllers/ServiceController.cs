using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalLeader.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Services
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DigitalConsulting()
        {
            return View();
        }
        public ActionResult SoftwareDevelopment()
        {
            return View();
        }
        public ActionResult ItOutsourcing()
        {
            return View();
        }
        public ActionResult DigitalMarketing()
        {
            return View();
        }

    }
}