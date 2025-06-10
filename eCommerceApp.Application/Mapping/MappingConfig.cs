using AutoMapper;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Identity;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Entities.Identity;

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

        CreateMap<CreateUser, ApplicationUser>();
        CreateMap<LoginUser, ApplicationUser>();

        CreateMap<PaymentMethod, GetPaymentMethod>();

        CreateMap<CreateAchieve, Achieve>();
    }
}

