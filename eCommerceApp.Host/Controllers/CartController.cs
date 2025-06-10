using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class CartController(ICartServices cartServices) : ControllerBase
{
    private readonly ICartServices _cartServices = cartServices;

    [HttpPost("")]
    public async Task<IActionResult> Checkout([FromBody] Checkout checkout)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _cartServices.CheckoutAsync(checkout);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("")]
    public async Task<IActionResult> SaveCheckout([FromBody] IEnumerable<CreateAchieve> achieves)
    {
        var result = await _cartServices.SaveCheckoutAsync(achieves);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
