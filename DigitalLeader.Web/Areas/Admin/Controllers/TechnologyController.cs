namespace DigitalLeader.Web.Areas.Admin.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.ViewModels;
	using System;
	using System.Collections.Generic;
	using System.Web.Mvc;

	public class TechnologyController : BaseAdminController
	{
		private ITechnologyService _technologyService;

		public TechnologyController(ITechnologyService technologyService)
		{
			_technologyService = technologyService;
		}

		// GET: Admin/Technology
		public ActionResult Index()
		{
			var viewModel = Mapper.Map<List<Technology>, List<TechnologyViewModel>>(_technologyService.GetAll());

			return View(viewModel);
		}

		// GET: Admin/Technology/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Admin/Technology/Create
		[HttpPost]
		public ActionResult Create(TechnologyViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<TechnologyViewModel, Technology>(viewModel);

					_technologyService.Insert(entity);

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
			}

			return View(viewModel);
		}

		// GET: Admin/Technology/Edit
		public ActionResult Edit(int id)
		{
			var viewModel = Mapper.Map<Technology, TechnologyViewModel>(_technologyService.GetById(id));

			return View(viewModel);
		}

		// POST: Admin/Technology/Edit
		[HttpPost]
		public ActionResult Edit(TechnologyViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<TechnologyViewModel, Technology>(viewModel);

					_technologyService.Update(entity);

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
			}

			return View(viewModel);
		}

		// GET: Admin/Technology/Delete
		public ActionResult Delete(int id)
		{
			var viewModel = Mapper.Map<Technology, TechnologyViewModel>(_technologyService.GetById(id));

			return View(viewModel);
		}

		// POST: Admin/Technology/Delete
		[HttpPost]
		public ActionResult Delete(TechnologyViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<TechnologyViewModel, Technology>(viewModel);

					_technologyService.Delete(entity);

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