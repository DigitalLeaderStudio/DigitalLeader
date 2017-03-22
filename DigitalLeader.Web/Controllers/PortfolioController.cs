namespace DigitalLeader.Web.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.ViewModels;
	using DigitalLeader.Web.Controllers.Controllers;
	using System.Collections.Generic;
	using System.Web.Mvc;

	public class PortfolioController : BaseController
	{
		private ITestimonialService _testimonialService;
		private IProjectService _projectService;

		public PortfolioController(
			ITestimonialService testimonialService,
			IProjectService projectService)
		{
			_testimonialService = testimonialService;
			_projectService = projectService;
		}

		public ActionResult Index()
		{
			return View();
		}
        [Route("Testimonials")]
		public ActionResult Testimonials()
		{
			var viewModel = Mapper.Map<List<Testimonial>, List<TestimonialViewModel>>(_testimonialService.GetAll());

			return View(viewModel);
		}
        [Route("Projects")]
		public ActionResult Projects()
		{
			var viewModel = Mapper.Map<List<Project>, List<ProjectViewModel>>(_projectService.GetAll());

			return View(viewModel);
		}
	}
}