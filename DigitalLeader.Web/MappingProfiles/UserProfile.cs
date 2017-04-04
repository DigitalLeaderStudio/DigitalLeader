namespace DigitalLeader.Web.MappingProfiles
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Entities.Identity;
	using DigitalLeader.ViewModels;
	using System.Web.Mvc;
	using System.Linq;

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

			CreateMap<User, UserViewModel>()
				.ForMember(vm => vm.ImageId, opt => opt.MapFrom(user => user.ImageId))
				.ForMember(vm => vm.TechnologiesIds, opt => opt.MapFrom(item => item.Technologies.Select(t => t.ID).ToArray()));

			CreateMap<UserViewModel, User>()
				.AfterMap((vm, entity) =>
				{
					if (vm.File != null)
					{
						entity.Image = (File)MapperImageConverter.ImageConverter(vm);
					}
				});

			CreateMap<RegisterViewModel, User>()
				.AfterMap((vm, entity) =>
				{
					if (vm.File != null)
					{
						entity.Image = (File)MapperImageConverter.ImageConverter(vm);
					}
				});

		}
	}
}