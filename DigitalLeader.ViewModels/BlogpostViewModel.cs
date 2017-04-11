namespace DigitalLeader.ViewModels
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Mvc;

	public class BlogpostViewModel
	{
		public int ID { get; set; }

		public DateTime PublishedDate { get; set; }

		public string Title { get; set; }

		public string Overview { get; set; }


		public string Keywords { get; set; }

		public int ViewsCount { get; set; }

		public bool IsPublished { get; set; }

		[AllowHtml]
		[UIHint("tinymce_basic_compressed")]
		public string Content { get; set; }
		//public int AuthorId { get; set; }

		//public virtual User Author { get; set; }

		public int ServiceId { get; set; }

		[Display(Name = "Service")]
		public List<SelectListItem> ServicesSelectList { get; set; }
	}
}
