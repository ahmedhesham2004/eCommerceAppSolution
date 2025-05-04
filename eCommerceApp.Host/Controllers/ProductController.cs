using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class ProductController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productService.GetAllAsync();

        return result.Any() ? Ok(result) : NotFound(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await _productService.GetByIdAsync(id);

        return result != null ? Ok(result) : NotFound(result);
    }

    [HttpPost("")]
    public async Task<IActionResult> Add([FromBody] CreateProductRequest request)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _productService.AddAsync(request);

        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPut("")]
    public async Task<IActionResult> Update([FromBody] UpdateProductRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _productService.UpdateAsync(request);

        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await _productService.DeleteAsync(id);

        return result.Success ? Ok(result) : BadRequest(result);
    }
}
