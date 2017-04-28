namespace DigitalLeader.Web.MappingProfiles
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Localization;
	using DigitalLeader.ViewModels;
	using DigitalLeader.Web.Extensions;
	using System.Web;
	using System.Web.Mvc;

	public class BlogpostProfile : Profile
	{
		public BlogpostProfile()
		{
			CreateMap<Blogpost, SelectListItem>().ConvertUsing(
				(src, target) =>
				{
					return new SelectListItem
					{
						Text = src.Title,
						Value = src.ID.ToString()
					};
				});

			CreateMap<Blogpost, BlogpostViewModel>()
				.ForMember(vm => vm.Author, opt => opt.MapFrom(x => x.Author))				
				.ForMember(vm => vm.Title, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Title, languageId);
				}))
				.ForMember(vm => vm.Keywords, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Keywords, languageId);
				}))
				.ForMember(vm => vm.Overview, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Overview, languageId);
				}))
				.ForMember(vm => vm.Content, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Content, languageId);
				}));

			CreateMap<BlogpostViewModel, Blogpost>();
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