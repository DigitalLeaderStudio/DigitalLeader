namespace DigitalLeader.Web.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.ViewModels;
	using DigitalLeader.Web.Controllers.Controllers;
	using System.Web.Mvc;

	public class ServiceController : BaseController
	{
		//private ICategoryService _categoryService;
		private IServiceService _serviceService;

		public ServiceController(IServiceService serviceService)
		{
			_serviceService = serviceService;
		}

		// GET: Services
		[Route("Service/{service}")]
		public ActionResult Index(string service)
		{
			var viewModel = Mapper.Map<Service, ServiceViewModel>(_serviceService.GetByName(service));

			return View(viewModel);
		}
	}
}