using System.Web.Mvc;

namespace DigitalLeader.Web.Controllers
{
	public class TrackingController : Controller
    {
        public PartialViewResult GoogleAnalytics()
        {
            return PartialView();
        }
    }
}