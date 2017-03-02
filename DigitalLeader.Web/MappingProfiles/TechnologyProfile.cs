namespace DigitalLeader.Web.MappingProfiles
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.ViewModels;
	using System.Web.Mvc;

	public class TechnologyProfile : Profile
	{
		public TechnologyProfile()
		{
			CreateMap<Technology, SelectListItem>().ConvertUsing(
				(src, target) =>
				{
					return new SelectListItem
					{
						Text = src.Name,
						Value = src.ID.ToString()
					};
				});

			CreateMap<Technology, TechnologyViewModel>();

			CreateMap<TechnologyViewModel, Technology>();
		}
	}
}