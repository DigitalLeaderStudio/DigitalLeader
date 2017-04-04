namespace DigitalLeader.Web.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.ViewModels;
	using DigitalLeader.Web.Controllers.Controllers;
	using System.Collections.Generic;
	using System.Web.Mvc;

	public class HomeController : BaseController
	{
		private IServiceCategoryService _categoryService;
		private IServiceService _serviceService;

		public HomeController(
			IServiceCategoryService categoryService,
			IServiceService serviceService)
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

		[Route("ThankYou")]
		public ActionResult ThankYou()
		{
			return View();
		}
	}
}