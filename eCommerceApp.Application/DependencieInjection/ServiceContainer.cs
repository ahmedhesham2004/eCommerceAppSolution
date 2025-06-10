using eCommerceApp.Application.Mapping;
using eCommerceApp.Application.Services.Implementations;
using eCommerceApp.Application.Services.Implementations.Authentication;
using eCommerceApp.Application.Services.Implementations.Cart;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Application.Services.Interfaces.Authentication;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Application.Validation;
using eCommerceApp.Application.Validation.Authentication;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerceApp.Application.DependencieInjection;
public static class ServiceContainer
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingConfig));
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IPaymentMethodService, PaymentMethodService>();
        services.AddScoped<ICartServices, CartServices>();

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
        services.AddScoped<IValidationService, ValidationService>();
        return services;
    }
}
