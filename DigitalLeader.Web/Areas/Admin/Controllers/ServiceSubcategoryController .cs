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

	public class ServiceSubcategoryController : BaseAdminController
	{
		private readonly ILocalizedEntityService _localizedEntityService;
		private readonly IServiceSubcategoryService _subcategoryService;
		private readonly IServiceCategoryService _categoryService;

		public ServiceSubcategoryController(
			ILocalizedEntityService localizedEntityService,
			IServiceSubcategoryService subcategoryService,
			IServiceCategoryService categoryService)
		{
			_localizedEntityService = localizedEntityService;
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

			AddLocales(viewModel.Locales, (locale, languageId) => { });

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
					var entity = Mapper.Map<ServiceSubcategoryViewModel, ServiceSubcategory>(viewModel);

					_subcategoryService.Insert(entity);

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Name, l.Name, l.LanguageId);
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

		// GET: Admin/ServiceSubcategory/Edit
		public ActionResult Edit(int id)
		{
			var entity = _subcategoryService.GetById(id);
			var viewModel = Mapper.Map<ServiceSubcategory, ServiceSubcategoryViewModel>(entity);

			viewModel.ServiceCategoriesSelectList = Mapper.Map<List<ServiceCategory>, List<SelectListItem>>(_categoryService.GetAll());

			AddLocales(viewModel.Locales, (locale, languageId) =>
			{
				locale.Name = entity.GetLocalized(x => x.Name, languageId);
			});

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
					var entity = Mapper.Map<ServiceSubcategoryViewModel, ServiceSubcategory>(viewModel);

					_subcategoryService.Update(entity);

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Name, l.Name, l.LanguageId);
					});
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