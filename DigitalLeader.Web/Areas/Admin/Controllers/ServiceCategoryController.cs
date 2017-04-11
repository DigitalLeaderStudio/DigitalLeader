namespace DigitalLeader.Web.Areas.Admin.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.ViewModels;
	using System;
	using System.Collections.Generic;
	using System.Web.Mvc;
	using System.Linq;
	using DigitalLeader.Services.Localization;

	public class ServiceCategoryController : BaseAdminController
	{
		private readonly ILocalizedEntityService _localizedEntityService;
		private readonly IServiceCategoryService _categoryService;

		public ServiceCategoryController(
			ILocalizedEntityService localizedEntityService,
			IServiceCategoryService categoryService)
		{
			_localizedEntityService = localizedEntityService;
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
			var viewModel = new ContentViewModel();

			AddLocales(viewModel.Locales, (locale, languageId) => { });

			return View(viewModel);
		}

		//POST: Admin/ServiceCategory/Create
		[HttpPost]
		public ActionResult Create(ServiceCategoryViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<ServiceCategoryViewModel, ServiceCategory>(viewModel);

					_categoryService.Insert(entity);

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Name, l.Name, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Content, l.Content, l.LanguageId);
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

		// GET: Admin/ServiceCategory/Edit
		public ActionResult Edit(int id)
		{
			var entity = _categoryService.GetById(id);
			var viewModel = Mapper.Map<ServiceCategory, ServiceCategoryViewModel>(entity);

			AddLocales(viewModel.Locales, (locale, languageId) =>
			{
				locale.Name = entity.GetLocalized(x => x.Name, languageId);
				locale.Content = entity.GetLocalized(x => x.Content, languageId);
			});

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
					var entity = Mapper.Map<ServiceCategoryViewModel, ServiceCategory>(viewModel);

					_categoryService.Update(entity);

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Name, l.Name, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Content, l.Content, l.LanguageId);
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