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
		private IServiceSubcategoryService _subcategoryService;
		private IServiceService _serviceService;
		private IProjectService _projectService;
		private IClientService _clientService;

		public ServiceController(
			ITechnologyService technologyService,
			IServiceSubcategoryService subcategoryService,
			IServiceService serviceService,
			IProjectService projectService,
			IClientService clientService)
		{
			_technologyService = technologyService;
			_subcategoryService = subcategoryService;
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

			viewModel.ServiceSubcategoriesSelectList = Mapper.Map<List<ServiceSubcategory>, List<SelectListItem>>(_subcategoryService.GetAll());
			return View(viewModel);
		}

		public ActionResult Edit(int id)
		{
			var viewModel = Mapper.Map<Service, ServiceViewModel>(_serviceService.GetById(id));
			viewModel.ServiceSubcategoriesSelectList = Mapper.Map<List<ServiceSubcategory>, List<SelectListItem>>(_subcategoryService.GetAll());

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