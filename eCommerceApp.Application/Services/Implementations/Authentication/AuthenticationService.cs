using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Identity;
using eCommerceApp.Application.Services.Interfaces.Authentication;
using eCommerceApp.Application.Services.Interfaces.Logging;
using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Services.Interfaces.Authentication;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Application.Services.Implementations.Authentication;
public class AuthenticationService( 
    IUserManagement userManagement,
    ITokenManagement tokenManagement,
    IRoleManagement roleManagement,
    IAppLogger<AuthenticationService> logger,
    IMapper mapper) : IAuthenticationService
{
    private readonly IUserManagement _userManagement = userManagement;
    private readonly ITokenManagement _tokenManagement = tokenManagement;
    private readonly IRoleManagement _roleManagement = roleManagement;
    private readonly IAppLogger<AuthenticationService> _logger = logger;
    private readonly IMapper _mapper = mapper;

    public async Task<ServiceResponse> CreateUserAsync(CreateUser user) 
    {
        var mappedUser = _mapper.Map<ApplicationUser>(user);
        mappedUser.UserName = user.Email;
        mappedUser.PasswordHash = user.Password;

        var result = await _userManagement.CreateUser(mappedUser);
        if (!result)
            return new ServiceResponse { Message = "Email Address might be already i use or unknown error occurred" };

        var _user = await _userManagement.GetUserByEmail(user.Email);
        var users = await _userManagement.GetAllUsers();
        var assignRoleResult = await _roleManagement.AddUserToRole(_user!, users!.Count() > 1 ? "User" : "Admin");

        if (!assignRoleResult)
        {
            var removeUserResult = await _userManagement.RemoveUserByEmail(_user!.Email!);
            if (removeUserResult <= 0)
            {
                logger.LogError(new Exception("User Creation Failed"), "User Creation Failed");
                return new ServiceResponse { Message = "Error occurred in creating account" };
            }
        }

        return new ServiceResponse { Success = true ,Message = "Account created!" };
    }
    public async Task<LoginResponse> LoginUserAsync(LoginUser user)
    {
        var mappedUser = mapper.Map<ApplicationUser>(user);
        mappedUser.PasswordHash = user.Password;

        var loginResult = await _userManagement.LoginUser(mappedUser);
        if (!loginResult)
            return new LoginResponse(Message: "Email not found or invalid credentials");
    
        var _user = await _userManagement.GetUserByEmail(user.Email);
        var claims = await _userManagement.GetUserClaims(_user!.Email!);

        var jwtToken = _tokenManagement.GenerateToken(claims);
        var refreshToken = _tokenManagement.GetRefreshToken();

        var saveTokenResult = 0;
        var userTokenCheck = await tokenManagement.CheckRefreshToken(_user.Id);
        if (userTokenCheck)
            await tokenManagement.UpdateRefreshToken(_user.Id, refreshToken);
        else
            saveTokenResult = await tokenManagement.AddRefreshToken(_user.Id, refreshToken);

        return saveTokenResult < 0
            ? new LoginResponse(Message: "Token Generation Failed")
            : new LoginResponse(Success: true, Token: jwtToken, RefreshToken: refreshToken);
    }
    public async Task<LoginResponse> ReviveTokenAsync(string refreshToken)
    {
        var isValidRefreshToken = await _tokenManagement.ValidateRefreshToken(refreshToken);
        if (!isValidRefreshToken)
            return new LoginResponse(Message: "Invalid Token");

        var userId = await _tokenManagement.GetUserIdByRefreshToken(refreshToken);
        var user = await _userManagement.GetUserById(userId);

        var claims = await _userManagement.GetUserClaims(user!.Email!);
        var newJwtToken = _tokenManagement.GenerateToken(claims);
        var newRefreshToken = _tokenManagement.GetRefreshToken();
        await _tokenManagement.UpdateRefreshToken(user.Id, newRefreshToken);

        return new LoginResponse(Success: true, Token: newJwtToken, RefreshToken: newRefreshToken);
    }
}
