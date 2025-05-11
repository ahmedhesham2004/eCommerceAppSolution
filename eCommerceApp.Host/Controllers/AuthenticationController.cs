using eCommerceApp.Application.DTOs.Identity;
using eCommerceApp.Application.Services.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
{
    private readonly IAuthenticationService _authenticationService = authenticationService;

    [HttpPost("")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUser user)
    {
        var result = await _authenticationService.CreateUserAsync(user);

        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUser user)
    {
        var result = await _authenticationService.LoginUserAsync(user);

        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("")]
    public async Task<IActionResult> ReviveToken([FromQuery] string refreshToken)
    {
        var result = await _authenticationService.ReviveTokenAsync(refreshToken);

        return result.Success ? Ok(result) : BadRequest(result);
    }
}
