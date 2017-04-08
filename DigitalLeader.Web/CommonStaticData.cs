using DigitalLeader.Web.Configuration;
using System.Configuration;

namespace DigitalLeader.Web
{
	//TODO: use cache instead static data
	public static class CommonStaticData
	{
		static CultureSection _cultures = null;
		public static CultureSection CulturesSection
		{
			set { _cultures = value; }
			get
			{
				if (_cultures == null)
				{
					_cultures = ConfigurationManager.GetSection("SiteCultures") as CultureSection;
				}

				return _cultures;
			}
		}
	}
}