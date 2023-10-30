using Microsoft.AspNetCore.Identity;
using Web_API_JWT.Models;
using Web_API_JWT.Services.Interfaces;

namespace Web_API_JWT.Services;

public class AuthService : IAuth
{

    private readonly UserManager<IdentityUser> _userManager;

    public AuthService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> AddToRoleAsync(IdentityUser user, string role)
    {

        try
        {
            var result = await _userManager.AddToRoleAsync(user, role);
            return result;
        }
        catch (Exception ex)
        {
            return IdentityResult.Failed(new IdentityError { Description = ex.Message });
        }
    }

    public async Task<bool> CheckPasswordAsync(IdentityUser user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<IdentityResult> CreateAsync(IdentityUser user, string password)
    {
        try
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }
        catch (Exception ex)
        {

           throw new Exception(ex.Message);


        }
    }


    public async Task<IdentityUser> FindByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<IList<string>> GetRolesAsync(IdentityUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }
}
