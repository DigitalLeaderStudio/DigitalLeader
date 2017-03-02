namespace DigitalLeader.Web.MappingProfiles
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.ViewModels;
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

			CreateMap<Blogpost, BlogpostViewModel>();

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