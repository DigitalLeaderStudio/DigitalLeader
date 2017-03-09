namespace DigitalLeader.Web.MappingProfiles
{
    using AutoMapper;
    using DigitalLeader.Entities;
    using DigitalLeader.ViewModels;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Linq;

    public class ServiceSubcategoryProfile : Profile
    {
        public ServiceSubcategoryProfile()
        {
            CreateMap<ServiceSubcategory, SelectListItem>().ConvertUsing(
                (src, target) =>
                {
                    return new SelectListItem
                    {
                        Text = src.Name,
                        Value = src.ID.ToString()
                    };
                });

            CreateMap<ServiceSubcategory, ServiceSubcategoryViewModel>();

            CreateMap<ServiceCategoryViewModel, ServiceCategory>();
            //.AfterMap((vm, entity) =>
            //{
            //	if (vm.File != null)
            //	{
            //		entity.Image = (File)MapperImageConverter.ImageConverter(vm);
            //	}
            //});
        }
    }
}