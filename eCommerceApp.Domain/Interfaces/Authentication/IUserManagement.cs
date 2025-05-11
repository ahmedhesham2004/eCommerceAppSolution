using eCommerceApp.Domain.Entities.Identity;
using System.Security.Claims;

namespace eCommerceApp.Domain.Services.Interfaces.Authentication;

public interface IUserManagement
{
    Task<bool> CreateUser(ApplicationUser user);
    Task<bool> LoginUser(ApplicationUser user);
    Task<ApplicationUser?> GetUserByEmail(string email);
    Task<ApplicationUser> GetUserById(string id);
    Task<IEnumerable<ApplicationUser>?> GetAllUsers();
    Task<int> RemoveUserByEmail(string email);
    Task<List<Claim>> GetUserClaims(string email);
}
