using DigitalLeader.ViewModels.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;

namespace DigitalLeader.ViewModels
{
	public class ServiceViewModel : FileViewModel, ILocalizedModel<ServiceViewModel.ServiceLocalizedModel>
	{
		#region Nested class

		public partial class ServiceLocalizedModel : ILocalizedModelLocal
		{
			public int LanguageId { get; set; }

			public string LanguageName { get; set; }

			public string Title { get; set; }

			public string SubTitle { get; set; }

			[DataType(DataType.MultilineText)]
			public string Description { get; set; }

			[AllowHtml]
			[UIHint("tinymce_full_compressed")]
			public string Content { get; set; }
		}

		#endregion

		public ServiceViewModel()
		{
			Locales = new List<ServiceLocalizedModel>();
		}

		public IList<ServiceLocalizedModel> Locales { get; set; }

		public int ID { get; set; }

		public string Title { get; set; }

		public string SubTitle { get; set; }

		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		[AllowHtml]
		[UIHint("tinymce_full_compressed")]
		public string Content { get; set; }

		[Display(Name = "Subcategory")]
		public int ServiceSubcategoryID { get; set; }

		[Display(Name = "SeervicesSubcategoties")]
		public List<SelectListItem> ServiceSubcategoriesSelectList { get; set; }
	}
}
