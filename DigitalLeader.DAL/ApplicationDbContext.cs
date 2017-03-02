namespace DigitalLeader.DAL
{
	using DigitalLeader.Entities;
	using DigitalLeader.Entities.Identity;
	using EntityFramework.DbContextScope.Interfaces;
	using Microsoft.AspNet.Identity.EntityFramework;
	using System.Data.Entity;

	public class ApplicationDbContext :
		IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>, IDbContext
	{
		public ApplicationDbContext()
			: base("DegitalLeaderConnection")
		{
		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}

		#region DbSets

		public virtual DbSet<Blogpost> Blogposts { get; set; }

		public virtual DbSet<Category> Categories { get; set; }

		public virtual DbSet<Project> Projects { get; set; }

		public virtual DbSet<Service> Services { get; set; }

		public virtual DbSet<Client> Clients { get; set; }

		public virtual DbSet<Industry> Industries { get; set; }

		public virtual DbSet<Technology> Technologies { get; set; }

		#endregion

		protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>().ToTable("Users");
			modelBuilder.Entity<UserClaim>().ToTable("UserClaims");
			modelBuilder.Entity<UserLogin>().ToTable("UserLogins");
			modelBuilder.Entity<UserRole>().ToTable("UserRoles");
			modelBuilder.Entity<Role>().ToTable("Roles");

			#region User (Employee) relations
			modelBuilder.Entity<User>()
						.HasMany(u => u.Services)
						.WithMany(s => s.Employees)
						.Map(map =>
						{
							map.ToTable("EmployeesWithServices");
							map.MapLeftKey("UserId");
							map.MapRightKey("ServiceId");
						});

			modelBuilder.Entity<User>()
						.HasMany(u => u.Technologies)
						.WithMany(t => t.Employees)
						.Map(map =>
						{
							map.ToTable("EmployeesWithTechnologies");
							map.MapLeftKey("UserId");
							map.MapRightKey("TechnologyId");
						});

			#endregion

			#region Project relations
			modelBuilder.Entity<Project>()
					.HasMany(p => p.Contributors)
					.WithMany(u => u.Projects)
					.Map(map =>
					{
						map.ToTable("ContributorsWithProjects");
						map.MapLeftKey("ProjectId");
						map.MapRightKey("UserId");
					});

			modelBuilder.Entity<Project>()
				.HasMany(p => p.Technologies)
				.WithMany(t => t.Projects)
				.Map(map =>
				{
					map.ToTable("TechnologiesWithProjects");
					map.MapLeftKey("ProjectId");
					map.MapRightKey("TechnologyId");
				});

			modelBuilder.Entity<Project>()
				.HasMany(p => p.Services)
				.WithMany(s => s.Projects)
				.Map(map =>
				{
					map.ToTable("ProjectsWithProjects");
					map.MapLeftKey("ProjectId");
					map.MapRightKey("ServiceId");
				});
			#endregion

			#region Clients relations
			modelBuilder.Entity<Client>()
							.HasMany(c => c.Industries)
							.WithMany(i => i.Clients)
							.Map(map =>
							{
								map.ToTable("ClientsWithIndustries");
								map.MapLeftKey("ClientId");
								map.MapRightKey("IndustryId");
							});
			#endregion
		}
	}
}
