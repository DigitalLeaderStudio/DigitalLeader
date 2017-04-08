namespace DigitalLeader.ViewModels
{
	using DigitalLeader.ViewModels.Localization;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Mvc;

	public class TestimonialViewModel : ILocalizedModel<TestimonialViewModel.TestimonialLocalizedModel>
	{
		#region Nested class

		public partial class TestimonialLocalizedModel : ILocalizedModelLocal
		{
			public int LanguageId { get; set; }

			public string LanguageName { get; set; }

			[DataType(DataType.MultilineText)]
			public string Text { get; set; }
		}

		#endregion

		public IList<TestimonialLocalizedModel> Locales { get; set; }

		public TestimonialViewModel()
		{
			Locales = new List<TestimonialLocalizedModel>();
		}

		public int ID { get; set; }

		public DateTime CreatedDate { get; set; }

		[DataType(DataType.MultilineText)]
		public string Text { get; set; }

		[Display(Name = "Client")]

		public int? ClientID { get; set; }

		public ClientViewModel Client { get; set; }

		[Display(Name = "Client")]
		public List<SelectListItem> ClientsSelectList { get; set; }
	}
}
