namespace DigitalLeader.Web
{
	using Autofac;
	using Autofac.Integration.Mvc;
	using DigitalLeader.Services.Implementation;
	using DigitalLeader.Services.Interfaces;
	using DigitalLeader.Web;
	using EntityFramework.DbContextScope;
	using EntityFramework.DbContextScope.Interfaces;
	using System.Web.Mvc;


	public class AutofacConfig
	{
		public static void ConfigureContainer()
		{
			var builder = new ContainerBuilder();

			// Register dependencies in controllers
			builder.RegisterControllers(typeof(MvcApplication).Assembly);

			// Register dependencies in filter attributes
			builder.RegisterFilterProvider();

			// Register dependencies in custom views
			builder.RegisterSource(new ViewRegistrationSource());

			// Register our Data dependencies
			//builder.RegisterModule(new DataModule("MVCWithAutofacDB"));
			builder.RegisterType<DbContextScopeFactory>().As<IDbContextScopeFactory>();
			
			builder.RegisterType<ProjectService>().As<IProjectService>();
			builder.RegisterType<ClientService>().As<IClientService>();
			builder.RegisterType<FileService>().As<IFileService>();


			var container = builder.Build();

			// Setup global sitemap loader (required)
			//MvcSiteMapProvider.SiteMaps.Loader = container.Resolve<ISiteMapLoader>();

			//XmlSiteMapController.RegisterRoutes(RouteTable.Routes);

			// Set MVC DI resolver to use our Autofac container
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}