using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Domain.Entities;
using Stripe.Checkout;

namespace eCommerceApp.Infrastructure.Services;
public class StripePaymentService : IPaymentService
{
    public async Task<ServiceResponse> Pay(decimal totalAmount, IEnumerable<Product> cartProducts, IEnumerable<ProcessCart> carts)
    {
        try
        {
            var lineItems = new List<SessionLineItemOptions>();
            foreach (var item in cartProducts)
            {
                var pQuantity = carts.FirstOrDefault(x => x.ProductId == item.Id);
                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Name,
                            Description = item.Description
                        },
                        UnitAmountDecimal = (long)(item.Price * 100),
                    },
                    Quantity = pQuantity!.Quantity,
                });
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = ["usd"],
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https://localhost:7082/payment-success",
                CancelUrl = "https://localhost:7082/payment-cancel"
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return new ServiceResponse (true , session.Url);
        }
        catch (Exception e)
        {
            return new ServiceResponse(false, e.Message);
        }
    }
}
