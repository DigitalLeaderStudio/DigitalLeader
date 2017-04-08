using AutoMapper;
using DigitalLeader.Entities;
using DigitalLeader.Services.Interfaces;
using DigitalLeader.Services.Localization;
using DigitalLeader.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalLeader.Web.Areas.Admin.Controllers
{
	public class VacancyController : BaseAdminController
	{
		private readonly ILocalizedEntityService _localizedEntityService;
		private ITechnologyService _technologyService;
		private IVacancyService _vacancyService;

		public VacancyController(
			ILocalizedEntityService localizedEntityService,
			IVacancyService vacancyService,
			ITechnologyService technologyService)
		{
			_localizedEntityService = localizedEntityService;
			_technologyService = technologyService;
			_vacancyService = vacancyService;
		}


		// GET: Admin/Vacancy
		public ActionResult Index()
		{
			var vacancies = _vacancyService.GetAll();
			var viewModel = Mapper.Map<List<Vacancy>, List<VacancyViewModel>>(vacancies);

			return View(viewModel);
		}

		// GET: Admin/Vacancy/Create
		public ActionResult Create()
		{
			var viewModel = new VacancyViewModel
			{
				CreatedDate = DateTime.Now,
			};
			viewModel.TechnologiesSelectList = Mapper.Map<List<Technology>, List<SelectListItem>>(_technologyService.GetAll());

			AddLocales(viewModel.Locales, (locale, languageId) => { });

			return View(viewModel);
		}

		// POST: Admin/Vacancy/Create
		[HttpPost]
		public ActionResult Create(VacancyViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var vacancy = Mapper.Map<VacancyViewModel, Vacancy>(viewModel);

					vacancy.Technologies = viewModel.TechnologiesIds != null ?
						_technologyService.GetByIds(viewModel.TechnologiesIds) : new List<Technology>();

					_vacancyService.Insert(vacancy);

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(vacancy, e => e.Title, l.Title, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(vacancy, e => e.ShortDescription, l.ShortDescription, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(vacancy, e => e.Bonuses, l.Bonuses, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(vacancy, e => e.Requirments, l.Requirments, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(vacancy, e => e.Responsibilities, l.Responsibilities, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(vacancy, e => e.WeOffer, l.WeOffer, l.LanguageId);
					});

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
				viewModel.TechnologiesSelectList = Mapper.Map<List<Technology>, List<SelectListItem>>(_technologyService.GetAll());
			}


			return View(viewModel);
		}

		// GET: Admin/Vacancy/Edit
		public ActionResult Edit(int id)
		{
			var entity = _vacancyService.GetById(id);
			var viewModel = Mapper.Map<Vacancy, VacancyViewModel>(entity);

			viewModel.TechnologiesSelectList = Mapper.Map<List<Technology>, List<SelectListItem>>(_technologyService.GetAll());
			viewModel.TechnologiesSelectList.ForEach(item =>
			{
				item.Selected = viewModel.TechnologiesIds.Contains(int.Parse(item.Value));
			});

			AddLocales(viewModel.Locales, (locale, languageId) =>
			{
				locale.Title = entity.GetLocalized(x => x.Title, languageId);
				locale.ShortDescription = entity.GetLocalized(x => x.ShortDescription, languageId);
				locale.Bonuses = entity.GetLocalized(x => x.Bonuses, languageId);
				locale.Requirments = entity.GetLocalized(x => x.Requirments, languageId);
				locale.Responsibilities = entity.GetLocalized(x => x.Responsibilities, languageId);
				locale.WeOffer = entity.GetLocalized(x => x.WeOffer, languageId);
			});

			return View(viewModel);
		}

		// POST: Admin/Vacancy/Edit
		[HttpPost]
		public ActionResult Edit(VacancyViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var vacancy = Mapper.Map<VacancyViewModel, Vacancy>(viewModel);

					vacancy.Technologies = viewModel.TechnologiesIds != null ?
						_technologyService.GetByIds(viewModel.TechnologiesIds) :
						new List<Technology>();

					_vacancyService.Update(vacancy);

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(vacancy, e => e.Title, l.Title, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(vacancy, e => e.ShortDescription, l.ShortDescription, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(vacancy, e => e.Bonuses, l.Bonuses, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(vacancy, e => e.Requirments, l.Requirments, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(vacancy, e => e.Responsibilities, l.Responsibilities, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(vacancy, e => e.WeOffer, l.WeOffer, l.LanguageId);
					});

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);

				viewModel = Mapper.Map<Vacancy, VacancyViewModel>(_vacancyService.GetById(viewModel.ID));

				viewModel.TechnologiesSelectList = Mapper.Map<List<Technology>, List<SelectListItem>>(_technologyService.GetAll());
				viewModel.TechnologiesSelectList.ForEach(item =>
				{
					item.Selected = viewModel.TechnologiesIds.Contains(int.Parse(item.Value));
				});
			}

			return View(viewModel);
		}

		// GET: Admin/Vacancy/Details
		public ActionResult Details(int id)
		{
			var viewModel = Mapper.Map<Vacancy, VacancyViewModel>(_vacancyService.GetById(id));

			return View(viewModel);
		}
		// GET: Admin/Vacancy/Delete
		public ActionResult Delete(int id)
		{
			var viewModel = Mapper.Map<Vacancy, VacancyViewModel>(_vacancyService.GetById(id));

			return View(viewModel);
		}

		// POST: Admin/Vacancy/Delete
		[HttpPost]
		public ActionResult Delete(VacancyViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<VacancyViewModel, Vacancy>(viewModel);

					_vacancyService.Delete(entity);

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