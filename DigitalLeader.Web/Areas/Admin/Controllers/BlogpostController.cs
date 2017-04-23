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

	public class BlogpostController : BaseAdminController
	{
		private readonly ILocalizedEntityService _localizedEntityService;
		private readonly IBlogpostService _blogpostService;
		private readonly IServiceService _serviceService;

		public BlogpostController(
			ILocalizedEntityService localizedEntityService,
			IServiceService serviceService,
			IBlogpostService blogpostService)
		{
			_localizedEntityService = localizedEntityService;
			_serviceService = serviceService;
			_blogpostService = blogpostService;
		}

		// GET: Admin/Blogpost
		public ActionResult Index()
		{
			var viewModel = Mapper.Map<List<Blogpost>, List<BlogpostViewModel>>(_blogpostService.GetAll());

			return View(viewModel);
		}

		// GET: Admin/Blogpost/Details
		public ActionResult Details(int id)
		{
			var viewModel = Mapper.Map<Blogpost, BlogpostViewModel>(_blogpostService.GetById(id));

			return View(viewModel);
		}

		// GET: Admin/Blogpost/Create
		public ActionResult Create()
		{
			var viewModel = new BlogpostViewModel
			{
				ServicesSelectList = Mapper.Map<List<Service>, List<SelectListItem>>(_serviceService.GetAll()),
				PublishedDate = DateTime.Now
			};

			AddLocales(viewModel.Locales, (locale, languageId) => { });

			return View(viewModel);
		}

		// POST: Admin/Project/Create
		[HttpPost]
		public ActionResult Create(BlogpostViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<BlogpostViewModel, Blogpost>(viewModel);

					entity.AuthorId = base.LoggetUserID;

					_blogpostService.Insert(entity);

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Title, l.Title, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Overview, l.Overview, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Keywords, l.Keywords, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Content, l.Content, l.LanguageId);
					});

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
			}

			viewModel.ServicesSelectList = Mapper.Map<List<Service>, List<SelectListItem>>(_serviceService.GetAll());

			return View(viewModel);
		}

		// GET: Admin/Project/Edit
		public ActionResult Edit(int id)
		{
			var entity = _blogpostService.GetById(id);
			var viewModel = Mapper.Map<Blogpost, BlogpostViewModel>(entity);
			viewModel.ServicesSelectList = Mapper.Map<List<Service>, List<SelectListItem>>(_serviceService.GetAll());

			AddLocales(viewModel.Locales, (locale, languageId) =>
			{
				locale.Title = entity.GetLocalized(x => x.Title, languageId);
				locale.Keywords = entity.GetLocalized(x => x.Keywords, languageId);
				locale.Overview = entity.GetLocalized(x => x.Overview, languageId);
				locale.Content = entity.GetLocalized(x => x.Content, languageId);
			});

			return View(viewModel);
		}

		// POST: Admin/Project/Edit
		[HttpPost]
		public ActionResult Edit(BlogpostViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<BlogpostViewModel, Blogpost>(viewModel);

					_blogpostService.Update(entity);

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Title, l.Title, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Keywords, l.Keywords, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Overview, l.Overview, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Content, l.Content, l.LanguageId);
					});

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
			}

			viewModel.ServicesSelectList = Mapper.Map<List<Service>, List<SelectListItem>>(_serviceService.GetAll());

			return View(viewModel);
		}

		// GET: Admin/Blogpost/Delete
		public ActionResult Delete(int id)
		{
			var viewModel = Mapper.Map<Blogpost, BlogpostViewModel>(_blogpostService.GetById(id));

			return View(viewModel);
		}

		// POST: Admin/Blogpost/Delete
		[HttpPost]
		public ActionResult Delete(BlogpostViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<BlogpostViewModel, Blogpost>(viewModel);

					_blogpostService.Delete(entity);

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