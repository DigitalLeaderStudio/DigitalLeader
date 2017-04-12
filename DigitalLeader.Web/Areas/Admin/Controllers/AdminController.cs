
namespace DigitalLeader.Web.Areas.Admin.Controllers
{
	using System.Web.Mvc;

	public class AdminController : BaseAdminController
	{
		public AdminController()
		{
		}

		// GET: Admin/Admin
		public ActionResult Index()
		{
			return View();
		}
	}
}