using AutoMapper;
using DigitalLeader.Entities;
using DigitalLeader.Entities.Identity;
using DigitalLeader.Services.Interfaces;
using DigitalLeader.ViewModels;
using DigitalLeader.Web.Controllers.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DigitalLeader.Web.Controllers
{
	public class CompanyController : BaseController
	{
		private const string STORY_KEY = "company-story";
		private const string CREED_KEY = "company-creed";

		private readonly IUserService _userService;
		private readonly IContentService _contentService;
		private readonly IVacancyService _vacancyService;

		public CompanyController(
			IUserService userService,
			IContentService contentService,
			IVacancyService vacancyService)
		{
			_userService = userService;
			_contentService = contentService;
			_vacancyService = vacancyService;
		}

		[Route("Company/story")]
		public ActionResult Story()
		{
			var entity = _contentService.GetByKey(STORY_KEY);

			var viewModel = Mapper.Map<Content, ContentViewModel>(entity);

			return View(viewModel);
		}

		[Route("Company/creed")]
		public ActionResult Creed()
		{
			var entity = _contentService.GetByKey(CREED_KEY);

			var viewModel = Mapper.Map<Content, ContentViewModel>(entity);

			return View(viewModel);
		}

		[Route("Company/Team")]
		public ActionResult Team()
		{
			var viewModel = Mapper.Map<IEnumerable<User>, List<UserViewModel>>(_userService.GetAllExceptAdmins());

			return View(viewModel);
		}

		[Route("Company/Careers")]
		public ActionResult Careers()
		{
			var viewModel = Mapper.Map<IEnumerable<Vacancy>, List<VacancyViewModel>>(_vacancyService.GetAllOpenedVacancies());

			return View(viewModel);
		}

		[Route("Company/Careers/{id}")]
		public ActionResult Career(int id)
		{
			var viewModel = Mapper.Map<Vacancy, VacancyViewModel>(_vacancyService.GetById(id));

			return View(viewModel);
		}
	}
}