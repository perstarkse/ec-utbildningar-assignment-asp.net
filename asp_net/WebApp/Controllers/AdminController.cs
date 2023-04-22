using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UserAdministration()
        {
            var users = await _userManager.Users.ToListAsync();
            var model = new UserAdminViewModel();

            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                model.Users.Add(new UserListItemViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = userRoles.FirstOrDefault()
                });
            }

            return View(model);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = _roleManager.Roles;

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Role = userRoles.FirstOrDefault(),
                AvailableRoles = roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList()
            };

            return View(model);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                user.Email = model.Email;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var currentRole = userRoles.FirstOrDefault(); 
                    if (currentRole != model.Role)
                    {
                        await _userManager.RemoveFromRoleAsync(user, currentRole);
                        await _userManager.AddToRoleAsync(user, model.Role);
                    }

                    return RedirectToAction("UserAdministration", "Admin");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            var roles = _roleManager.Roles;
            model.AvailableRoles = roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            return View(model);
        }
        [Authorize(Roles = "admin")]
        public IActionResult CreateUserAsAdmin()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUserAsAdmin(CreateUserAsAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(model.UserRole))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(model.UserRole));
                    }

                    var roleResult = await _userManager.AddToRoleAsync(user, model.UserRole);

                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction("UserAdministration", "Admin");
                    }
                    else
                    {
                        foreach (var error in roleResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

    }
}
