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
		[Route("Portfolio/Testimonials")]
		public ActionResult Testimonials()
		{
			var viewModel = Mapper.Map<List<Testimonial>, List<TestimonialViewModel>>(_testimonialService.GetAll());

			return View(viewModel);
		}
		[Route("Portfolio/Projects")]
		public ActionResult Projects()
		{
			var projects = _projectService.GetAll();

			var viewModel = Mapper.Map<List<Project>, List<ProjectViewModel>>(projects);

			return View(viewModel);
		}

		[Route("Portfolio/Case-studies")]
		public ActionResult CaseStudies()
		{
			var viewModel = Mapper.Map<List<Project>, List<ProjectViewModel>>(_projectService.GetAllCaseStudies());

			return View(viewModel);
		}

		[Route("Portfolio/Project/{id}")]
		public ActionResult Project(int id)
		{
			var viewModel = Mapper.Map<Project, ProjectViewModel>(_projectService.GetById(id));

			return View(viewModel);
		}
	}
}