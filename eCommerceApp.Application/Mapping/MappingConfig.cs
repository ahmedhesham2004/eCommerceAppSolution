using AutoMapper;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Domain.Entities;

namespace eCommerceApp.Application.Mapping;
public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<CreateProductRequest, Product>();
        CreateMap<CreateCategoryRequest, Category>();

        CreateMap<UpdateProductRequest, Product>();
        CreateMap<UpdateCategoryRequest, Category>();

        CreateMap<Product, ProductResponse>();
        CreateMap<Category, CategoryResponse>();

    }
}

