using AutoMapper;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Domain.Interfaces.Cart;

namespace eCommerceApp.Application.Services.Implementations.Cart;
public class PaymentMethodService(IPaymentMethod paymentMethod, IMapper mapper) : IPaymentMethodService
{
    private readonly IPaymentMethod _paymentMethod = paymentMethod;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<GetPaymentMethod>> GetPaymentMethodsAsync()
    {
        var methods = await _paymentMethod.GetPaymentMethodAsync();
        if (!methods.Any())
            return [];

        return _mapper.Map<IEnumerable<GetPaymentMethod>>(methods);

    }
}
