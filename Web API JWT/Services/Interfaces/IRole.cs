using Microsoft.AspNetCore.Identity;

namespace Web_API_JWT.Services.Interfaces;

public interface IRole
{
   
    Task<bool> CreateRoleAsync(string roleName);
}
