namespace DigitalLeader.Web.Areas.Admin.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.ViewModels;
	using System;
	using System.Collections.Generic;
	using System.Web.Mvc;

	public class CategoryController : BaseAdminController
	{
		private ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		// GET: Admin/Category
		public ActionResult Index()
		{
			//var viewModel = Mapper.Map<IEnumerable<Client>, List<ClientViewModel>>(_clientService.GetAll());
			var viewModel = Mapper.Map<List<Category>, List<CategoryViewModel>>(_categoryService.GetAll());

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
			var viewModel = Mapper.Map<Category, CategoryViewModel>(_categoryService.GetById(id));

			return View(viewModel);
		}

		// POST: Admin/Category/Edit
		[HttpPost]
		public ActionResult Edit(CategoryViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var client = Mapper.Map<CategoryViewModel, Category>(viewModel);

					_categoryService.Update(client);
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