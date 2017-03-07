namespace DigitalLeader.DAL.Migrations
{
	using DigitalLeader.Entities;
	using DigitalLeader.Entities.Identity;
	using Microsoft.AspNet.Identity;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity.Migrations;
	using System.IO;
	using System.Reflection;

	internal sealed class Configuration : DbMigrationsConfiguration<DigitalLeader.DAL.ApplicationDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(DigitalLeader.DAL.ApplicationDbContext context)
		{
			#region Users
			
			var roleAdmin = new Role
			{
				Name = "Admin"
			};
			var roleContributor = new Role
			{
				Name = "Contributor"
			};
			context.Roles.AddOrUpdate(r => r.Name, roleAdmin, roleContributor);
			context.SaveChanges();

			var user = new User
			{
				UserName = "admin@digitalleader.solutions",
				Email = "admin@digitalleader.solutions",
				EmailConfirmed = true,
				SecurityStamp = "random",
				PasswordHash = new PasswordHasher().HashPassword("ProjectSpartan63")
			};
			user.Roles.Add(new UserRole
			{
				RoleId = roleAdmin.Id,
				UserId = user.Id
			});			
			context.Users.AddOrUpdate(u => u.Email, user);

			context.SaveChanges();

			var user1 = new User
			{
				UserName = "Atnon Valintsev",
				Email = "valintsev.anton@gmail.com",
				EmailConfirmed = true,
				SecurityStamp = "random",
				PasswordHash = new PasswordHasher().HashPassword("ProjectSpartan63")
			};
			user1.Roles.Add(new UserRole
			{
				RoleId = roleContributor.Id,
				UserId = user1.Id
			});			
			context.Users.AddOrUpdate(u => u.Email, user1);
			context.SaveChanges();
			
			var user2 = new User
			{
				UserName = "Serhii Kalaida",
				Email = "s.kalaida.biz@gmail.com",
				EmailConfirmed = true,
				SecurityStamp = "random",
				PasswordHash = new PasswordHasher().HashPassword("ProjectSpartan63")
			};
			user2.Roles.Add(new UserRole
			{
				RoleId = roleContributor.Id,
				UserId = user2.Id
			});
			context.Users.AddOrUpdate(u => u.Email, user2);
			context.SaveChanges();

			#endregion

			#region Categories

			var outsorcingCategory = new Category
				{
					Name = "Technology Solutions",
					Content = @"IT outsourcing is the way of use of external information technology service provider to effectively deliver IT-enabled business process and infrastructure solutions for its client. It includes many information technology services such as software as a service and cloud services. Your enterprise can benefit by delegating IT related processes to information technology outsourcing company. Those benefits include costs reduction, marketing life cycle acceleration, and external expertise, assets and intellectual property exploitation.",
					Services = new List<Service>(),
					Image = new DigitalLeader.Entities.File
					{
						ContentType = "image/svg+xml",
						FileName = "outsourcing.svg",
						Content = System.IO.File.ReadAllBytes(MapPath("~/../../DigitalLeader.Web/Content/Images/services/outsourcing.svg"))
					}
				};
			context.Categories.AddOrUpdate(c => c.Name, outsorcingCategory);

			var marketingCategory = new Category
				{
					Name = "Digital Marketing",
					Content = @"This is the most important method of marketing your products in today's economy. The volume of sales made via Internet or digital devices increases with every day. Digital Marketing consists of many modern methodologies and techniques, which if used and performed properly can sky rocket your business revenues. Friendly saying, if you don't do Digital Marketing or do it badly, your businesses becomes dead very soon in the severe ocean of competition.",
					Services = new List<Service>(),
					Image = new DigitalLeader.Entities.File
					{
						ContentType = "image/svg+xml",
						FileName = "marketing.svg",
						Content = System.IO.File.ReadAllBytes(MapPath("~/../../DigitalLeader.Web/Content/Images/services/marketing.svg"))
					}
				};
			context.Categories.AddOrUpdate(c => c.Name, marketingCategory);

			context.SaveChanges();

			#endregion

			#region Services

			#region Outsorcing services

			outsorcingCategory.Services.Add(new Service
			{
				Title = "Software infrastructure",
				Description = "We'll take care of your in-house software"
			});

			outsorcingCategory.Services.Add(new Service
			{
				Title = "Cloud management",
				Description = "All your data is safe and sound in the digital cloud"
			});

			outsorcingCategory.Services.Add(new Service
			{
				Title = "Software as a Service",
				Description = "You won't buy any software, only pay when you use it"
			});

			#endregion

			#region Marketing services

			marketingCategory.Services.Add(new Service
			{
				Title = "Marketing strategy",
				Description = "Reach your customers precisly and fastly"
			});

			marketingCategory.Services.Add(new Service
			{
				Title = "Analytics & optimization",
				Description = "Reach your customers precisly and fastly"
			});

			marketingCategory.Services.Add(new Service
			{
				Title = "Analytics & optimization",
				Description = "Tune your digital marketing channels to be effective"
			});

			#endregion

			context.SaveChanges();

			#endregion

			#region Technologies

			context.Technologies.AddOrUpdate(t => t.Name,
				new Technology { Name = "C#" },
				new Technology { Name = "ASP.NET MVC" },
				new Technology { Name = "Entity Framework" },
				new Technology { Name = "MS SQL Server" },
				new Technology { Name = "Less" },
				new Technology { Name = "HMTL 5" },
				new Technology { Name = "Bootstrap" },
				new Technology { Name = "Materialize" },
				new Technology { Name = "jQuery" },
				new Technology { Name = "NopCommerce" },
				new Technology { Name = "Ajax" },
				new Technology { Name = "Knockout" },
				new Technology { Name = "Angular" },
				new Technology { Name = "Web Serivces" },
				new Technology { Name = "Web API" }
				);

			#endregion
		}

		private string MapPath(string seedFile)
		{
			var absolutePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
			var directoryName = Path.GetDirectoryName(absolutePath);
			var path = Path.Combine(directoryName, ".." + seedFile.TrimStart('~').Replace('/', '\\'));

			return path;
		}
	}
}
