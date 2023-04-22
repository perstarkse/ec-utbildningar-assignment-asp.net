using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApp.Contexts;
using WebApp.Models.Entities;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public class UserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IdentityContext _identityContext;

        public UserService(UserManager<IdentityUser> userManager, IdentityContext identityContext)
        {
            _userManager = userManager;
            _identityContext = identityContext;
        }

        public async Task<ProfileEntity?> GetProfileByUserIdAsync(string userId)
        {
            return await _identityContext.UserProfiles
                                          .Include(p => p.User)
                                          .FirstOrDefaultAsync(p => p.UserId == userId);
        }

    }
}
