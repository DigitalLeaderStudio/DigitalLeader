namespace DigitalLeader.Web.Areas.Admin.Controllers
{
	using DigitalLeader.Entities.Identity;
	using DigitalLeader.ViewModels.Localization;
	using DigitalLeader.Web.Configuration;
	using DigitalLeader.Web.Managers;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.Owin;
	using System;
	using System.Collections.Generic;
	using System.Configuration;
	using System.Web;
	using System.Web.Mvc;

	[Authorize(Roles = "Admin")]
	public class BaseAdminController : Controller
	{
		#region Properties

		private ApplicationSignInManager _signInManager;
		private ApplicationUserManager _userManager;

		public ApplicationSignInManager SignInManager
		{
			get
			{
				return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
			}
			private set
			{
				_signInManager = value;
			}
		}

		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		}

		public User LoggedUser { get; private set; }

		public int LoggetUserID
		{
			get
			{
				return User.Identity.GetUserId<int>();
			}
		}

		#endregion

		protected override void Initialize(System.Web.Routing.RequestContext requestContext)
		{
			base.Initialize(requestContext);
			
			InitLoggedUser();
		}

		public void InitLoggedUser()
		{
			LoggedUser = UserManager.FindById<User, int>(LoggetUserID);

			ViewBag.User = LoggedUser;
		}

		/// <summary>
		/// Add locales for localizable entities
		/// </summary>
		/// <typeparam name="TLocalizedModelLocal">Localizable model</typeparam>
		/// <param name="languageService">Language service</param>
		/// <param name="locales">Locales</param>
		/// <param name="configure">Configure action</param>
		protected virtual void AddLocales<TLocalizedModelLocal>(
			IList<TLocalizedModelLocal> locales, 
			Action<TLocalizedModelLocal, int> configure) where TLocalizedModelLocal : ILocalizedModelLocal
		{
			CultureSection cultureSection = ConfigurationManager.GetSection("SiteCultures") as CultureSection;
			
			foreach (var language in cultureSection.Cultures)
			{
				var locale = Activator.CreateInstance<TLocalizedModelLocal>();
				locale.LanguageId = language.Id;
				locale.LanguageName = language.Name;

				if (configure != null)
				{
					configure.Invoke(locale, locale.LanguageId);
				}

				locales.Add(locale);
			}
		}
	}
}