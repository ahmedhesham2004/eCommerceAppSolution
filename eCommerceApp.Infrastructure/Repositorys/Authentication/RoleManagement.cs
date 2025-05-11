using eCommerceApp.Domain.Services.Interfaces.Authentication;
using eCommerceApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace eCommerceApp.Infrastructure.Repositorys.Authentication;
public class RoleManagement(UserManager<ApplicationUser> userManager) : IRoleManagement
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<bool> AddUserToRole(ApplicationUser user, string roleName) =>
        (await _userManager.AddToRoleAsync(user, roleName)).Succeeded;
    public async Task<string?> GetUserRole(string userEmail)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);

        return (await _userManager.GetRolesAsync(user!)).FirstOrDefault();
    }
}
