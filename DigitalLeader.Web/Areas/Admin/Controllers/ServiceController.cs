namespace DigitalLeader.Web.Areas.Admin.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.ViewModels;
	using System;
	using System.Collections.Generic;
	using System.Web.Mvc;

	public class ServiceController : BaseAdminController
	{
		private ITechnologyService _technologyService;
		private ICategoryService _categoryService;
		private IServiceService _serviceService;
		private IProjectService _projectService;
		private IClientService _clientService;

		public ServiceController(
			ITechnologyService technologyService,
			ICategoryService categoryService,
			IServiceService serviceService,
			IProjectService projectService,
			IClientService clientService)
		{
			_technologyService = technologyService;
			_categoryService = categoryService;
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
				CategoriesSelectList = Mapper.Map<List<Category>, List<SelectListItem>>(_categoryService.GetAll())
			};

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

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
			}

			viewModel.CategoriesSelectList = Mapper.Map<List<Category>, List<SelectListItem>>(_categoryService.GetAll());
			return View(viewModel);
		}

		public ActionResult Edit(int id)
		{
			var viewModel = Mapper.Map<Service, ServiceViewModel>(_serviceService.GetById(id));
			viewModel.CategoriesSelectList = Mapper.Map<List<Category>, List<SelectListItem>>(_categoryService.GetAll());

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
	}
}