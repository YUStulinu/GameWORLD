using GameWORLD.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace GameWORLD.Services
{
    public class UserIdendityService : IUserIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<UserIdendityService> _logger;

        public UserIdendityService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IUserStore<IdentityUser> userStore,
            ILogger<UserIdendityService> logger,
            IUserEmailStore<IdentityUser> emailStore
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userStore = userStore;
            _logger = logger;
            _emailStore = emailStore;
        }

        public async Task<IdentityResult> RegisterAsync(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            await _userStore.SetUserNameAsync(user, email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                await _userManager.AddToRoleAsync(user, "User");

                if (!requireConfirmation())
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }

            }

            return result;
        }

        public async Task<SignInResult> LoginAsync(string email, string password, bool rememberMe)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
            }

            return result;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<List<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
        {
            // Get the authentication schemes
            var schemes = await _signInManager.GetExternalAuthenticationSchemesAsync();
            return schemes.ToList();
        }

        public bool requireConfirmation()
        {
            return _userManager.Options.SignIn.RequireConfirmedAccount;
        }

        public bool supportsUserEmail()
        {
            return _userManager.SupportsUserEmail;
        }

        public IUserStore<IdentityUser> GetUserEmailStore()
        {
            return _userStore;
        }
    }
}
