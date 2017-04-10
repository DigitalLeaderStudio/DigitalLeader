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

	public class ContentController : BaseAdminController
	{
		private readonly ILocalizedEntityService _localizedEntityService;
		private readonly IContentService _contentService;
		private readonly IServiceService _serviceService;

		public ContentController(
			ILocalizedEntityService localizedEntityService,
			IServiceService serviceService,
			IContentService contetnService)
		{
			_localizedEntityService = localizedEntityService;
			_serviceService = serviceService;
			_contentService = contetnService;
		}

		public ActionResult Index()
		{
			var viewModel = Mapper.Map<List<Content>, List<ContentViewModel>>(_contentService.GetAll());

			return View(viewModel);
		}

		public ActionResult Details(int id)
		{
			var viewModel = Mapper.Map<Content, ContentViewModel>(_contentService.GetById(id));

			return View(viewModel);
		}

		public ActionResult Create()
		{
			var viewModel = new ContentViewModel();

			AddLocales(viewModel.Locales, (locale, languageId) => { });

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Create(ContentViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<ContentViewModel, Content>(viewModel);

					entity.CreatedDate = DateTime.UtcNow;

					_contentService.Insert(entity);

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Description, l.Description, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Keywords, l.Keywords, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Html, l.Html, l.LanguageId);
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

		public ActionResult Edit(int id)
		{
			var entity = _contentService.GetById(id);

			var viewModel = Mapper.Map<Content, ContentViewModel>(entity);

			AddLocales(viewModel.Locales, (locale, languageId) =>
			{
				locale.Keywords = entity.GetLocalized(x => x.Keywords, languageId);
				locale.Description = entity.GetLocalized(x => x.Description, languageId);
				locale.Html = entity.GetLocalized(x => x.Html, languageId);
			});

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Edit(ContentViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<ContentViewModel, Content>(viewModel);

					_contentService.Update(entity);

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Description, l.Description, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Keywords, l.Keywords, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Html, l.Html, l.LanguageId);
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

		public ActionResult Delete(int id)
		{
			var viewModel = Mapper.Map<Content, ContentViewModel>(_contentService.GetById(id));

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Delete(ContentViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<ContentViewModel, Content>(viewModel);

					_contentService.Delete(entity);

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