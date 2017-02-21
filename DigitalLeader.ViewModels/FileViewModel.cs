namespace DigitalLeader.ViewModels
{
	using System.Web;

	public class FileViewModel
	{
		public virtual int? ImageId { get; set; }

		public virtual HttpPostedFileBase File { get; set; }
	}
}
