using eCommerceApp.Domain.Services.Interfaces.Authentication;
using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace eCommerceApp.Infrastructure.Repositorys.Authentication;

public class TokenManagement(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IConfiguration configuration) : ITokenManagement
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ApplicationDbContext _context = context;
    private readonly IConfiguration _configuration = configuration;

    public async Task<int> AddRefreshToken(string userId, string refreshToken)
    {
        if (await _userManager.FindByIdAsync(userId) is not { } user)
            return 0;

        _context.RefreshTokens.Add(new RefreshToken
        {
            UserId = userId,
            Token = refreshToken
        });

        return await _context.SaveChangesAsync();
    }
    public string GenerateToken(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddHours(5);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: cred
        );


        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public string GetRefreshToken()
    {
        const int byteSize = 64;
        var randomBytes = new byte[byteSize];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);

        return Convert.ToBase64String(randomBytes);
        //var x = WebUtility.UrlEncode(token);
        //return x;
    }
    public async Task<string> GetUserIdByRefreshToken(string refreshToken) 
        => (await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken))!.UserId;
    public List<Claim> GetUserClaimsFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);

        return jwtToken != null ? jwtToken.Claims.ToList() : [];
    }
    public async Task<int> UpdateRefreshToken(string userId, string refreshToken)
    {
        var user = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == userId);
        if (user is null)
            return -1;

        user.Token = refreshToken;
        return await _context.SaveChangesAsync();
    }
    public async Task<bool> ValidateRefreshToken(string refreshToken)
    {
        var user = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);

        return user != null;        
    }

    public async Task<bool> CheckRefreshToken(string Id)
    {
        var user = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == Id);

        return user != null;
    }
}


