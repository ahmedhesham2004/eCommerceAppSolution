using eCommerceApp.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class PaymentController(IPaymentMethodService paymentMethodService) : ControllerBase
{
    private readonly IPaymentMethodService _paymentMethodService = paymentMethodService;

    [HttpGet("")]
    public async Task<IActionResult> GetPaymentMethods()
    {
        var result = await _paymentMethodService.GetPaymentMethodsAsync();

        return result.Any() ? Ok(result) : NotFound(result);
    }
}
