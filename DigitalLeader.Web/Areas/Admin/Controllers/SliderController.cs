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

	public class SliderController : BaseAdminController
	{
		private readonly ILocalizedEntityService _localizedEntityService;
		private readonly ISliderService _sliderService;

		public SliderController(
			ILocalizedEntityService localizedEntityService,
			ISliderService sliderService)
		{
			_localizedEntityService = localizedEntityService;
			_sliderService = sliderService;
		}

		//GET: Admin/Sliders
		public ActionResult Index()
		{
			var viewModel = Mapper.Map<List<Slider>, List<SliderViewModel>>(_sliderService.GetAll());

			return View(viewModel);
		}

		// GET: Admin/CreateSlider
		public ActionResult Create()
		{
			var viewModel = new SliderViewModel();

			AddLocales(viewModel.Locales, (locale, languageId) => { });

			return View();
		}

		// POST: Admin/CreateSlider
		[HttpPost]
		public ActionResult Create(SliderViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<SliderViewModel, Slider>(viewModel);

					_sliderService.Insert(entity);

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Title, l.Title, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Description, l.Description, l.LanguageId);
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

		// GET: Admin/EditSlider
		public ActionResult Edit(int id)
		{
			var entity = _sliderService.GetById(id);

			var viewModel = Mapper.Map<Slider, SliderViewModel>(entity);

			AddLocales(viewModel.Locales, (locale, languageId) =>
			{
				locale.Title = entity.GetLocalized(x => x.Title, languageId);
				locale.Description = entity.GetLocalized(x => x.Description, languageId);
			});

			return View(viewModel);
		}

		// POST: Admin/EditSlider
		[HttpPost]
		public ActionResult Edit(SliderViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<SliderViewModel, Slider>(viewModel);

					_sliderService.Update(entity);

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Title, l.Title, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Description, l.Description, l.LanguageId);
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

		//GET: Admin/DeleteSlider
		public ActionResult Delete(int id)
		{
			var viewModel = Mapper.Map<Slider, SliderViewModel>(_sliderService.GetById(id));

			return View(viewModel);
		}

		//POST: Admin/DeleteSlider
		[HttpPost]
		public ActionResult Delete(SliderViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<SliderViewModel, Slider>(viewModel);

					_sliderService.Delete(entity);

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
