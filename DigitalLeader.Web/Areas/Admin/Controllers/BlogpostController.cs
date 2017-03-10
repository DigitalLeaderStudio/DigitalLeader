namespace DigitalLeader.Web.Areas.Admin.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.ViewModels;
	using System;
	using System.Collections.Generic;
	using System.Web.Mvc;

	public class BlogpostController : BaseAdminController
	{
		private IBlogpostService _blogpostService;
		private IServiceService _serviceService;

		public BlogpostController(
			IServiceService serviceService,
			IBlogpostService blogpostService)
		{
			_serviceService = serviceService;
			_blogpostService = blogpostService;
		}

		// GET: Admin/Blogpost
		public ActionResult Index()
		{
			var viewModel = Mapper.Map<List<Blogpost>, List<BlogpostViewModel>>(_blogpostService.GetAll());

			return View(viewModel);
		}

		// GET: Admin/Blogpost/Details
		public ActionResult Details(int id)
		{
			var viewModel = Mapper.Map<Blogpost, BlogpostViewModel>(_blogpostService.GetById(id));

			return View(viewModel);
		}

		// GET: Admin/Blogpost/Create
		public ActionResult Create()
		{
			var viewModel = new BlogpostViewModel
			{
				ServicesSelectList = Mapper.Map<List<Service>, List<SelectListItem>>(_serviceService.GetAll()),
				PublishedDate = DateTime.Now
			};

			return View(viewModel);
		}

		// POST: Admin/Project/Create
		[HttpPost]
		public ActionResult Create(BlogpostViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<BlogpostViewModel, Blogpost>(viewModel);

					entity.AuthorId = base.LoggetUserID;

					_blogpostService.Insert(entity);

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
			}

			viewModel.ServicesSelectList = Mapper.Map<List<Service>, List<SelectListItem>>(_serviceService.GetAll());

			return View(viewModel);
		}

		// GET: Admin/Project/Edit
		public ActionResult Edit(int id)
		{
			var viewModel = Mapper.Map<Blogpost, BlogpostViewModel>(_blogpostService.GetById(id));
			viewModel.ServicesSelectList = Mapper.Map<List<Service>, List<SelectListItem>>(_serviceService.GetAll());

			return View(viewModel);
		}

		// POST: Admin/Project/Edit
		[HttpPost]
		public ActionResult Edit(BlogpostViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var entity = Mapper.Map<BlogpostViewModel, Blogpost>(viewModel);
										
					_blogpostService.Update(entity);

					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
			}

			viewModel.ServicesSelectList = Mapper.Map<List<Service>, List<SelectListItem>>(_serviceService.GetAll());

			return View(viewModel);
		}

        // GET: Admin/Blogpost/Delete
        public ActionResult Delete(int id)
        {
            var viewModel = Mapper.Map<Blogpost, BlogpostViewModel>(_blogpostService.GetById(id));

            return View(viewModel);
        }

        // POST: Admin/Blogpost/Delete
        [HttpPost]
        public ActionResult Delete(BlogpostViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = Mapper.Map<BlogpostViewModel, Blogpost>(viewModel);

                    _blogpostService.Delete(entity);

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