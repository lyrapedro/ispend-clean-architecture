using iSpend.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace iSpend.Infra.Data.Identity;

public class SeedUserRoleInitial : ISeedUserRoleInitial
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SeedUserRoleInitial(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public void SeedUsers()
    {
        if (_userManager.FindByEmailAsync("usuario@localhost").Result == null)
        {
            var user = new ApplicationUser
            {
                UserName = "usuario@localhost",
                Email = "usuario@localhost",
                NormalizedUserName = "USUARIO@LOCALHOST",
                NormalizedEmail = "USUARIO@LOCALHOST",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = _userManager.CreateAsync(user, "123456").Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "User").Wait();
            }
        }

        if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
        {
            var user = new ApplicationUser
            {
                UserName = "admin@localhost",
                Email = "admin@localhost",
                NormalizedUserName = "ADMIN@LOCALHOST",
                NormalizedEmail = "ADMIN@LOCALHOST",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = _userManager.CreateAsync(user, "123456").Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }

    public async void SeedRoles()
    {
        if (!_roleManager.RoleExistsAsync("User").Result)
        {
            var role = new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER"
            };
            await _roleManager.CreateAsync(role);
        }

        if (!_roleManager.RoleExistsAsync("Admin").Result)
        {
            var role = new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            };
            await _roleManager.CreateAsync(role);
        }
    }
}
