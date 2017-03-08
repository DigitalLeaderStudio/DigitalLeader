namespace DigitalLeader.Web.Areas.Admin.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.ViewModels;
	using System;
	using System.Collections.Generic;
	using System.Web.Mvc;

	public class ServiceSubcategoryController : BaseAdminController
	{
		private IServiceSubcategoryService _subcategoryService;

		public ServiceSubcategoryController(IServiceSubcategoryService subcategoryService)
		{
			_subcategoryService = subcategoryService;
		}

		// GET: Admin/Category
		public ActionResult Index()
		{
			//var viewModel = Mapper.Map<IEnumerable<Client>, List<ClientViewModel>>(_clientService.GetAll());
			var viewModel = Mapper.Map<List<ServiceSubcategory>, List<ServiceSubcategoryViewModel>>(_subcategoryService.GetAll());

			return View(viewModel);
		}

		//// GET: Admin/Category/Create
		//public ActionResult Create()
		//{
		//	return View();
		//}

		// POST: Admin/Category/Create
		//[HttpPost]
		//public ActionResult Create(ClientViewModel viewModel)
		//{
		//	try
		//	{
		//		if (ModelState.IsValid)
		//		{
		//			var client = Mapper.Map<ClientViewModel, Client>(viewModel);

		//			_clientService.Insert(client);

		//			return RedirectToAction("Index");
		//		}
		//	}
		//	catch (Exception e)
		//	{
		//		ModelState.AddModelError("", e.Message);
		//	}

		//	return View(viewModel);
		//}

		// GET: Admin/Category/Edit
		public ActionResult Edit(int id)
		{
			//var viewModel = Mapper.Map<Client, ClientViewModel>(_clientService.GetById(id));
			var viewModel = Mapper.Map<ServiceSubcategory, ServiceSubcategoryViewModel>(_subcategoryService.GetById(id));

			return View(viewModel);
		}

		// POST: Admin/Category/Edit
		[HttpPost]
		public ActionResult Edit(ServiceSubcategoryViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var client = Mapper.Map<ServiceSubcategoryViewModel, ServiceSubcategory>(viewModel);

					_subcategoryService.Update(client);
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);

				return View(viewModel);
			}

			return RedirectToAction("Index");
		}

		//// GET: Admin/Clinet/Details
		//public ActionResult Details(int id)
		//{
		//	var viewModel = Mapper.Map<Client, ClientViewModel>(_clientService.GetById(id));

		//	return View(viewModel);
		//}
	}
}