namespace DigitalLeader.ViewModels
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Mvc;

	public class SliderViewModel : FileViewModel
	{
		public int ID { get; set; }

		[AllowHtml]
		public string Title { get; set; }

		[DataType(DataType.MultilineText)]
		[AllowHtml]
		public string Description { get; set; }

		public string BackgroundStyle { get; set; }

		public bool HasImage { get; set; }

		public string TargetLink { get; set; }
	}
}
