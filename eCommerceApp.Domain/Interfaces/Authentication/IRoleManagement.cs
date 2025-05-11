using eCommerceApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Domain.Services.Interfaces.Authentication;
public interface IRoleManagement
{
    Task<string?> GetUserRole(string userEmail);
    Task<bool> AddUserToRole(ApplicationUser user, string roleName);
}
