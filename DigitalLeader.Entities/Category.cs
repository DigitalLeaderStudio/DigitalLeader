namespace DigitalLeader.Entities
{
	using System.Collections.Generic;

	public class Category
	{
		public int ID { get; set; }

		public string Name { get; set; }
		
		public virtual ICollection<Blogpost> Blogposts { get; set; }
	}
}
