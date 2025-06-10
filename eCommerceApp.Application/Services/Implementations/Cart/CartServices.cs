using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Domain.Interfaces.Cart;

namespace eCommerceApp.Application.Services.Implementations.Cart;
internal class CartServices(ICart cartRepository, IMapper mapper, IGeneric<Product> productRepository,
    IPaymentMethodService paymentMethodService,
    IPaymentService paymentService) : ICartServices
{
    private readonly ICart _cartRepository = cartRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IGeneric<Product> _productRepository = productRepository;
    private readonly IPaymentMethodService _paymentMethodService = paymentMethodService;
    private readonly IPaymentService _paymentService = paymentService;

    public async Task<ServiceResponse> CheckoutAsync(Checkout checkout)
    {
        var (products, totalAmount) = await GetCartTotalAmount(checkout.Carts);
        var paymentMethods = await _paymentMethodService.GetPaymentMethodsAsync();

        if(checkout.PaymentMethodId == paymentMethods.FirstOrDefault().Id)
            return await _paymentService.Pay(totalAmount, products, checkout.Carts);
        else
            return new ServiceResponse(false, "Invalid payment mathod");
    }

    public async Task<ServiceResponse> SaveCheckoutAsync(IEnumerable<CreateAchieve> achieves)
    {
        var cart = mapper.Map<IEnumerable<Achieve>>(achieves);
        var result = await _cartRepository.SaveCheckoutAsync(cart);

        return result > 0
            ? new ServiceResponse { Success = true, Message = "Checkout Achieved", }
            : new ServiceResponse { Success = true, Message = "Failed to save checkout history", };

    }

    private async Task<(IEnumerable<Product>, decimal)> GetCartTotalAmount(IEnumerable<ProcessCart> carts)
    {
        if (!carts.Any())
            return ([], 0);

        var products = await _productRepository.GetAllAsync();
        if (!products.Any())
            return ([], 0);

        var cartProducts = carts
            .Select(cartItem => products.FirstOrDefault(p => p.Id == cartItem.ProductId))
            .Where(product => product != null)
            .ToList();

        var totalAmount = carts
            .Where(cartItem => cartProducts.Any(p => p.Id == cartItem.ProductId))
            .Sum(cartItem => cartItem.Quantity * cartProducts.First(p => p.Id == cartItem.ProductId)!.Price);
        return (cartProducts!, totalAmount);
    }
}