using Microsoft.AspNetCore.Identity;
using Web_API_JWT.Models;

namespace Web_API_JWT.Services.Interfaces;

public interface IAuth
{
    Task<IdentityUser> FindByEmailAsync(string userName);
    Task<bool> CheckPasswordAsync(IdentityUser user, string password);
    Task<IdentityResult> CreateAsync(IdentityUser user, string password);
    Task<IdentityResult> AddToRoleAsync(IdentityUser user, string role);
    Task<IList<string>> GetRolesAsync(IdentityUser user);

}
