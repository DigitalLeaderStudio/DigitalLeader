namespace DigitalLeader.Web.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.ViewModels;
	using DigitalLeader.Web.Controllers.Controllers;
	using System.Collections.Generic;
	using System.Web.Mvc;
	using DigitalLeader.Web.Extensions;

	public class HomeController : BaseController
	{
		private const string PRIVACY_KEY = "privacy";
		private const string CONDITIONSOFUSE_KEY = "conditions-of-use";

		private readonly IServiceCategoryService _categoryService;
		private readonly IContentService _contentService;
		private readonly IServiceService _serviceService;
		private readonly ISliderService _sliderService;

		public HomeController(
			IServiceCategoryService categoryService,
			IServiceService serviceService,
			IContentService contentService,
			ISliderService sliderService)
		{
			_categoryService = categoryService;
			_serviceService = serviceService;
			_contentService = contentService;
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


		[Route("Privacy")]
		public ActionResult Privacy()
		{
			var entity = _contentService.GetByKey(PRIVACY_KEY);

			var viewModel = Mapper.Map<Content, ContentViewModel>(entity);

			return View(viewModel);
		}

		[Route("Condition-of-use")]
		public ActionResult ConditionsOfUse()
		{
			var entity = _contentService.GetByKey(CONDITIONSOFUSE_KEY);

			var viewModel = Mapper.Map<Content, ContentViewModel>(entity);

			return View(viewModel);
		}

		public ActionResult _SliderPartialView()
		{
			var viewModel = Mapper.Map<List<Slider>, List<SliderViewModel>>(_sliderService.GetAll());

			return PartialView(viewModel);
		}
	}
}