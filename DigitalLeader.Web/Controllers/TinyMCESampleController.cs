using System.Web.Mvc;
using DigitalLeader.Web.Models;

namespace DigitalLeader.Web.Controllers
{
    public class TinyMCESampleController : Controller
	{
        public ActionResult Index()
		{
            return View(new TinyMCEModel { 
				Basic = "This editor instance is using the 'tinymce_jquery_basic_compressed' template.", 
				Classic = "This editor instance is using the 'tinymce_jquery_classic_compressed' template.", 
				Full = "This editor instance is using the 'tinymce_jquery_full_compressed' template.", 
			});
        }
    }
}