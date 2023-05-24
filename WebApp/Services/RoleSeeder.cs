using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Services
{
    public class RoleSeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleSeeder(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task SeedRoles()
        {
            if (!await _roleManager.RoleExistsAsync("admin"))
                await _roleManager.CreateAsync(new IdentityRole("admin"));
            if (!await _roleManager.RoleExistsAsync("user"))
                await _roleManager.CreateAsync(new IdentityRole("user"));
        }
    }
}