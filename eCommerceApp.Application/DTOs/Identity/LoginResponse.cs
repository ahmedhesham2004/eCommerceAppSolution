namespace eCommerceApp.Application.DTOs.Identity;
public record LoginResponse
(
    bool Success = false,
    string Message = null!,
    string Token = null!,
    string RefreshToken = null!
);
