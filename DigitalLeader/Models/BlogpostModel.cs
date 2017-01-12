using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Configuration;
using System.Web.Mvc;

namespace DigitalLeader.Models
{
    public class BlogpostContext : DbContext
    {
        public BlogpostContext()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BlogpostConnection"].ConnectionString;
            this.Database.Connection.ConnectionString = connectionString;
        }
        public DbSet<Blogpost> Blogposts { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
    public class Blogpost
    {
        public int ID { get; set; }
        public DateTime PublishedOn { get; set; }
        public bool Visible { get; set; }
        public int Views { get; set; }

        // EN
        public string Title { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public string Keywords { get; set; }
        public string CategoryID { get; set; }

        // UA
        public string TitleUA { get; set; }
        [AllowHtml]
        public string ContentUA { get; set; }
        public string KeywordsUA { get; set; }
        public string CategoryUA { get; set; }

        public virtual Category Category { get; set; }
    }

    public class Category
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public int Count { get; set; }

        public virtual ICollection<Blogpost> Blogposts { get; set; }
    }
}