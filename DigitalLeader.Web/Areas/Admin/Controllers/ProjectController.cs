namespace DigitalLeader.Web.Areas.Admin.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Entities.Identity;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.ViewModels;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Mvc;

	public class ProjectController : BaseAdminController
	{
		private ITechnologyService _technologyService;
		private IServiceService _serviceService;
		private IProjectService _projectService;
		private IClientService _clientService;
		private IUserService _userService;

		public ProjectController(
			ITechnologyService technologyService,
			IServiceService serviceService,
			IProjectService projectService,
			IClientService clientService,
			IUserService userService)
		{
			_technologyService = technologyService;
			_serviceService = serviceService;
			_projectService = projectService;
			_clientService = clientService;
			_userService = userService;
		}

		// GET: Admin/Project
		public ActionResult Index()
		{
			var projects = _projectService.GetAll();
			var viewModel = Mapper.Map<List<Project>, List<ProjectViewModel>>(projects);

			return View(viewModel);
		}

		// GET: Admin/Project/Create
		public ActionResult Create()
		{
			var viewModel = new ProjectViewModel
			{
				Clients = Mapper.Map<List<Client>, List<SelectListItem>>(_clientService.GetAll()),
				StartDate = DateTime.Now,
				EndDate = DateTime.Now
			};
			viewModel.TechnologiesSelectList = Mapper.Map<List<Technology>, List<SelectListItem>>(_technologyService.GetAll());
			viewModel.ContributorsSelectList = Mapper.Map<List<User>, List<SelectListItem>>(_userService.GetAll());
			viewModel.ServicesSelectList = Mapper.Map<List<Service>, List<SelectListItem>>(_serviceService.GetAll());

			return View(viewModel);
		}

		// POST: Admin/Project/Create
		[HttpPost]
		public ActionResult Create(ProjectViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var project = Mapper.Map<ProjectViewModel, Project>(viewModel);

					project.Technologies = viewModel.TechnologiesIds != null ?
						_technologyService.GetByIds(viewModel.TechnologiesIds) : new List<Technology>();

					project.Services = viewModel.ServicesIds != null ?
						_serviceService.GetByIds(viewModel.ServicesIds) : new List<Service>();

					project.Contributors = viewModel.ContributorsIds != null ?
						_userService.GetByIds(viewModel.ContributorsIds) : new List<User>();

					_projectService.Insert(project);

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
				viewModel.Clients = Mapper.Map<List<Client>, List<SelectListItem>>(_clientService.GetAll());
				viewModel.TechnologiesSelectList = Mapper.Map<List<Technology>, List<SelectListItem>>(_technologyService.GetAll());
				viewModel.ContributorsSelectList = Mapper.Map<List<User>, List<SelectListItem>>(_userService.GetAll());
				viewModel.ServicesSelectList = Mapper.Map<List<Service>, List<SelectListItem>>(_serviceService.GetAll());
			}

			return View(viewModel);
		}

		// GET: Admin/Project/Edit
		public ActionResult Edit(int id)
		{
			var viewModel = Mapper.Map<Project, ProjectViewModel>(_projectService.GetById(id));
			viewModel.Clients = Mapper.Map<List<Client>, List<SelectListItem>>(_clientService.GetAll());

			viewModel.ServicesSelectList = Mapper.Map<List<Service>, List<SelectListItem>>(_serviceService.GetAll());
			viewModel.ServicesSelectList.ForEach(item =>
			{
				item.Selected = viewModel.ServicesIds.Contains(int.Parse(item.Value));
			});

			viewModel.TechnologiesSelectList = Mapper.Map<List<Technology>, List<SelectListItem>>(_technologyService.GetAll());
			viewModel.TechnologiesSelectList.ForEach(item =>
			{
				item.Selected = viewModel.TechnologiesIds.Contains(int.Parse(item.Value));
			});

			viewModel.ContributorsSelectList = Mapper.Map<List<User>, List<SelectListItem>>(_userService.GetAll());
			viewModel.ContributorsSelectList.ForEach(item =>
			{
				item.Selected = viewModel.ContributorsIds.Contains(int.Parse(item.Value));
			});

			return View(viewModel);
		}

		// POST: Admin/Project/Edit
		[HttpPost]
		public ActionResult Edit(ProjectViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var project = Mapper.Map<ProjectViewModel, Project>(viewModel);

					project.Technologies = viewModel.TechnologiesIds != null ?
						_technologyService.GetByIds(viewModel.TechnologiesIds) :
						new List<Technology>();

					project.Services = viewModel.ServicesIds != null ?
						_serviceService.GetByIds(viewModel.ServicesIds) :
						new List<Service>();

					project.Contributors = viewModel.ContributorsIds != null ?
						_userService.GetByIds(viewModel.ContributorsIds) :
						new List<User>();

					_projectService.Update(project);

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);

				viewModel = Mapper.Map<Project, ProjectViewModel>(_projectService.GetById(viewModel.ID));
				viewModel.Clients = Mapper.Map<List<Client>, List<SelectListItem>>(_clientService.GetAll());

				viewModel.ServicesSelectList = Mapper.Map<List<Service>, List<SelectListItem>>(_serviceService.GetAll());
				viewModel.ServicesSelectList.ForEach(item =>
				{
					item.Selected = viewModel.ServicesIds.Contains(int.Parse(item.Value));
				});

				viewModel.TechnologiesSelectList = Mapper.Map<List<Technology>, List<SelectListItem>>(_technologyService.GetAll());
				viewModel.TechnologiesSelectList.ForEach(item =>
				{
					item.Selected = viewModel.TechnologiesIds.Contains(int.Parse(item.Value));
				});
			}

			return View(viewModel);
		}

		// GET: Admin/Project/Details
		public ActionResult Details(int id)
		{
			var viewModel = Mapper.Map<Project, ProjectViewModel>(_projectService.GetById(id));

			return View(viewModel);
		}
	}
}