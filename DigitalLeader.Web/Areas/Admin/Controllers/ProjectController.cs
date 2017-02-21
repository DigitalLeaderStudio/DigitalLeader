namespace DigitalLeader.Web.Areas.Admin.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Entities.Identity;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.ViewModels;
	using System;
	using System.Collections.Generic;
	using System.Web.Mvc;

	public class ProjectController : BaseAdminController
	{
		private IProjectService _projectService;
		private IClientService _clientService;

		public ProjectController(IProjectService projectService, IClientService clientService)
		{
			_projectService = projectService;
			_clientService = clientService;
		}

		// GET: Admin/Project
		public ActionResult Index()
		{
			var viewModel = Mapper.Map<List<Project>, List<ProjectViewModel>>(_projectService.GetAll());

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

					_projectService.Insert(project);

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
				viewModel.Clients = Mapper.Map<List<Client>, List<SelectListItem>>(_clientService.GetAll());
			}

			return View(viewModel);
		}

		public ActionResult Edit(int id)
		{
			var viewModel = Mapper.Map<Project, ProjectViewModel>(_projectService.GetById(id));
			viewModel.Clients = Mapper.Map<List<Client>, List<SelectListItem>>(_clientService.GetAll());

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Edit(ProjectViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var project = Mapper.Map<ProjectViewModel, Project>(viewModel);

					_projectService.Update(project);

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
				viewModel.Clients = Mapper.Map<List<Client>, List<SelectListItem>>(_clientService.GetAll());
			}

			return View(viewModel);
		}
	}
}