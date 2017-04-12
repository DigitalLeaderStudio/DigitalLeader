namespace DigitalLeader.ViewModels
{
	using DigitalLeader.ViewModels.Localization;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Mvc;

	public class SliderViewModel : FileViewModel, ILocalizedModel<SliderViewModel.SliderLocalizedModel>
	{
		#region Nested class

		public partial class SliderLocalizedModel : ILocalizedModelLocal
		{
			public int LanguageId { get; set; }

			public string LanguageName { get; set; }

			[DataType(DataType.MultilineText)]
			[AllowHtml]
			public string Title { get; set; }

			[DataType(DataType.MultilineText)]
			[AllowHtml]
			public string Description { get; set; }
		}

		#endregion

		public IList<SliderLocalizedModel> Locales { get; set; }

		public SliderViewModel()
		{
			Locales = new List<SliderLocalizedModel>();
		}

		public int ID { get; set; }

		[DataType(DataType.MultilineText)]
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
