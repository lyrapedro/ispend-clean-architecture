
using iSpend.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace iSpend.Infra.Data.Identity;

public class AuthenticateService : IAuthenticate
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthenticateService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<bool> Authenticate(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);

        return result.Succeeded;
    }

    public async Task<bool> Register(string name, string email, string password)
    {
        var applicationUser = new ApplicationUser
        {
            Name = name,
            UserName = email,
            Email = email,
        };

        var result = await _userManager.CreateAsync(applicationUser, password);

        return result.Succeeded;
    }

    public async Task<IEnumerable<string>> GetUserNameAndId(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var nameAndId = new List<string>
        {
            new string(user.Name),
            new string(user.Email),
            new string(user.Id)
        };
        return nameAndId;
    }

    public async Task<IEnumerable<string>> GetUser(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return new List<string>
        {
            new string(user.Name),
            new string(user.Email),
            new string(user.RefreshToken),
            new string(user.RefreshTokenExpiryTime.ToString())
        };
    }

    public async Task UpdateAsync(string email, string refreshToken, int refreshTokenValidityInDays)
    {
        var user = await _userManager.FindByEmailAsync(email);
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
        await _userManager.UpdateAsync(user);
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }
}
