namespace DigitalLeader.Web.Controllers
{
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.ViewModels;
	using DigitalLeader.Web.Controllers.Controllers;
	using System;
	using System.Net.Mail;
	using System.Threading.Tasks;
	using System.Web.Mvc;
	using System.Linq;
	using Simplify.Mail;
	using AutoMapper;
	using DigitalLeader.Entities;

	public class ContactController : BaseController
	{
		private readonly IContactRequestService _contactRequestService;

		public ContactController(IContactRequestService contactRequestService)
		{
			_contactRequestService = contactRequestService;
		}

		[Route("Contact")]
		public ActionResult Index()
		{
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
		public async Task<ActionResult> ContactRequest(PromoFormViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var entity = Mapper.Map<PromoFormViewModel, ContactRequest>(model);

					entity.CreatedDate = DateTime.UtcNow;
					entity.Ip = Request.UserHostAddress;
					_contactRequestService.Insert(entity);

					var adminEmialAddress = System.Configuration.ConfigurationManager.AppSettings["AdminEmail"].ToString();

					await MailSender.Default.SendAsync(
						adminEmialAddress,
						adminEmialAddress,
						"New contact",
						String.Format(@"
					                                   <h1>New contact from the website</h1>
					                                   <p><strong>Name:</strong> {0}</p>
					                                   <p><strong>Email:</strong> {1}</p>
					                                   <p><strong>Phone:</strong> {2}</p>
					                                   <p><strong>Message:</strong> {3}</p>
					                               ", model.FirstName, model.Email, model.Phone, model.Message));

					model.RedirectLink = Url.Action("ThankYou", "Home");
					return PartialView("_ContactRequestPartialView", model);
				}
				catch (Exception e)
				{
					ModelState.AddModelError("", e.Message);
				}
			}
			else
			{
				model.Errors = ModelState.Values.SelectMany(m => m.Errors)
									 .Select(e => e.ErrorMessage)
									 .ToList();
			}

			return PartialView("_ContactRequestPartialView", model);
		}
	}
}