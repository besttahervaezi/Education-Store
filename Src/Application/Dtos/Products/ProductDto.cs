﻿using Application.Common.Mapping;
using Application.Common.Mapping.Resolvers;
using Application.Dtos.Common;
using AutoMapper;
using Domain.Entities;

namespace Application.Dtos.Products;

public class ProductDto:CommandDto,IMapFrom<Product>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string PictureUrl { get; set; }
    public int ProductTypeId { get; set; }
    public int ProductBrandId { get; set; }
    public string producttype { get; set; } //title
    public string productbrand { get; set; } //title
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductDto>().ForMember(x=>x.PictureUrl,c=>c.MapFrom<ProductimageurlResolver>())
            .ForMember(x => x.productbrand, c => c.MapFrom(v => v.productbrand.Title))
            .ForMember(x=>x.producttype,c=>c.MapFrom(v=>v.producttype.Title));

    }
}