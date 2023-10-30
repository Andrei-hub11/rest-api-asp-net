using Microsoft.AspNetCore.Identity;

namespace Web_API_JWT.Services.Interfaces;

public interface IToken
{
    string GenerateJwtToken(IdentityUser user, IEnumerable<string> roles);
}
