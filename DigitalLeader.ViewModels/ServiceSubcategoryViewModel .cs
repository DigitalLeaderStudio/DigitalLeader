namespace DigitalLeader.ViewModels
{
	using DigitalLeader.ViewModels.Localization;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Mvc;

	public class ServiceSubcategoryViewModel : FileViewModel, ILocalizedModel<ServiceSubcategoryViewModel.ServiceSubcategoryLocalizedModel>
	{
		#region Nested class

		public partial class ServiceSubcategoryLocalizedModel : ILocalizedModelLocal
		{
			public int LanguageId { get; set; }

			public string LanguageName { get; set; }

			public string Name { get; set; }
		}

		#endregion

		public IList<ServiceSubcategoryLocalizedModel> Locales { get; set; }

		public ServiceSubcategoryViewModel()
		{
			Locales = new List<ServiceSubcategoryLocalizedModel>();
		}

		public int ID { get; set; }

		public string Name { get; set; }

		public virtual List<ServiceViewModel> Services { get; set; }

		[Display(Name = "ServiceCategory")]
		public int ServiceCategoryID { get; set; }

		[Display(Name = "ServiceCategories")]
		public List<SelectListItem> ServiceCategoriesSelectList { get; set; }
	}
}
