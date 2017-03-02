namespace DigitalLeader.Web.Controllers.Controllers
{
	using DigitalLeader.Services.Interfaces;
	using System.Web.Mvc;
	using System.Web.UI;

	public class FileController : BaseController
	{
		private IFileService _fileService;

		public FileController(IFileService fileService)
		{
			_fileService = fileService;
		}

		[OutputCache(Duration = 3600, Location = OutputCacheLocation.Client, VaryByParam = "id")]
		public ActionResult Show(int id)
		{
			var file = _fileService.GetById(id);

			return File(file.Content, file.ContentType);
		}
	}
}