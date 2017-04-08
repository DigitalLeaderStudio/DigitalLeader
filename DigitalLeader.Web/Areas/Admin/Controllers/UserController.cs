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
	using System.Linq;
	using DigitalLeader.Services.Localization;

	public class UserController : BaseAdminController
	{
		private readonly ILocalizedEntityService _localizedEntityService;
		private readonly ITechnologyService _technologyService;
		private readonly IUserService _userService;

		public UserController(
			ILocalizedEntityService localizedEntityService,
			ITechnologyService technologyService,
			IUserService userService)
		{
			_localizedEntityService = localizedEntityService;
			_technologyService = technologyService;
			_userService = userService;
		}

		// GET: Admin/User/Client
		public ActionResult Index()
		{
			var viewModel = Mapper.Map<IEnumerable<User>, List<UserViewModel>>(_userService.GetAll());

			return View(viewModel);
		}

		// GET: Admin/User/Create
		public ActionResult Create()
		{
			var viewModel = new UserViewModel();

			AddLocales(viewModel.Locales, (locale, languageId) => { });

			return View(viewModel);
		}

		// POST: Admin/User/Create
		[HttpPost]
		public ActionResult Create(UserViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var user = Mapper.Map<UserViewModel, User>(viewModel);

					_userService.Insert(user);

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(user, e => e.Title, l.Title, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(user, e => e.UserName, l.UserName, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(user, e => e.Biography, l.Biography, l.LanguageId);
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

		// GET: Admin/User/Edit
		public ActionResult Edit(int id)
		{
			var entity = _userService.GetById(id);
			var viewModel = Mapper.Map<User, UserViewModel>(entity);

			viewModel.TechnologiesSelectList = Mapper.Map<List<Technology>, List<SelectListItem>>(_technologyService.GetAll());
			viewModel.TechnologiesSelectList.ForEach(item =>
			{
				item.Selected = viewModel.TechnologiesIds.Contains(int.Parse(item.Value));
			});

			AddLocales(viewModel.Locales, (locale, languageId) =>
			{
				locale.Title = entity.GetLocalized(x => x.Title, languageId);
				locale.Biography = entity.GetLocalized(x => x.Biography, languageId);
				locale.UserName = entity.GetLocalized(x => x.UserName, languageId);
			});

			return View(viewModel);
		}

		// POST: Admin/User/Edit
		[HttpPost]
		public ActionResult Edit(UserViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var user = Mapper.Map<UserViewModel, User>(viewModel);

					user.Technologies = viewModel.TechnologiesIds != null ?
						_technologyService.GetByIds(viewModel.TechnologiesIds) :
						new List<Technology>();

					viewModel.Locales.ToList().ForEach(l =>
					{
						_localizedEntityService.SaveLocalizedValue(user, e => e.Title, l.Title, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(user, e => e.UserName, l.UserName, l.LanguageId);
						_localizedEntityService.SaveLocalizedValue(user, e => e.Biography, l.Biography, l.LanguageId);
					});

					_userService.Update(user);
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);

				return View(viewModel);
			}

			return RedirectToAction("Index");
		}

		// GET: Admin/User/Details
		public ActionResult Details(int id)
		{
			var viewModel = Mapper.Map<User, UserViewModel>(_userService.GetById(id));

			return View(viewModel);
		}
	}
}