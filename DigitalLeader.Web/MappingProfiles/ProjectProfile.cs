namespace DigitalLeader.Web.MappingProfiles
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Localization;
	using DigitalLeader.ViewModels;
	using DigitalLeader.Web.Extensions;
	using System.Linq;
	using System.Web;

	public class ProjectProfile : Profile
	{
		public ProjectProfile()
		{
			CreateMap<Project, ProjectViewModel>()
				.ForMember(vm => vm.Client, opt => opt.ResolveUsing(e =>
				{
					return Mapper.Map<ClientViewModel>(e.Client);
				}))
				.ForMember(vm => vm.TechnologiesIds, opt => opt.MapFrom(item => item.Technologies.Select(t => t.ID).ToArray()))
				.ForMember(vm => vm.ServicesIds, opt => opt.MapFrom(item => item.Services.Select(s => s.ID).ToArray()))
				.ForMember(vm => vm.ContributorsIds, opt => opt.MapFrom(item => item.Contributors.Select(s => s.Id).ToArray()))
				.ForMember(vm => vm.Logo, opt => opt.Ignore())
				.ForMember(vm => vm.LogoId, opt => opt.MapFrom(project => project.LogoId))
				.ForMember(vm => vm.ImageId, opt => opt.MapFrom(project => project.ImageId))
				.ForMember(vm => vm.Title, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Title, languageId);
				}))
				.ForMember(vm => vm.Kewywords, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Kewywords, languageId);
				}))
				.ForMember(vm => vm.Overview, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Overview, languageId);
				}))
				.ForMember(vm => vm.Objective, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Objective, languageId);
				}))
				.ForMember(vm => vm.WorkOverview, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.WorkOverview, languageId);
				}))
				.ForMember(vm => vm.ResultOverview, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.ResultOverview, languageId);
				}));


			CreateMap<ProjectViewModel, Project>()
				.ForMember(entity => entity.Client, opt => opt.Ignore())
				.ForMember(entity => entity.Technologies, opt => opt.Ignore())
				.ForMember(entity => entity.Services, opt => opt.Ignore())
				.ForMember(entity => entity.Contributors, opt => opt.Ignore())
				.ForMember(entity => entity.Logo, opt => opt.Ignore())
				.AfterMap((vm, entity) =>
				{
					if (vm.Logo != null)
					{
						entity.Logo = (File)MapperImageConverter.ImageConverter(vm.Logo);
					}
					if (vm.File != null)
					{
						entity.Image = (File)MapperImageConverter.ImageConverter(vm);
					}
				});
		}
	}
}