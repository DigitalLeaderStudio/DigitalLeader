namespace DigitalLeader.ViewModels
{
	using DigitalLeader.ViewModels.Localization;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Mvc;

	public class ServiceCategoryViewModel : FileViewModel, ILocalizedModel<ServiceCategoryViewModel.ServiceCategoryLocalizedModel>
	{
		#region Nested class

		public partial class ServiceCategoryLocalizedModel : ILocalizedModelLocal
		{
			public int LanguageId { get; set; }

			public string LanguageName { get; set; }

			public string Name { get; set; }

			[AllowHtml]
			[UIHint("tinymce_basic_compressed")]
			public string Content { get; set; }			
		}

		public IList<ServiceCategoryLocalizedModel> Locales { get; set; }

		public ServiceCategoryViewModel()
		{
			Locales = new List<ServiceCategoryLocalizedModel>();
		}

		#endregion
		public int ID { get; set; }

		public string Name { get; set; }

		[AllowHtml]
		[UIHint("tinymce_basic_compressed")]
		public string Content { get; set; }

        public string CssClass { get; set; }

        public virtual List<ServiceSubcategoryViewModel> ServiceSubcategories { get; set; }
	}
}
