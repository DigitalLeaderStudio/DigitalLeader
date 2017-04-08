namespace DigitalLeader.Web.MappingProfiles
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Entities.Identity;
	using DigitalLeader.Services.Localization;
	using DigitalLeader.ViewModels;
	using DigitalLeader.Web.Extensions;
	using System.Linq;
	using System.Web;
	using System.Web.Mvc;

	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<User, SelectListItem>().ConvertUsing(
				(src, target) =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();					
					return new SelectListItem
					{
						Text = src.GetLocalized(x => x.UserName, languageId),
						Value = src.Id.ToString()
					};
				});

			CreateMap<User, UserViewModel>()
				.ForMember(vm => vm.Biography, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Biography, languageId);
				}))
				.ForMember(vm => vm.Title, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Title, languageId);
				}))
				.ForMember(vm => vm.UserName, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.UserName, languageId);
				}))
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