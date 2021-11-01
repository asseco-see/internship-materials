using AutoMapper;
using Product.Commands;
using Product.Database.Entities;
using Product.Models;

namespace Product.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductEntity, Models.Product>()
                .ForMember(d => d.ProductCode, opts => opts.MapFrom(s => s.Code));

            CreateMap<PagedSortedList<ProductEntity>, PagedSortedList<Models.Product>>();

            CreateMap<CreateProductCommand, ProductEntity>()
            .ForMember(d => d.Code, opts => opts.MapFrom(s => s.ProductCode));
        }
    }
}