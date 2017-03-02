namespace DigitalLeader.ViewModels
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Mvc;

	public class TestimonialViewModel
	{
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
