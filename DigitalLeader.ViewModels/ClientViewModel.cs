namespace DigitalLeader.ViewModels
{
	using DigitalLeader.ViewModels.Localization;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class ClientViewModel : FileViewModel, ILocalizedModel<ClientViewModel.ClientLocalizedModel>
	{
		#region Nested class

		public partial class ClientLocalizedModel : ILocalizedModelLocal
		{
			public int LanguageId { get; set; }

			public string LanguageName { get; set; }

			public string Company { get; set; }

			public string FirstName { get; set; }

			public string LastName { get; set; }

			public string Title { get; set; }
		}

		#endregion

		public IList<ClientLocalizedModel> Locales { get; set; }

		public ClientViewModel()
		{
			Locales = new List<ClientLocalizedModel>();
		}

		public int ID { get; set; }

		public string Company { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Title { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime JoinDate { get; set; }
	}
}
