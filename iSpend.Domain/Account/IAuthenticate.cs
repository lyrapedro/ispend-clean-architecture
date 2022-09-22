namespace iSpend.Domain.Account;

public interface IAuthenticate
{
    Task<bool> Authenticate(string email, string password);
    Task<AuthenticateResponse> Register(string name, string email, string password);
    Task<IEnumerable<string>> GetUserNameAndId(string email);
    Task<IEnumerable<string>> GetUser(string email);
    Task UpdateAsync(string email, string refreshToken, int refreshTokenValidityInDays);
    Task Logout();
}

public class AuthenticateResponse
{
    public bool Succeded { get; set; }
    public List<string>? Errors { get; private set; }

    public AuthenticateResponse(bool succeeded, List<string>? errors = null)
    {
        Succeded = succeeded;
        Errors = errors;
    }
}
