using Microsoft.AspNetCore.Identity;

namespace Projekt_Avancerad.NET.Authentication
{
    public interface IUserService
    {
        Task<string>AuthenticateAsync(string username, string password);
        Task SeedRolesAndUsersAync();
        string GenerateJwtToken(IdentityUser user, IList<string> roles);
    }
}

