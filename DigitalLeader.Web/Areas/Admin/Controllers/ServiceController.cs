namespace DigitalLeader.Web.Areas.Admin.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.Services.Localization;
	using DigitalLeader.ViewModels;
	using System;
	using System.Collections.Generic;
	using System.Web.Mvc;
	using System.Linq;

	public class ServiceController : BaseAdminController
	{
		private readonly ILocalizedEntityService _localizedEntityService;
		private readonly IServiceSubcategoryService _subcategoryService;
		private readonly ITechnologyService _technologyService;
		private readonly IServiceService _serviceService;
		private readonly IProjectService _projectService;
		private readonly IClientService _clientService;

		public ServiceController(
			ILocalizedEntityService localizedEntityService,
			IServiceSubcategoryService subcategoryService,
			ITechnologyService technologyService,
			IServiceService serviceService,
			IProjectService projectService,
			IClientService clientService)
		{
			_localizedEntityService = localizedEntityService;
			_subcategoryService = subcategoryService;
			_technologyService = technologyService;
			_serviceService = serviceService;
			_projectService = projectService;
			_clientService = clientService;
		}

		// GET: Admin/Service
		public ActionResult Index()
		{
			var projects = _serviceService.GetAll();
			var viewModel = Mapper.Map<List<Service>, List<ServiceViewModel>>(projects);

			return View(viewModel);
		}

		// GET: Admin/Service/Create
		public ActionResult Create()
		{
			var viewModel = new ServiceViewModel
			{
				ServiceSubcategoriesSelectList = Mapper.Map<List<ServiceSubcategory>, List<SelectListItem>>(_subcategoryService.GetAll())
			};

			AddLocales(viewModel.Locales, (locale, languageId) => { });

			return View(viewModel);
		}

		// POST: Admin/Service/Create
		[HttpPost]
		public ActionResult Create(ServiceViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var service = Mapper.Map<ServiceViewModel, Service>(viewModel);

					_serviceService.Insert(service);

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(service, e => e.Title, l.Title, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(service, e => e.SubTitle, l.SubTitle, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(service, e => e.Description, l.Description, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(service, e => e.Content, l.Content, l.LanguageId);
					});

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
			}

			viewModel.ServiceSubcategoriesSelectList = Mapper.Map<List<ServiceSubcategory>, List<SelectListItem>>(_subcategoryService.GetAll());
			return View(viewModel);
		}

		public ActionResult Edit(int id)
		{
			var entity = _serviceService.GetById(id);
			var viewModel = Mapper.Map<Service, ServiceViewModel>(entity);
			viewModel.ServiceSubcategoriesSelectList = Mapper.Map<List<ServiceSubcategory>, List<SelectListItem>>(_subcategoryService.GetAll());

			AddLocales(viewModel.Locales, (locale, languageId) =>
			{
				locale.Title = entity.GetLocalized(x => x.Title, languageId);
				locale.SubTitle = entity.GetLocalized(x => x.SubTitle, languageId);
				locale.Description = entity.GetLocalized(x => x.Description, languageId);
				locale.Content = entity.GetLocalized(x => x.Content, languageId);
			});

			return View(viewModel);
		}

		// POST: Admin/Service/Edit
		[HttpPost]
		public ActionResult Edit(ServiceViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var service = Mapper.Map<ServiceViewModel, Service>(viewModel);

					_serviceService.Update(service);

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(service, e => e.Title, l.Title, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(service, e => e.SubTitle, l.SubTitle, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(service, e => e.Description, l.Description, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(service, e => e.Content, l.Content, l.LanguageId);
					});

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
			}

			return View(viewModel);
		}

		// GET: Admin/Project/Details
		public ActionResult Details(int id)
		{
			var viewModel = Mapper.Map<Service, ServiceViewModel>(_serviceService.GetById(id));

			return View(viewModel);
		}

		// GET: Admin/Service/Delete
		public ActionResult Delete(int id)
		{
			var viewModel = Mapper.Map<Service, ServiceViewModel>(_serviceService.GetById(id));

			return View(viewModel);
		}

		// POST: Admin/Service/Delete
		[HttpPost]
		public ActionResult Delete(ServiceViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<ServiceViewModel, Service>(viewModel);

					_serviceService.Delete(entity);

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