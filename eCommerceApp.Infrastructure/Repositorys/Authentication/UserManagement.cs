using eCommerceApp.Domain.Services.Interfaces.Authentication;
using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace eCommerceApp.Infrastructure.Repositorys.Authentication;

public class UserManagement(UserManager<ApplicationUser> userManager, IRoleManagement roleManagement, ApplicationDbContext context) : IUserManagement
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IRoleManagement _roleManagement = roleManagement;
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> CreateUser(ApplicationUser user)
    {
        if (await GetUserByEmail(user.Email!) != null)
            return false;

        return (await _userManager.CreateAsync(user, user.PasswordHash!)).Succeeded;
    }
    public async Task<IEnumerable<ApplicationUser>?> GetAllUsers() => await _context.ApplicationUsers.AsNoTracking().ToListAsync();
    public async Task<ApplicationUser?> GetUserByEmail(string email) => await _userManager.FindByEmailAsync(email);
    public async Task<ApplicationUser> GetUserById(string id) => (await _userManager.FindByIdAsync(id))!;
    public async Task<List<Claim>> GetUserClaims(string email)
    {
        var _user = await GetUserByEmail(email);
        string? roleName = await _roleManagement.GetUserRole(email);

        List<Claim> claims = [
            new Claim("Fullname", _user!.FullName!),
            new Claim(ClaimTypes.NameIdentifier, _user!.Id!),
            new Claim(ClaimTypes.Email, _user.Email!),
            new Claim(ClaimTypes.Role, roleName!)
        ];

        return claims;
    }
    public async Task<bool> LoginUser(ApplicationUser user)
    {
        if (await GetUserByEmail(user.Email!) is not { } _user)
            return false;

        string? roleName = await _roleManagement.GetUserRole(_user.Email);
        if(string.IsNullOrEmpty(roleName))
            return false;

        return await _userManager.CheckPasswordAsync(_user, user.PasswordHash);
    }
    public async Task<int> RemoveUserByEmail(string email)
    {
        var user = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Email == email);
        _context.Remove(user);

        return await _context.SaveChangesAsync();
    }
}


