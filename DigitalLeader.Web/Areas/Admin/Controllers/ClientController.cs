namespace DigitalLeader.Web.Areas.Admin.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities.Identity;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.ViewModels;
	using System;
	using System.Collections.Generic;
	using System.Web.Mvc;

	public class ClientController : BaseAdminController
	{
		private IClientService _clientService;

		public ClientController(IClientService clientService)
		{
			_clientService = clientService;
		}

		// GET: Admin/Client
		public ActionResult Index()
		{
			var viewModel = Mapper.Map<IEnumerable<Client>, List<ClientViewModel>>(_clientService.GetAll());

			return View(viewModel);
		}

		// GET: Admin/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Admin/Create
		[HttpPost]
		public ActionResult Create(ClientViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var client = Mapper.Map<ClientViewModel, Client>(viewModel);

					_clientService.Insert(client);

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
			}

			return View(viewModel);
		}


		// GET: Admin/Edit
		public ActionResult Edit(int id)
		{
			var viewModel = Mapper.Map<Client, ClientViewModel>(_clientService.GetById(id));

			return View(viewModel);
		}

		// POST: Admin/Edit
		[HttpPost]
		public ActionResult Edit(ClientViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var client = Mapper.Map<ClientViewModel, Client>(viewModel);

					_clientService.Update(client);
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);

				return View(viewModel);
			}

			return RedirectToAction("Index");
		}
	}
}