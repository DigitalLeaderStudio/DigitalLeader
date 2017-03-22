namespace DigitalLeader.Web.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.ViewModels;
	using DigitalLeader.Web.Controllers;
	using DigitalLeader.Web.Controllers.Controllers;
	using System;
	using System.Collections.Generic;
	using System.Net.Mail;
	using System.Threading.Tasks;
	using System.Web.Mvc;

	public class HomeController : BaseController
	{
		private IServiceCategoryService _categoryService;
		private IServiceService _serviceService;

		public HomeController(IServiceCategoryService categoryService, IServiceService serviceService)
		{
			_categoryService = categoryService;

			_serviceService = serviceService;
		}
        [Route("")]
        public ActionResult Index()
		{
			var data = _categoryService.GetAll();

			var viewModel = Mapper.Map<List<ServiceCategory>, List<ServiceCategoryViewModel>>(data);

			return View(viewModel);
		}

		public ActionResult Company()
		{
			var data = _categoryService.GetAll();

			var viewModel = Mapper.Map<List<ServiceCategory>, List<ServiceCategoryViewModel>>(data);

			return View(viewModel);
		}
        [Route("Contact")]
		public ActionResult Contact()
		{
			PromoFormViewModel model = new PromoFormViewModel();
			model.Message = "Describe your business problem";

			return View(model);
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
                                        <p><strong>Email:</strong> {3}</p>
                                        <p><strong>Phone:</strong> {4}</p>
                                        <p><strong>Message:</strong> {5}</p>
                                    ", model.FirstName, model.Email, model.Phone, model.Message);
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