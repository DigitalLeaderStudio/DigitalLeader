namespace DigitalLeader.Web.Areas.Admin.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities.Identity;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.Services.Localization;
	using DigitalLeader.ViewModels;
	using System;
	using System.Collections.Generic;
	using System.Web.Mvc;
	using System.Linq;

	public class ClientController : BaseAdminController
	{
		private readonly ILocalizedEntityService _localizedEntityService;
		private readonly IClientService _clientService;

		public ClientController(
			ILocalizedEntityService localizedEntityService,
			IClientService clientService)
		{
			_localizedEntityService = localizedEntityService;
			_clientService = clientService;
		}

		// GET: Admin/Client
		public ActionResult Index()
		{
			var viewModel = Mapper.Map<IEnumerable<Client>, List<ClientViewModel>>(_clientService.GetAll());

			return View(viewModel);
		}

		// GET: Admin/Client/Create
		public ActionResult Create()
		{
			var viewModel = new ClientViewModel
			{
				JoinDate = DateTime.UtcNow
			};

			AddLocales(viewModel.Locales, (locale, languageId) => { });

			return View(viewModel);
		}

		// POST: Admin/Client/Create
		[HttpPost]
		public ActionResult Create(ClientViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var client = Mapper.Map<ClientViewModel, Client>(viewModel);

					_clientService.Insert(client);

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(client, e => e.Title, l.Title, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(client, e => e.Company, l.Company, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(client, e => e.FirstName, l.FirstName, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(client, e => e.LastName, l.LastName, l.LanguageId);
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

		// GET: Admin/Client/Edit
		public ActionResult Edit(int id)
		{
			var entity = _clientService.GetById(id);

			var viewModel = Mapper.Map<Client, ClientViewModel>(entity);

			AddLocales(viewModel.Locales, (locale, languageId) =>
			{
				locale.FirstName = entity.GetLocalized(x => x.FirstName, languageId);
				locale.LastName = entity.GetLocalized(x => x.LastName, languageId);
				locale.Title = entity.GetLocalized(x => x.Title, languageId);
				locale.Company = entity.GetLocalized(x => x.Company, languageId);
			});

			return View(viewModel);
		}

		// POST: Admin/Client/Edit
		[HttpPost]
		public ActionResult Edit(ClientViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var client = Mapper.Map<ClientViewModel, Client>(viewModel);

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(client, e => e.Title, l.Title, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(client, e => e.Company, l.Company, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(client, e => e.FirstName, l.FirstName, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(client, e => e.LastName, l.LastName, l.LanguageId);
					});

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

		// GET: Admin/Client/Details
		public ActionResult Details(int id)
		{
			var viewModel = Mapper.Map<Client, ClientViewModel>(_clientService.GetById(id));

			return View(viewModel);
		}
		// GET: Admin/Client/Delete
		public ActionResult Delete(int id)
		{
			var viewModel = Mapper.Map<Client, ClientViewModel>(_clientService.GetById(id));

			return View(viewModel);
		}

		// POST: Admin/Client/Delete
		[HttpPost]
		public ActionResult Delete(ClientViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<ClientViewModel, Client>(viewModel);

					_clientService.Delete(entity);

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