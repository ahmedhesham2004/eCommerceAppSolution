using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Cart;

namespace eCommerceApp.Application.Services.Interfaces.Cart;
public interface ICartServices
{
    Task<ServiceResponse> SaveCheckoutAsync(IEnumerable<CreateAchieve> achieves);
    Task<ServiceResponse> CheckoutAsync(Checkout checkout);
}
