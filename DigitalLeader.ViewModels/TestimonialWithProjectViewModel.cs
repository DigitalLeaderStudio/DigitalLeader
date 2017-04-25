namespace DigitalLeader.ViewModels
{
	using DigitalLeader.ViewModels.Localization;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class TestimonialWithProjectViewModel : ILocalizedModel<TestimonialWithProjectViewModel.TestimonialLocalizedModel>
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

		public TestimonialWithProjectViewModel()
		{
			Locales = new List<TestimonialLocalizedModel>();
		}

		public int ID { get; set; }

		public DateTime CreatedDate { get; set; }

		[DataType(DataType.MultilineText)]
		public string Text { get; set; }

		public int? ClientImageID { get; set; }

		public string ClientName { get; set; }

		public string ClientTitle { get; set; }

		public int? ProjectID { get; set; }
	}
}
