namespace DigitalLeader.Web.MappingProfiles
{
	using AutoMapper;
	using DigitalLeader.Entities.Identity;
	using DigitalLeader.ViewModels;
	using System.Web.Mvc;

	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<User, SelectListItem>().ConvertUsing(
				(src, target) =>
				{
					return new SelectListItem
					{
						Text = src.UserName,
						Value = src.Id.ToString()
					};
				});

			CreateMap<User, UserViewModel>();

			CreateMap<UserViewModel, User>();
			//	.AfterMap((vm, entity) =>
			//	{
			//		if (vm.File != null)
			//		{
			//			entity.Image = (File)MapperImageConverter.ImageConverter(vm);
			//		}
			//	});
		}
	}
}