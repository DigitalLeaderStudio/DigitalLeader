namespace DigitalLeader.ViewModels
{
	using DigitalLeader.ViewModels.Localization;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Mvc;

	public class BlogpostViewModel : ILocalizedModel<BlogpostViewModel.BlogpostLocalizedModel>
	{
		#region Nested class

		public partial class BlogpostLocalizedModel : ILocalizedModelLocal
		{
			public int LanguageId { get; set; }

			public string LanguageName { get; set; }

			public string Title { get; set; }

			[DataType(DataType.MultilineText)]
			public string Overview { get; set; }

			[DataType(DataType.MultilineText)]
			public string Keywords { get; set; }

			[AllowHtml]
			[UIHint("tinymce_basic_compressed")]
			public string Content { get; set; }
		}

		#endregion

		public IList<BlogpostLocalizedModel> Locales { get; set; }

		public BlogpostViewModel()
		{
			Locales = new List<BlogpostLocalizedModel>();
		}

		public int ID { get; set; }

		public DateTime PublishedDate { get; set; }

		public string Title { get; set; }

		[DataType(DataType.MultilineText)]
		public string Overview { get; set; }

		[DataType(DataType.MultilineText)]
		public string Keywords { get; set; }

		[AllowHtml]
		[UIHint("tinymce_basic_compressed")]
		public string Content { get; set; }

		public int ViewsCount { get; set; }

		public bool IsPublished { get; set; }

		//public int AuthorId { get; set; }

		//public virtual User Author { get; set; }

		public int ServiceId { get; set; }

		[Display(Name = "Service")]
		public List<SelectListItem> ServicesSelectList { get; set; }
	}
}
