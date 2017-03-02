namespace DigitalLeader.Web.App_Start
{
	using AutoMapper;
	using DigitalLeader.Web.MappingProfiles;

	public class AutoMapperConfig
	{
		public static void Config()
		{
			Mapper.Initialize(config =>
			{
				config.AddProfiles(new string[] { "DigitalLeader.Web" });
			});
		}
	}
}