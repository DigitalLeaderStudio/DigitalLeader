namespace DigitalLeader.Web.Areas.Admin.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.Services.Localization;
	using DigitalLeader.ViewModels;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Mvc;
	using System.Web.Routing;

	public class SEOController : BaseAdminController
	{
		private readonly ILocalizedEntityService _localizedEntityService;
		private readonly ISEOService _seoService;

		public SEOController(
			ILocalizedEntityService localizedEntityService,
			ISEOService seoService)
		{
			_localizedEntityService = localizedEntityService;
			_seoService = seoService;
		}

		// GET: Admin/SEO
		public ActionResult Index()
		{
			var viewModel = Mapper.Map<List<SEO>, List<SEOViewModel>>(_seoService.GetAll());

			return View(viewModel);
		}

		// GET: Admin/SEO/Details
		public ActionResult Details(int id)
		{
			var viewModel = Mapper.Map<SEO, SEOViewModel>(_seoService.GetById(id));

			return View(viewModel);
		}

		// GET: Admin/SEO/Create
		public ActionResult Create()
		{
			var viewModel = new SEOViewModel();

			viewModel.SEOKeysSelectList = GetSelectListForKeys();

			AddLocales(viewModel.Locales, (locale, languageId) => { });

			return View(viewModel);
		}

		// POST: Admin/SEO/Create
		[HttpPost]
		public ActionResult Create(SEOViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<SEOViewModel, SEO>(viewModel);

					_seoService.Insert(entity);

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Description, l.Description, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Keywords, l.Keywords, l.LanguageId);
					});

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
			}

			viewModel.SEOKeysSelectList = GetSelectListForKeys();

			return View(viewModel);
		}

		// GET: Admin/SEO/Edit
		public ActionResult Edit(int id)
		{
			var entity = _seoService.GetById(id);
			var viewModel = Mapper.Map<SEO, SEOViewModel>(entity);

			viewModel.SEOKeysSelectList = GetSelectListForKeys();

			AddLocales(viewModel.Locales, (locale, languageId) =>
			{
				locale.Keywords = entity.GetLocalized(x => x.Keywords, languageId);
				locale.Description = entity.GetLocalized(x => x.Description, languageId);
			});

			return View(viewModel);
		}

		// POST: Admin/SEO/Edit
		[HttpPost]
		public ActionResult Edit(SEOViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<SEOViewModel, SEO>(viewModel);

					_seoService.Update(entity);

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Description, l.Description, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Keywords, l.Keywords, l.LanguageId);
					});

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
			}

			viewModel.SEOKeysSelectList = GetSelectListForKeys();
			
			return View(viewModel);
		}

		// GET: Admin/SEO/Delete
		public ActionResult Delete(int id)
		{
			var viewModel = Mapper.Map<SEO, SEOViewModel>(_seoService.GetById(id));

			return View(viewModel);
		}

		// POST: Admin/SEO/Delete
		[HttpPost]
		public ActionResult Delete(SEOViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<SEOViewModel, SEO>(viewModel);

					_seoService.Delete(entity);

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
			}

			return View(viewModel);
		}

		private List<SelectListItem> GetSelectListForKeys()
		{
			var selectList = new List<SelectListItem>();

			var routes = RouteTable.Routes
				.Where(r => r.GetType().ToString().Contains("LinkGenerationRoute"))
				.Select(r =>
				{
					var route = r as Route;

					return string.Format("{0}-{1}",
						route.Defaults["controller"],
						route.Defaults["action"]);
				}).ToList();

			routes.ForEach(r =>
			{
				selectList.Add(new SelectListItem
				{
					Text = r,
					Value = r
				});
			});

			return selectList;
		}
	}
}