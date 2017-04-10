namespace DigitalLeader.ViewModels
{
	using DigitalLeader.ViewModels.Localization;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Mvc;

	public class ContentViewModel : ILocalizedModel<ContentViewModel.ContentLocalizedModel>
	{
		#region Nested class

		public partial class ContentLocalizedModel : ILocalizedModelLocal
		{
			public int LanguageId { get; set; }

			public string LanguageName { get; set; }

			[DataType(DataType.MultilineText)]
			public string Description { get; set; }

			[DataType(DataType.MultilineText)]
			public string Keywords { get; set; }

			[AllowHtml]
			[UIHint("tinymce_basic_compressed")]
			public string Html { get; set; }
		}

		#endregion

		public IList<ContentLocalizedModel> Locales { get; set; }

		public ContentViewModel()
		{
			Locales = new List<ContentLocalizedModel>();
		}

		public int ID { get; set; }

		[Required]
		public string Key { get; set; }

		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		[DataType(DataType.MultilineText)]
		public string Keywords { get; set; }

		[AllowHtml]
		[UIHint("tinymce_basic_compressed")]
		public string Html { get; set; }

		public DateTime CreatedDate { get; set; }

		public bool IsActive { get; set; }
	}
}
