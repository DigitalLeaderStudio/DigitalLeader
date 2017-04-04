namespace DigitalLeader.ViewModels
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class SliderViewModel : FileViewModel
	{
		public int ID { get; set; }

		public string Title { get; set; }

		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		public string TargetLink { get; set; }
	}
}
