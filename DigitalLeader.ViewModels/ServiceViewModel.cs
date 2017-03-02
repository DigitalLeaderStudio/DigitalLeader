﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace DigitalLeader.ViewModels
{
	public class ServiceViewModel : FileViewModel
	{
		public int ID { get; set; }

		public string Title { get; set; }

		public string SubTitle { get; set; }

		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		[AllowHtml]
		[UIHint("tinymce_full_compressed")]
		public string Content { get; set; }

		[Display(Name = "Category")]
		public int CategoryID { get; set; }

		[Display(Name = "Category")]
		public List<SelectListItem> CategoriesSelectList { get; set; }
	}
}
