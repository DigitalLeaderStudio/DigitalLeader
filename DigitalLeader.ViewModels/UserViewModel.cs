using DigitalLeader.ViewModels.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;

namespace DigitalLeader.ViewModels
{
	public class UserViewModel : FileViewModel, ILocalizedModel<UserViewModel.UserLocalizedModel>
	{
		#region Nested class

		public partial class UserLocalizedModel : ILocalizedModelLocal
		{
			public int LanguageId { get; set; }

			public string LanguageName { get; set; }

			public string UserName { get; set; }

			public string Biography { get; set; }

			public string Title { get; set; }
		}

		#endregion

		public IList<UserLocalizedModel> Locales { get; set; }

		public UserViewModel()
		{
			Locales = new List<UserLocalizedModel>();
			Technologies = new List<TechnologyViewModel>();
		}

		public int Id { get; set; }

		public int ExperianceYears { get; set; }

		public string Email { get; set; }

		public string UserName { get; set; }

		public string Biography { get; set; }

		public string Title { get; set; }

		public int[] TechnologiesIds { get; set; }

		public List<TechnologyViewModel> Technologies { get; set; }

		[Display(Name = "Technologies")]
		public List<SelectListItem> TechnologiesSelectList { get; set; }
	}
}
