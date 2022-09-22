using iSpend.API.ViewModels;
using iSpend.Domain.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var result = await _authentication.Authenticate(model.Email, model.Password);

        if (result)
        {
            var user = _authentication.GetUserNameAndId(model.Email);

            var name = user.Result.ElementAt(0).ToString();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, user.Result.ElementAt(1).ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Result.ElementAt(2).ToString()),
            };

            var token = GenerateToken(claims);
            var refreshToken = GenerateRefreshToken();
            _ = int.TryParse(_configuration["JwtBearerTokenSettings:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            await _authentication.UpdateAsync(model.Email, refreshToken, refreshTokenValidityInDays);

            return Ok(new
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            });
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return BadRequest(ModelState);
        }
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        try
        {
            var result = await _authentication.Register(model.Name, model.Email, model.Password);

            if (result.Succeded)
            {
                return Ok($"Succesfully registered");
            }
            else
            {
                var message = result.Errors?.First();
                return BadRequest(message);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpPost]
    [Route("Revoke/{email}")]
    public async Task<IActionResult> Revoke(string email)
    {
        var user = await _authentication.GetUser(email);
        if (user == null) return BadRequest("Invalid user name");

        _ = int.TryParse(_configuration["JwtBearerTokenSettings:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
        await _authentication.UpdateAsync(email, null, refreshTokenValidityInDays);

        return NoContent();
    }

    [HttpPost]
    [Route("RefreshToken")]
    public async Task<IActionResult> RefreshToken(UserToken tokenModel)
    {
        if (tokenModel is null)
        {
            return BadRequest("Invalid client request");
        }

        string? accessToken = tokenModel.AccessToken;
        string? refreshToken = tokenModel.RefreshToken;

        var principal = GetPrincipalFromExpiredToken(accessToken);
        if (principal == null)
        {
            return BadRequest("Invalid access token or refresh token");
        }

        string username = principal.FindFirstValue(ClaimTypes.Email);

        var user = _authentication.GetUser(username);

        var userRefToken = user.Result.ElementAt(2);
        _ = DateTime.TryParse(user.Result.ElementAt(3), out DateTime userRefTokenExpiryTime);

        if (user == null || userRefToken != refreshToken || userRefTokenExpiryTime <= DateTime.Now)
        {
            return BadRequest("Invalid access token or refresh token");
        }

        var newAccessToken = GenerateToken(principal.Claims.ToList());
        var newRefreshToken = GenerateRefreshToken();
        _ = int.TryParse(_configuration["JwtBearerTokenSettings:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

        await _authentication.UpdateAsync(username, newRefreshToken, refreshTokenValidityInDays);

        return new ObjectResult(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken = newRefreshToken
        });
    }

    private JwtSecurityToken GenerateToken(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtBearerTokenSettings:SecretKey"]));

        _ = int.TryParse(_configuration["JwtBearerTokenSettings:TokenValidityInMinutes"], out int tokenValidityInMinutes);

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.Now.AddMinutes(tokenValidityInMinutes);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["JwtBearerTokenSettings:Issuer"],
            audience: _configuration["JwtBearerTokenSettings:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: creds);

        return token;
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtBearerTokenSettings:SecretKey"])),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;

    }
}
