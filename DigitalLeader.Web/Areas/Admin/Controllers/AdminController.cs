
namespace DigitalLeader.Web.Areas.Admin.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.ViewModels;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.Web.Mvc;

	public class AdminController : BaseAdminController
	{
		private readonly ISliderService _sliderService;

		public AdminController(ISliderService sliderService)
		{
			_sliderService = sliderService;
		}

		// GET: Admin/Admin
		public ActionResult Index()
		{
			return View();
		}

		#region Slider

		//GET: Admin/Sliders
		public ActionResult Sliders()
		{
			var viewModel = Mapper.Map<List<Slider>, List<SliderViewModel>>(_sliderService.GetAll());

			return View(viewModel);
		}

		// GET: Admin/CreateSlider
		public ActionResult CreateSlider()
		{
			return View();
		}

		// POST: Admin/CreateSlider
		[HttpPost]
		public ActionResult CreateSlider(SliderViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<SliderViewModel, Slider>(viewModel);

					_sliderService.Insert(entity);

					return RedirectToAction("Sliders");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
			}

			return View(viewModel);
		}

		// GET: Admin/EditSlider
		public ActionResult EditSlider(int id)
		{
			var viewModel = Mapper.Map<Slider, SliderViewModel>(_sliderService.GetById(id));

			return View(viewModel);
		}

		// POST: Admin/EditSlider
		[HttpPost]
		public ActionResult EditSlider(SliderViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var client = Mapper.Map<SliderViewModel, Slider>(viewModel);

					_sliderService.Update(client);
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);

				return View(viewModel);
			}

			return RedirectToAction("Sliders");
		}

		//GET: Admin/DeleteSlider
		public ActionResult DeleteSlider(int id)
		{
			var viewModel = Mapper.Map<Slider, SliderViewModel>(_sliderService.GetById(id));

			return View(viewModel);
		}

		//POST: Admin/DeleteSlider
		[HttpPost]
		public ActionResult DeleteSlider(SliderViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<SliderViewModel, Slider>(viewModel);

					_sliderService.Delete(entity);

					return RedirectToAction("Sliders");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
			}

			return View(viewModel);
		}

		#endregion
	}
}