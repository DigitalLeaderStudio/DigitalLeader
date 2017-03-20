namespace DigitalLeader.Entities
{
    using DigitalLeader.Entities.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Vacancy : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }
                
        public string Bonuses { get; set; }

        public string Requirments { get; set; }

        public string Responsibilities { get; set; }

        public string WeOffer { get; set; }

        public bool IsPositionOpen { get; set; }

		public DateTime CreatedDate { get; set; }

        public virtual ICollection<Technology> Technologies { get; set; }

    }
}
