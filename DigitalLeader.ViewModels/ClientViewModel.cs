namespace DigitalLeader.ViewModels
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class ClientViewModel : FileViewModel
	{
		public int ID { get; set; }

		public string Company { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Title { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime JoinDate { get; set; }
	}
}
