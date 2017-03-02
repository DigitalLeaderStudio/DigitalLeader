namespace DigitalLeader.Web.Areas.Admin.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities.Identity;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.ViewModels;
	using System;
	using System.Collections.Generic;
	using System.Web.Mvc;

	public class UserController : BaseAdminController
	{
		private IUserService _userService;

		public UserController(IUserService userService)
		{
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
			return View();
		}

		// POST: Admin/User/Create
		[HttpPost]
		public ActionResult Create(UserViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var client = Mapper.Map<UserViewModel, User>(viewModel);

					_userService.Insert(client);

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
			var viewModel = Mapper.Map<User, UserViewModel>(_userService.GetById(id));

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