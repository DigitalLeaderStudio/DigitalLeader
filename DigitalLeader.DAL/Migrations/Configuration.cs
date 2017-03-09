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

			#region ServiceCategories

			var technologyCategory = new ServiceCategory
				{
					Name = "Technology Solutions",
					Content = @"IT outsourcing is the way of use of external information technology service provider to effectively deliver IT-enabled business process and infrastructure solutions for its client. It includes many information technology services such as software as a service and cloud services. Your enterprise can benefit by delegating IT related processes to information technology outsourcing company. Those benefits include costs reduction, marketing life cycle acceleration, and external expertise, assets and intellectual property exploitation.",
					ServiceSubcategories = new List<ServiceSubcategory>(),
                    CssClass = "technology",
					Image = new DigitalLeader.Entities.File
					{
						ContentType = "image/svg+xml",
						FileName = "technology-glyph-icon.png",
						Content = System.IO.File.ReadAllBytes(MapPath("~/../../DigitalLeader.Web/Content/Images/services/technology-glyph-icon.png"))
					}
				};

            #region Technology Services Subcategories 

            var developmentServiceSubcategory = new ServiceSubcategory
            {
                Name = "Development",
                Services = new List<Service>(),
            };
            technologyCategory.ServiceSubcategories.Add(developmentServiceSubcategory);

            var designServiceSubcategory = new ServiceSubcategory
            {
                Name = "Design",
                Services = new List<Service>(),
            };
            technologyCategory.ServiceSubcategories.Add(designServiceSubcategory);

            var maintenanceServiceSubcategory = new ServiceSubcategory
            {
                Name = "Meaintenance",
                Services = new List<Service>(),
            };
            technologyCategory.ServiceSubcategories.Add(maintenanceServiceSubcategory);

            #endregion

            context.ServiceCategories.AddOrUpdate(c => c.Name, technologyCategory);

            var marketingCategory = new ServiceCategory
				{
					Name = "Digital Marketing",
					Content = @"This is the most important method of marketing your products in today's economy. The volume of sales made via Internet or digital devices increases with every day. Digital Marketing consists of many modern methodologies and techniques, which if used and performed properly can sky rocket your business revenues. Friendly saying, if you don't do Digital Marketing or do it badly, your businesses becomes dead very soon in the severe ocean of competition.",
					ServiceSubcategories = new List<ServiceSubcategory>(),
                    CssClass = "marketing",
					Image = new DigitalLeader.Entities.File
					{
						ContentType = "image/svg+xml",
						FileName = "marketing-glyph-icon.png",
						Content = System.IO.File.ReadAllBytes(MapPath("~/../../DigitalLeader.Web/Content/Images/services/marketing-glyph-icon.png"))
					}
				};

            #region Marketing Services Subcategories
            var contentServiceSubcategory = new ServiceSubcategory
            {
                Name = "Content",
                Services = new List<Service>(),
            };
            marketingCategory.ServiceSubcategories.Add(contentServiceSubcategory);

            var smmServiceSubcategory = new ServiceSubcategory
            {
                Name = "SMM",
                Services = new List<Service>(),
            };
            marketingCategory.ServiceSubcategories.Add(smmServiceSubcategory);

            var paidServiceSubcategory = new ServiceSubcategory
            {
                Name = "Paid",
                Services = new List<Service>(),
            };
            marketingCategory.ServiceSubcategories.Add(paidServiceSubcategory);

            #endregion

            context.ServiceCategories.AddOrUpdate(c => c.Name, marketingCategory);

			context.SaveChanges();

            #endregion

            #region Services

            #region Development services

            developmentServiceSubcategory.Services.Add(new Service
            {
                Title = "Website",
                Description = "Some text"
            });

            developmentServiceSubcategory.Services.Add(new Service
            {
                Title = "Design",
                Description = "Some text"
            });

            developmentServiceSubcategory.Services.Add(new Service
            {
                Title = "Web Application",
                Description = "Some text"
            });

            developmentServiceSubcategory.Services.Add(new Service
            {
                Title = "Mobile Application",
                Description = "Some text"
            });

            developmentServiceSubcategory.Services.Add(new Service
            {
                Title = "Enterprise System",
                Description = "Some text"
            });

            developmentServiceSubcategory.Services.Add(new Service
            {
                Title = "Development consulting",
                Description = "Some text"
            });

            designServiceSubcategory.Services.Add(new Service
            {
                Title = "UI/UX Design",
                Description = "Some text"

            });

            designServiceSubcategory.Services.Add(new Service
            {
                Title = "Brand Identity Design",
                Description = "Some text"

            });

            designServiceSubcategory.Services.Add(new Service
            {
                Title = "Brand Identity Design",
                Description = "Some text"

            });

            designServiceSubcategory.Services.Add(new Service
            {
                Title = "Graphic Design",
                Description = "Some text"

            });

            designServiceSubcategory.Services.Add(new Service
            {
                Title = "Design Consulting",
                Description = "Some text"

            });

            maintenanceServiceSubcategory.Services.Add(new Service
            {
                Title = "Software Upgrade",
                Description = "Some text"

            });

            #endregion

            #region Marketing services

            contentServiceSubcategory.Services.Add(new Service
            {
                Title = "Copy Wrighting",
                Description = "Reach your customers precisly and fastly"
            });


            smmServiceSubcategory.Services.Add(new Service
            {
                Title = "Facebook",
                Description = "Reach your customers precisly and fastly"
            });

            paidServiceSubcategory.Services.Add(new Service
            {
                Title = "Google",
                Description = "Reach your customers precisly and fastly"
            });


            #endregion

            #endregion

            context.SaveChanges();

			

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
