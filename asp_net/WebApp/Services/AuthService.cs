using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebApp.Contexts;
using WebApp.Models.Entities;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public class AuthService
    { 
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IdentityContext _identityContext;
        public AuthService(UserManager<IdentityUser> userManager, IdentityContext identityContext, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _identityContext = identityContext;
            _signInManager = signInManager;
        }

        public async Task<bool> SignUpAsync (RegisterViewModel registerViewModel)
        {
            try
            {
                IdentityUser identityUser = registerViewModel;
                await _userManager.CreateAsync(identityUser, registerViewModel.Password);

                ProfileEntity profileEntity = registerViewModel;
                profileEntity.UserId = identityUser.Id;

                _identityContext.UserProfiles.Add(profileEntity);
                await _identityContext.SaveChangesAsync();

                var usersInRole = await _userManager.GetUsersInRoleAsync("admin");
                if (!usersInRole.Any())
                {
                    await _userManager.AddToRoleAsync(identityUser, "admin");
                }
                else 
                { 
                    await _userManager.AddToRoleAsync(identityUser, "user"); 
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SignInAsync(SignInViewModel signInViewModel)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(signInViewModel.Email, signInViewModel.Password, signInViewModel.RememberMe, false);
                return result.Succeeded;
            }

            catch 
            { 
            return false;
            }
        }

        public async Task<bool> SignOutAsync (ClaimsPrincipal user)
        {
            await _signInManager.SignOutAsync();
            return _signInManager.IsSignedIn(user);
        }
    }
}
