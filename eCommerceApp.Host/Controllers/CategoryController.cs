using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _categoryService.GetAllAsync();

        return result.Any() ? Ok(result) : NotFound(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await _categoryService.GetByIdAsync(id);

        return result != null ? Ok(result) : NotFound(result);
    }

    [HttpPost("")]
    public async Task<IActionResult> Add([FromBody] CreateCategoryRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _categoryService.AddAsync(request);

        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPut("")]
    public async Task<IActionResult> Update([FromBody] UpdateCategoryRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _categoryService.UpdateAsync(request);

        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await _categoryService.DeleteAsync(id);

        return result.Success ? Ok(result) : BadRequest(result);
    }
}

