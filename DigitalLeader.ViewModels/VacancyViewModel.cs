using DigitalLeader.ViewModels.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DigitalLeader.ViewModels
{
	public class VacancyViewModel : ILocalizedModel<VacancyViewModel.VacancyLocalizedModel>
	{
		#region Nested class

		public partial class VacancyLocalizedModel : ILocalizedModelLocal
		{
			public int LanguageId { get; set; }

			public string LanguageName { get; set; }

			public string Title { get; set; }

			[DataType(DataType.MultilineText)]
			public string ShortDescription { get; set; }

			[DataType(DataType.MultilineText)]
			public string Bonuses { get; set; }

			[DataType(DataType.MultilineText)]
			public string Requirments { get; set; }

			[DataType(DataType.MultilineText)]
			public string Responsibilities { get; set; }

			[DataType(DataType.MultilineText)]
			public string WeOffer { get; set; }
		}

		#endregion

		public IList<VacancyLocalizedModel> Locales { get; set; }

		public VacancyViewModel()
		{
			Locales = new List<VacancyLocalizedModel>();
		}

		public int ID { get; set; }

		public string Title { get; set; }

		[Display(Name = "Short Description")]
		[DataType(DataType.MultilineText)]
		public string ShortDescription { get; set; }

		[DataType(DataType.MultilineText)]
		public string Bonuses { get; set; }

		[DataType(DataType.MultilineText)]
		public string Requirments { get; set; }

		[DataType(DataType.MultilineText)]
		public string Responsibilities { get; set; }

		[DataType(DataType.MultilineText)]
		public string WeOffer { get; set; }

		public DateTime CreatedDate { get; set; }

		public bool IsPositionOpen { get; set; }

		public int[] TechnologiesIds { get; set; }

		[Display(Name = "Technikal Skills")]
		public List<TechnologyViewModel> Technologies { get; set; }

		[Display(Name = "Technikal Skills")]
		public List<SelectListItem> TechnologiesSelectList { get; set; }
	}
}
