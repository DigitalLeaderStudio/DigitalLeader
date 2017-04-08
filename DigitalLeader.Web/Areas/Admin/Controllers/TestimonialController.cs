namespace DigitalLeader.Web.Areas.Admin.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Entities.Identity;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.ViewModels;
	using System;
	using System.Collections.Generic;
	using System.Web.Mvc;
	using DigitalLeader.Services.Localization;
	using DigitalLeader.Web.Extensions;
	using DigitalLeader.Web.Configuration;
	using System.Configuration;
	using System.Linq;

	public class TestimonialController : BaseAdminController
	{
		private readonly IClientService _clientService;
		private readonly ITestimonialService _testimonialService;
		private readonly ILocalizedEntityService _localizedEntityService;

		public TestimonialController(
			IClientService clientService,
			ITestimonialService testimonialService,
			ILocalizedEntityService localizedEntityService)
		{
			_clientService = clientService;
			_testimonialService = testimonialService;
			_localizedEntityService = localizedEntityService;
		}

		// GET: Admin/Testimonial
		public ActionResult Index()
		{
			var viewModel = Mapper.Map<List<Testimonial>, List<TestimonialViewModel>>(_testimonialService.GetAll());

			return View(viewModel);
		}

		// GET: Admin/Testimonial/Create
		public ActionResult Create()
		{
			var viewModel = new TestimonialViewModel
			{
				CreatedDate = DateTime.Now,
				ClientsSelectList = Mapper.Map<List<Client>, List<SelectListItem>>(_clientService.GetAll())
			};

			AddLocales(viewModel.Locales, (locale, languageId) => { });

			return View(viewModel);
		}

		// POST: Admin/Testimonial/Create
		[HttpPost]
		public ActionResult Create(TestimonialViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<TestimonialViewModel, Testimonial>(viewModel);

					_testimonialService.Insert(entity);

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Text, l.Text, l.LanguageId);
					});

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
			}

			viewModel.ClientsSelectList = Mapper.Map<List<Client>, List<SelectListItem>>(_clientService.GetAll());

			return View(viewModel);
		}

		// GET: Admin/Testimonial/Edit
		public ActionResult Edit(int id)
		{
			var entity = _testimonialService.GetById(id);
			var viewModel = Mapper.Map<Testimonial, TestimonialViewModel>(entity);
			viewModel.ClientsSelectList = Mapper.Map<List<Client>, List<SelectListItem>>(_clientService.GetAll());

			AddLocales(viewModel.Locales, (locale, languageId) =>
			{
				locale.Text = entity.GetLocalized(x => x.Text, languageId, false, false);
			});

			return View(viewModel);
		}

		// POST: Admin/Testimonial/Edit
		[HttpPost]
		public ActionResult Edit(TestimonialViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<TestimonialViewModel, Testimonial>(viewModel);

					_testimonialService.Update(entity);

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(entity, e => e.Text, l.Text, l.LanguageId);
					});

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
			}
			viewModel.ClientsSelectList = Mapper.Map<List<Client>, List<SelectListItem>>(_clientService.GetAll());

			return View(viewModel);
		}

		// GET: Admin/Testimonial/Delete
		public ActionResult Delete(int id)
		{
			var viewModel = Mapper.Map<Testimonial, TestimonialViewModel>(_testimonialService.GetById(id));

			return View(viewModel);
		}

		// POST: Admin/Testimonial/Delete
		[HttpPost]
		public ActionResult Delete(TestimonialViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<TestimonialViewModel, Testimonial>(viewModel);

					_testimonialService.Delete(entity);

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