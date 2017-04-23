namespace DigitalLeader.Web.Controllers
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.ViewModels;
	using DigitalLeader.Web.Controllers;
	using DigitalLeader.Web.Controllers.Controllers;
	using System.Collections.Generic;
	using System.Web.Mvc;

	public class BlogpostController : BaseController
	{
		private IBlogpostService _blogpostService;

		public BlogpostController(IBlogpostService blogpostService)
		{
			_blogpostService = blogpostService;
		}

		[Route("Blog")]
		public ActionResult Index()
		{
			var viewModel = Mapper.Map<List<Blogpost>, List<BlogpostViewModel>>(_blogpostService.GetAll());

			return View(viewModel);
		}

		//// GET: Blogpost/Details/5
		//public ActionResult Details(int? id)
		//{
		//	if (id == null)
		//	{
		//		return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//	}
		//	Blogpost blogpost = db.Blogposts.Find(id);
		//	if (blogpost == null)
		//	{
		//		return HttpNotFound();
		//	}
		//	return View(blogpost);
		//}

		//// GET: Blogpost/Create
		//public ActionResult Create()
		//{
		//	return View();
		//}

		// POST: Blogpost/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Create([Bind(Include = "ID,Title,Content,Visible")] Blogpost blogpost)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		blogpost.PublishedDate = DateTime.Now;

		//		db.Blogposts.Add(blogpost);
		//		db.SaveChanges();
		//		return RedirectToAction("Index");
		//	}

		//	return View(blogpost);
		//}

		//// GET: Blogpost/Edit/5
		//public ActionResult Edit(int? id)
		//{
		//	if (id == null)
		//	{
		//		return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//	}
		//	Blogpost blogpost = db.Blogposts.Find(id);
		//	if (blogpost == null)
		//	{
		//		return HttpNotFound();
		//	}
		//	return View(blogpost);
		//}

		//// POST: Blogpost/Edit/5
		//// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		//// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Edit([Bind(Include = "ID,Title,Content,PublishedOn,Visible,Views")] Blogpost blogpost)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		db.Entry(blogpost).State = EntityState.Modified;
		//		db.SaveChanges();
		//		return RedirectToAction("Index");
		//	}
		//	return View(blogpost);
		//}

		// GET: Blogpost/Delete/5
		//public ActionResult Delete(int? id)
		//{
		//	if (id == null)
		//	{
		//		return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//	}
		//	Blogpost blogpost = db.Blogposts.Find(id);
		//	if (blogpost == null)
		//	{
		//		return HttpNotFound();
		//	}
		//	return View(blogpost);
		//}

		//// POST: Blogpost/Delete/5
		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		//public ActionResult DeleteConfirmed(int id)
		//{
		//	Blogpost blogpost = db.Blogposts.Find(id);
		//	db.Blogposts.Remove(blogpost);
		//	db.SaveChanges();
		//	return RedirectToAction("Index");
		//}

		//protected override void Dispose(bool disposing)
		//{
		//	if (disposing)
		//	{
		//		db.Dispose();
		//	}
		//	base.Dispose(disposing);
		//}

		//[ChildActionOnly]
		//public ActionResult Categories()
		//{
		//	List<Category> categories = db.Categories.ToList();
		//	return PartialView(categories);
		//}
	}
}
