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
		private IUserService _userService;
		private IVacancyService _vacancyService;

		public CompanyController(
			IUserService userService,
			IVacancyService vacancyService)
		{
			_userService = userService;
			_vacancyService = vacancyService;
		}

		// GET: Company
		public ActionResult Story()
		{
			return View();
		}

		public ActionResult Creed()
		{
			return View();
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