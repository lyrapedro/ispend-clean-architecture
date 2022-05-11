using iSpend.API.ViewModels;
using iSpend.Domain.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace iSpend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IAuthenticate _authentication;
    public AccountController(IConfiguration configuration, IAuthenticate authentication)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        _authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));
    }

    [HttpGet]
    public async Task<IActionResult> Login(string returnUrl)
    {
        return Ok(new LoginViewModel()
        {
            ReturnUrl = returnUrl
        });
    }

    [HttpPost]
    public async Task<ActionResult<UserToken>> Login(LoginViewModel model)
    {
        var result = await _authentication.Authenticate(model.Email, model.Password);

        if (result)
        {
            return GenerateToken(model);
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return BadRequest(ModelState);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Register(string returnUrl)
    {
        return Ok(new LoginViewModel()
        {
            ReturnUrl = returnUrl
        });
    }

    [HttpPost]
    public async Task<IActionResult> Register(LoginViewModel model)
    {
        var result = await _authentication.Register(model.Email, model.Password);

        if (result)
        {
            return Ok($"Succesfully registered");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid register attempt.");
            return BadRequest(ModelState);
        }

    }

    private ActionResult<UserToken> GenerateToken(LoginViewModel model)
    {
        var claims = new[]
        {
            new Claim("email", model.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtBearerTokenSettings: SecretKey"]));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddMinutes(15);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["JwtBearerTokenSettings:Issuer"],
            audience: _configuration["JwtBearerTokenSettings:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: creds);

        return new UserToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration,
        };
    }
}
