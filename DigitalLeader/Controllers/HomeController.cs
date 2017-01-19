using System;
using System.Net.Mail;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitalLeader.Models;

namespace DigitalLeader.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Company()
        {
            return View();
        }

        public ActionResult Contact()
        {
            PromoFormViewModel model = new PromoFormViewModel();
            model.Message = "Describe your business problem";

            return View(model);
        }

        public ActionResult Services()
        {
            ViewBag.Message = "Services page";

            return View();
        }

        [HttpGet]
        public ActionResult Promo()
        {
            PromoFormViewModel model = new PromoFormViewModel();
            model.Message = "Describe your business problem";

            return PartialView("_PromoFormPartial", model);
        }

        [HttpPost]
        public async Task<ActionResult> Promo(PromoFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("info@digitalleader.solutions");
                mailMessage.To.Add("s.kalaida.biz@gmail.com");
                mailMessage.Subject = "New contact";
                mailMessage.Body = String.Format(@"
                                        <h1>New contact from the website</h1>
                                        <p><strong>Name:</strong> {0}</p>
                                        <p><strong>Last name:</strong> {1}</p>
                                        <p><strong>Company:</strong> {2}</p>
                                        <p><strong>Email:</strong> {3}</p>
                                        <p><strong>Phone:</strong> {4}</p>
                                        <p><strong>Message:</strong> {5}</p>
                                    ", model.FirstName, model.LastName, model.Company, model.Email, model.Phone, model.Message);
                mailMessage.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(mailMessage);
                }

                //return View("InvalidInput", model);
                return View("ThanksForContact", model);
            }            

            return View("InvalidInput");
        }
        
        public ActionResult Test()
        {
            ViewBag.Message = "Test page";

            return View();
        }
    }
}