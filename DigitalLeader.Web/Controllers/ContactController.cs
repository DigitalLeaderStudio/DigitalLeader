namespace DigitalLeader.Web.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.ViewModels;
	using DigitalLeader.Web.Controllers.Controllers;
	using Simplify.Mail;
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Web.Mvc;

	public class ContactController : BaseController
	{
		private readonly IContactRequestService _contactRequestService;
		private readonly IContentService _contentService;

		public ContactController(
			IContactRequestService contactRequestService,
			IContentService contentService)
		{
			_contactRequestService = contactRequestService;
			_contentService = contentService;
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

					try
					{
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
					}
					catch { }

					model.RedirectLink = Url.Action("ThankYou", "Home");

					if (model.Mode.Equals("Short"))
					{
						return new JsonResult
						{
							Data = model.RedirectLink
						};
					}

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