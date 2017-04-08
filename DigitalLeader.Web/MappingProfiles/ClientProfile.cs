namespace DigitalLeader.Web.MappingProfiles
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Localization;
	using DigitalLeader.Web.Extensions;
	using DigitalLeader.Entities.Identity;
	using DigitalLeader.ViewModels;
	using System.Web;
	using System.Web.Mvc;

	public class ClientProfile : Profile
	{
		public ClientProfile()
		{
			CreateMap<Client, SelectListItem>().ConvertUsing(
				(src, target) =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();

					return new SelectListItem
					{
						Text = string.Format("{0} {1} ({2})",
							src.GetLocalized(x => x.FirstName, languageId),
							src.GetLocalized(x => x.LastName, languageId),
							src.GetLocalized(x => x.Company, languageId)),
						Value = src.ID.ToString()
					};
				});

			CreateMap<Client, ClientViewModel>()
				.ForMember(vm => vm.Title, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Title, languageId);
				}))
				.ForMember(vm => vm.Company, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Company, languageId);
				}))
				.ForMember(vm => vm.FirstName, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.FirstName, languageId);
				}))
				.ForMember(vm => vm.LastName, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.LastName, languageId);
				}))
				.ForMember(viewModel => viewModel.ImageId, opt => opt.MapFrom(client => client.ImageId));

			CreateMap<ClientViewModel, Client>().AfterMap((vm, entity) =>
			{
				if (vm.File != null)
				{
					entity.Image = (File)MapperImageConverter.ImageConverter(vm);
				}
			});


		}
	}
}