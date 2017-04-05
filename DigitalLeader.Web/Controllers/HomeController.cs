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
		private readonly IServiceCategoryService _categoryService;
		private readonly IServiceService _serviceService;
		private readonly ISliderService _sliderService;

		public HomeController(
			IServiceCategoryService categoryService,
			IServiceService serviceService,
			ISliderService sliderService)
		{
			_categoryService = categoryService;
			_serviceService = serviceService;
			_sliderService = sliderService;
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

		public ActionResult _SliderPartialView()
		{
			var viewModel = Mapper.Map<List<Slider>, List<SliderViewModel>>(_sliderService.GetAll());

			return PartialView(viewModel);
		}
	}
}