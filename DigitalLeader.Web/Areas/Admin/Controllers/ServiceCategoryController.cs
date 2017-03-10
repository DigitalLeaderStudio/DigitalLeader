namespace DigitalLeader.Web.Areas.Admin.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.ViewModels;
	using System;
	using System.Collections.Generic;
	using System.Web.Mvc;

	public class ServiceCategoryController : BaseAdminController
	{
		private IServiceCategoryService _categoryService;

		public ServiceCategoryController(IServiceCategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		// GET: Admin/Category
		public ActionResult Index()
		{
			//var viewModel = Mapper.Map<IEnumerable<Client>, List<ClientViewModel>>(_clientService.GetAll());
			var viewModel = Mapper.Map<List<ServiceCategory>, List<ServiceCategoryViewModel>>(_categoryService.GetAll());

			return View(viewModel);
		}

        // GET: Admin/ServiceCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Admin/ServiceCategory/Create
       [HttpPost]
        public ActionResult Create(ServiceCategoryViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var serviceCategory = Mapper.Map<ServiceCategoryViewModel, ServiceCategory>(viewModel);

                    _categoryService.Insert(serviceCategory);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View(viewModel);
        }

        // GET: Admin/ServiceCategory/Edit
        public ActionResult Edit(int id)
		{
			//var viewModel = Mapper.Map<Client, ClientViewModel>(_clientService.GetById(id));
			var viewModel = Mapper.Map<ServiceCategory, ServiceCategoryViewModel>(_categoryService.GetById(id));

			return View(viewModel);
		}

		// POST: Admin/ServiceCategory/Edit
		[HttpPost]
		public ActionResult Edit(ServiceCategoryViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var categoryService = Mapper.Map<ServiceCategoryViewModel, ServiceCategory>(viewModel);

					_categoryService.Update(categoryService);
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);

				return View(viewModel);
			}

			return RedirectToAction("Index");
		}

        // GET: Admin/ServiceCategory/Details
        public ActionResult Details(int id)
        {
            var viewModel = Mapper.Map<ServiceCategory, ServiceCategoryViewModel>(_categoryService.GetById(id));

            return View(viewModel);
        }

        // GET: Admin/ServiceCategory/Delete
        public ActionResult Delete(int id)
        {
            var viewModel = Mapper.Map<ServiceCategory, ServiceCategoryViewModel>(_categoryService.GetById(id));

            return View(viewModel);
        }

        // POST: Admin/ServiceCategory/Delete
        [HttpPost]
        public ActionResult Delete(ServiceCategoryViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = Mapper.Map<ServiceCategoryViewModel, ServiceCategory>(viewModel);

                    _categoryService.Delete(entity);

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