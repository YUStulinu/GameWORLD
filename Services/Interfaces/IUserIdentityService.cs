using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace GameWORLD.Services.Interfaces
{
    public interface IUserIdentityService
    {
        Task<IdentityResult> RegisterAsync(string email, string password);
        Task SignOutAsync();
        Task<List<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();
        bool requireConfirmation();
        bool supportsUserEmail();

        IUserStore<IdentityUser> GetUserEmailStore();

        Task<SignInResult> LoginAsync(string email, string password, bool rememberMe);
    }
}
