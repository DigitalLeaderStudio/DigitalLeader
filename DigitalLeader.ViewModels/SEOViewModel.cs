namespace DigitalLeader.ViewModels
{
	using DigitalLeader.ViewModels.Localization;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Mvc;

	public class SEOViewModel : ILocalizedModel<SEOViewModel.SEOLocalizedModel>
	{
		#region Nested class

		public partial class SEOLocalizedModel : ILocalizedModelLocal
		{
			public int LanguageId { get; set; }

			public string LanguageName { get; set; }

			[DataType(DataType.MultilineText)]
			public string Keywords { get; set; }

			[DataType(DataType.MultilineText)]
			public string Description { get; set; }
		}

		#endregion

		public IList<SEOLocalizedModel> Locales { get; set; }

		public SEOViewModel()
		{
			Locales = new List<SEOLocalizedModel>();
		}

		public int ID { get; set; }

		public string Key { get; set; }

		[DataType(DataType.MultilineText)]
		public string Keywords { get; set; }

		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		[Display(Name = "Available keys")]
		public List<SelectListItem> SEOKeysSelectList { get; set; }
	}
}
