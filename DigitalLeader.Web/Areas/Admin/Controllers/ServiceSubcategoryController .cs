namespace DigitalLeader.Web.Areas.Admin.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.ViewModels;
	using System;
	using System.Collections.Generic;
	using System.Web.Mvc;

	public class ServiceSubcategoryController : BaseAdminController
	{
        private IServiceCategoryService _categoryService;
        private IServiceSubcategoryService _subcategoryService;

		public ServiceSubcategoryController(IServiceSubcategoryService subcategoryService, IServiceCategoryService categoryService)
		{
			_subcategoryService = subcategoryService;
            _categoryService = categoryService;
		}

		// GET: Admin/ServiceSubcategory
		public ActionResult Index()
		{
			//var viewModel = Mapper.Map<IEnumerable<Client>, List<ClientViewModel>>(_clientService.GetAll());
			var viewModel = Mapper.Map<List<ServiceSubcategory>, List<ServiceSubcategoryViewModel>>(_subcategoryService.GetAll());

			return View(viewModel);
		}

        // GET: Admin/ServiceSubCategory/Create
        public ActionResult Create()
        {
            var viewModel = new ServiceSubcategoryViewModel
            {
                ServiceCategoriesSelectList = Mapper.Map<List<ServiceCategory>, List<SelectListItem>>(_categoryService.GetAll())
            };

            return View(viewModel);
        }

       // POST: Admin/ServiceSubCategory/Create
       [HttpPost]
        public ActionResult Create(ServiceSubcategoryViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var serviceSubcategoory = Mapper.Map<ServiceSubcategoryViewModel, ServiceSubcategory>(viewModel);

                    _subcategoryService.Insert(serviceSubcategoory);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View(viewModel);
        }

        // GET: Admin/ServiceSubcategory/Edit
        public ActionResult Edit(int id)
		{
			//var viewModel = Mapper.Map<Client, ClientViewModel>(_clientService.GetById(id));
			var viewModel = Mapper.Map<ServiceSubcategory, ServiceSubcategoryViewModel>(_subcategoryService.GetById(id));

            viewModel.ServiceCategoriesSelectList = Mapper.Map<List<ServiceCategory>, List<SelectListItem>>(_categoryService.GetAll());

            return View(viewModel);
		}

		// POST: Admin/ServiceSubcategory/Edit
		[HttpPost]
		public ActionResult Edit(ServiceSubcategoryViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var client = Mapper.Map<ServiceSubcategoryViewModel, ServiceSubcategory>(viewModel);

					_subcategoryService.Update(client);
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);

				return View(viewModel);
			}

			return RedirectToAction("Index");
		}

        // GET: Admin/ServiceSubCategory/Details
        public ActionResult Details(int id)
        {
            var viewModel = Mapper.Map<ServiceSubcategory, ServiceSubcategoryViewModel>(_subcategoryService.GetById(id));

            return View(viewModel);
        }

        // GET: Admin/ServiceSubcategory/Delete
        public ActionResult Delete(int id)
        {
            var viewModel = Mapper.Map<ServiceSubcategory, ServiceSubcategoryViewModel>(_subcategoryService.GetById(id));

            return View(viewModel);
        }

        // POST: Admin/ServiceSubcategory/Delete
        [HttpPost]
        public ActionResult Delete(ServiceSubcategoryViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = Mapper.Map<ServiceSubcategoryViewModel, ServiceSubcategory>(viewModel);

                    _subcategoryService.Delete(entity);

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