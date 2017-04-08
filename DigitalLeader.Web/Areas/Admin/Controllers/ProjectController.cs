namespace DigitalLeader.Web.Areas.Admin.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Entities.Identity;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.Services.Localization;
	using DigitalLeader.ViewModels;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Mvc;

	public class ProjectController : BaseAdminController
	{
		private readonly ILocalizedEntityService _localizedEntityService;
		private readonly ITechnologyService _technologyService;
		private readonly IServiceService _serviceService;
		private readonly IProjectService _projectService;
		private readonly IClientService _clientService;
		private readonly IUserService _userService;

		public ProjectController(
			ILocalizedEntityService localizedEntityService,
			ITechnologyService technologyService,
			IServiceService serviceService,
			IProjectService projectService,
			IClientService clientService,
			IUserService userService)
		{
			_localizedEntityService = localizedEntityService;
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
				ClientsSelectList = Mapper.Map<List<Client>, List<SelectListItem>>(_clientService.GetAll()),
				StartDate = DateTime.Now,
				EndDate = DateTime.Now
			};
			viewModel.TechnologiesSelectList = Mapper.Map<List<Technology>, List<SelectListItem>>(_technologyService.GetAll());
			viewModel.ContributorsSelectList = Mapper.Map<List<User>, List<SelectListItem>>(_userService.GetAll());
			viewModel.ServicesSelectList = Mapper.Map<List<Service>, List<SelectListItem>>(_serviceService.GetAll());

			AddLocales(viewModel.Locales, (locale, languageId) => { });

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

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(project, e => e.Title, l.Title, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(project, e => e.Kewywords, l.Kewywords, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(project, e => e.Overview, l.Overview, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(project, e => e.Objective, l.Objective, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(project, e => e.WorkOverview, l.WorkOverview, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(project, e => e.ResultOverview, l.ResultOverview, l.LanguageId);
					});

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
				viewModel.ClientsSelectList = Mapper.Map<List<Client>, List<SelectListItem>>(_clientService.GetAll());
				viewModel.TechnologiesSelectList = Mapper.Map<List<Technology>, List<SelectListItem>>(_technologyService.GetAll());
				viewModel.ContributorsSelectList = Mapper.Map<List<User>, List<SelectListItem>>(_userService.GetAll());
				viewModel.ServicesSelectList = Mapper.Map<List<Service>, List<SelectListItem>>(_serviceService.GetAll());
			}

			viewModel.ClientsSelectList = Mapper.Map<List<Client>, List<SelectListItem>>(_clientService.GetAll());
			viewModel.TechnologiesSelectList = Mapper.Map<List<Technology>, List<SelectListItem>>(_technologyService.GetAll());
			viewModel.ContributorsSelectList = Mapper.Map<List<User>, List<SelectListItem>>(_userService.GetAll());
			viewModel.ServicesSelectList = Mapper.Map<List<Service>, List<SelectListItem>>(_serviceService.GetAll());

			return View(viewModel);
		}

		// GET: Admin/Project/Edit
		public ActionResult Edit(int id)
		{
			var entity = _projectService.GetById(id);
			var viewModel = Mapper.Map<Project, ProjectViewModel>(entity);

			viewModel.ClientsSelectList = Mapper.Map<List<Client>, List<SelectListItem>>(_clientService.GetAll());

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

			AddLocales(viewModel.Locales, (locale, languageId) =>
			{
				locale.Title = entity.GetLocalized(x => x.Title, languageId);
				locale.Kewywords = entity.GetLocalized(x => x.Kewywords, languageId);
				locale.Overview = entity.GetLocalized(x => x.Overview, languageId);
				locale.Objective = entity.GetLocalized(x => x.Objective, languageId);
				locale.WorkOverview = entity.GetLocalized(x => x.WorkOverview, languageId);
				locale.ResultOverview = entity.GetLocalized(x => x.ResultOverview, languageId);
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

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(project, e => e.Title, l.Title, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(project, e => e.Kewywords, l.Kewywords, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(project, e => e.Overview, l.Overview, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(project, e => e.Objective, l.Objective, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(project, e => e.WorkOverview, l.WorkOverview, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(project, e => e.ResultOverview, l.ResultOverview, l.LanguageId);
					});

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);

				viewModel = Mapper.Map<Project, ProjectViewModel>(_projectService.GetById(viewModel.ID));
				viewModel.ClientsSelectList = Mapper.Map<List<Client>, List<SelectListItem>>(_clientService.GetAll());

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
			viewModel.ClientsSelectList = Mapper.Map<List<Client>, List<SelectListItem>>(_clientService.GetAll());
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

		// GET: Admin/Project/Details
		public ActionResult Details(int id)
		{
			var viewModel = Mapper.Map<Project, ProjectViewModel>(_projectService.GetById(id));

			return View(viewModel);
		}
		// GET: Admin/Project/Delete
		public ActionResult Delete(int id)
		{
			var viewModel = Mapper.Map<Project, ProjectViewModel>(_projectService.GetById(id));

			return View(viewModel);
		}

		// POST: Admin/Project/Delete
		[HttpPost]
		public ActionResult Delete(ProjectViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<ProjectViewModel, Project>(viewModel);

					_projectService.Delete(entity);

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
			}

			return View(viewModel);
		}
	}


}