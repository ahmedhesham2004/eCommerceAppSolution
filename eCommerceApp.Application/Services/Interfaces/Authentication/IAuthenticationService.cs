using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Identity;

namespace eCommerceApp.Application.Services.Interfaces.Authentication;
public interface IAuthenticationService
{
    Task<ServiceResponse> CreateUserAsync(CreateUser user);
    Task<LoginResponse> LoginUserAsync(LoginUser user);
    Task<LoginResponse> ReviveTokenAsync(string refreshToken);
}
