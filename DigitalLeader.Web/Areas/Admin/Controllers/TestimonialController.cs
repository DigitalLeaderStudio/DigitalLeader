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

	public class TestimonialController : BaseAdminController
	{
		private IClientService _clientService;
		private ITestimonialService _testimonialService;

		public TestimonialController(
			IClientService clientService,
			ITestimonialService testimonialService)
		{
			_clientService = clientService;
			_testimonialService = testimonialService;
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
			var viewModel = Mapper.Map<Testimonial, TestimonialViewModel>(_testimonialService.GetById(id));
			viewModel.ClientsSelectList = Mapper.Map<List<Client>, List<SelectListItem>>(_clientService.GetAll());

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